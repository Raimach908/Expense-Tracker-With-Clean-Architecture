﻿using Microsoft.Data.SqlClient;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly string _connectionString;

        public IncomeRepository(string connectionString)
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExpenseTracker;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }
        public decimal GetTotalIncome(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT Sum(Amount) as TotalIncome FROM Income WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var result = command.ExecuteScalar() as decimal?;
                // Use the null-coalescing operator to return 0 if the result is null
                return result ?? 0m;
            }
        }
        public DateTime GetLastIncome(int userId)
        {
            DateTime lastIncomeDate = DateTime.MinValue;

            string query = @"SELECT TOP 1 [Date] FROM [Income] WHERE [UserId] = @UserId ORDER BY [Date] DESC;";

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
                        lastIncomeDate = Convert.ToDateTime(result);
                    }
                }
            }

            return lastIncomeDate;
        }
        public decimal GetMonthlyReport(int userId, DateTime date)
        {
            var startOfMonth = new DateTime(date.Year, date.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            string query = "SELECT SUM(Amount) AS TotalAmount FROM [Income] WHERE [UserId] = @UserId AND [Date] BETWEEN @StartOfMonth AND @EndOfMonth;";

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
            var startOfYear = new DateTime(year, 1, 1);
            var endOfYear = new DateTime(year, 12, 31);

            string query = "SELECT SUM(Amount) AS TotalAmount FROM [Income] WHERE [UserId] = @UserId AND [Date] BETWEEN @StartOfYear AND @EndOfYear;";

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
            string query = "SELECT SUM(Amount) AS TotalAmount FROM [Income] WHERE [UserId] = @UserId AND [Date] = @Date;";

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
        public IEnumerable<IncomeEntity> GetAllIncome(int userId)
        {
            var incomes = new List<IncomeEntity>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Income WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    incomes.Add(new IncomeEntity
                    {
                        Id = (int)reader["Id"],
                        category = (string)reader["category"],
                        Title = (string)reader["Title"],
                        Amount = (decimal)reader["Amount"],
                        Date = (DateTime)reader["Date"],
                        UserId = (int)reader["UserId"]
                    });
                }
            }

            return incomes;
        }

        public IncomeEntity GetIncomeById(int id)
        {
            IncomeEntity income = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Income WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    income = new IncomeEntity
                    {
                        Id = (int)reader["Id"],
                        category = (string)reader["category"],
                        Title = (string)reader["Title"],
                        Amount = (decimal)reader["Amount"],
                        Date = (DateTime)reader["Date"],
                        UserId = (int)reader["UserId"]
                    };
                }
            }

            return income;
        }
        public void AddIncome(IncomeEntity income)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(
                    "INSERT INTO Income (category, Title, Amount, Date, UserId) VALUES (@category, @Title, @Amount, @Date, @UserId)",
                    connection
                );
                insertCommand.Parameters.AddWithValue("@category", income.category);
                insertCommand.Parameters.AddWithValue("@Title", income.Title);
                insertCommand.Parameters.AddWithValue("@Amount", income.Amount);
                insertCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                insertCommand.Parameters.AddWithValue("@UserId", income.UserId);

                insertCommand.ExecuteNonQuery();
            }
        }
        public void UpdateIncome(IncomeEntity income)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Income SET category = @category,Title = @Title, Amount = @Amount, Date = @Date WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@category", income.category);
                command.Parameters.AddWithValue("@Title", income.Title);
                command.Parameters.AddWithValue("@Amount", income.Amount);
                command.Parameters.AddWithValue("@Date", income.Date);
                command.Parameters.AddWithValue("@Id", income.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteIncome(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Income WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
