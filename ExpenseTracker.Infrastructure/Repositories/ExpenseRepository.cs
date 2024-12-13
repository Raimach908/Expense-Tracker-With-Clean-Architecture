using Microsoft.Data.SqlClient;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly string _connectionString;
        private readonly IIncomeRepository _incomeRepository;

        public ExpenseRepository(string connectionString, IIncomeRepository incomeRepository)
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExpenseTracker;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            _incomeRepository = incomeRepository;
        }
        public decimal GetTotalExpense(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT Sum(Amount) as TotalExpense FROM Expense WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var result = command.ExecuteScalar() as decimal?;
                // Use the null-coalescing operator to return 0 if the result is null
                return result ?? 0m;
            }
        }
        public DateTime GetLastExpense(int userId)
        {
            DateTime lastExpenseDate = DateTime.MinValue;

            string query = @"SELECT TOP 1 [Date] FROM [Expense] WHERE [UserId] = @UserId ORDER BY [Date] DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    // Check if the result is not null and convert it to DateTime
                    if (result != null && result != DBNull.Value)
                    {
                        lastExpenseDate = Convert.ToDateTime(result);
                    }
                }
            }

            return lastExpenseDate;
        }
        public decimal GetMonthlyReport(int userId, DateTime date)
        {
            // Define the start and end of the month
            var startOfMonth = new DateTime(date.Year, date.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            string query = @"SELECT SUM(Amount) as TotalAmount FROM [Expense] WHERE [UserId] = @UserId AND [Date] BETWEEN @StartOfMonth AND @EndOfMonth;";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@StartOfMonth", startOfMonth);
                    command.Parameters.AddWithValue("@EndOfMonth", endOfMonth);

                    connection.Open();
                    var result = command.ExecuteScalar() as decimal?;
                    return result ?? 0m;
                }
            }
        }

        public decimal GetYearlyReport(int userId, int year)
        {
            // Define the start and end of the year
            var startOfYear = new DateTime(year, 1, 1);
            var endOfYear = new DateTime(year, 12, 31);

            string query = "SELECT SUM(Amount) AS TotalAmount FROM [Expense] WHERE [UserId] = @UserId AND [Date] BETWEEN @StartOfYear AND @EndOfYear;";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@StartOfYear", startOfYear);
                    command.Parameters.AddWithValue("@EndOfYear", endOfYear);

                    connection.Open();
                    var result = command.ExecuteScalar() as decimal?;
                    return result ?? 0m;
                }
            }
        }

        public decimal GetDailyReport(int userId, DateTime date)
        {
            string query = "SELECT SUM(Amount) AS TotalAmount FROM [Expense] WHERE [UserId] = @UserId AND [Date] = @Date;";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Date", date.Date);

                    connection.Open();
                    var result = command.ExecuteScalar() as decimal?;
                    return result ?? 0m;
                }
            }
        }
        public bool IsBalanceSufficient(int userId, decimal expenseAmount)
        {
            decimal totalIncome = _incomeRepository.GetTotalIncome(userId);
            decimal totalExpense = GetTotalExpense(userId);
            decimal balance = totalIncome - totalExpense;
            return balance >= expenseAmount;
        }

        public IEnumerable<ExpenseEntity> GetAllExpenses(int userId)
        {
            var expenses = new List<ExpenseEntity>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Expense WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    expenses.Add(new ExpenseEntity
                    {
                        Id = (int)reader["Id"],
                        Category = (string)reader["Category"],
                        Title = (string)reader["Title"],
                        Amount = (decimal)reader["Amount"],
                        Date = (DateTime)reader["Date"],
                        UserId = (int)reader["UserId"]
                    });
                }
            }

            return expenses;
        }

        public ExpenseEntity GetExpenseById(int id)
        {
            ExpenseEntity expense = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Expense WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    expense = new ExpenseEntity
                    {
                        Id = (int)reader["Id"],
                        Category = (string)reader["Category"],
                        Title = (string)reader["Title"],
                        Amount = (decimal)reader["Amount"],
                        Date = (DateTime)reader["Date"],
                        UserId = (int)reader["UserId"]
                    };
                }
            }

            return expense;
        }

        public void AddExpense(ExpenseEntity expense)
        {
            if (!IsBalanceSufficient(expense.UserId, expense.Amount))
            {
                throw new InvalidOperationException("Insufficient balance.");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    "INSERT INTO Expense (Category, Title, Amount, Date, UserId) VALUES (@Category, @Title, @Amount, @Date, @UserId)",
                    connection
                );
                command.Parameters.AddWithValue("@Category", expense.Category);
                command.Parameters.AddWithValue("@Title", expense.Title);
                command.Parameters.AddWithValue("@Amount", expense.Amount);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@UserId", expense.UserId);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateExpense(ExpenseEntity expense)
        {
            if (!IsBalanceSufficient(expense.UserId, expense.Amount))
            {
                throw new InvalidOperationException("Insufficient balance.");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE Expense SET Category = @Category, Title = @Title, Amount = @Amount, Date = @Date WHERE Id = @Id",
                    connection
                );
                command.Parameters.AddWithValue("@Category", expense.Category);
                command.Parameters.AddWithValue("@Title", expense.Title);
                command.Parameters.AddWithValue("@Amount", expense.Amount);
                command.Parameters.AddWithValue("@Date", expense.Date);
                command.Parameters.AddWithValue("@Id", expense.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteExpense(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Expense WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
