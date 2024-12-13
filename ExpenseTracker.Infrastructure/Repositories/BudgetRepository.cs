using Microsoft.Data.SqlClient;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly string _connectionString;

        public BudgetRepository(string connectionString)
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExpenseTracker;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public IEnumerable<BudgetEntity> GetAllBudgets(int userId)
        {
            var budgets = new List<BudgetEntity>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Budget WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    budgets.Add(new BudgetEntity
                    {
                        Id = (int)reader["Id"],
                        category = (string)reader["category"],
                        Limit = (decimal)reader["Limit"],
                        Spent = (decimal)reader["Spent"],
                        UserId = (int)reader["UserId"]
                    });
                }
            }

            return budgets;
        }

        public BudgetEntity GetBudgetById(int id)
        {
            BudgetEntity budget = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Budget WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    budget = new BudgetEntity
                    {
                        Id = (int)reader["Id"],
                        category = (string)reader["category"],
                        Limit = (decimal)reader["Limit"],
                        Spent = (decimal)reader["Spent"],
                        UserId = (int)reader["UserId"]
                    };
                }
            }

            return budget;
        }

        public void AddBudget(BudgetEntity budget)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(
                    "INSERT INTO Budget (category, Limit, Spent, UserId) VALUES (@category, @Limit, @Spent, @UserId)",
                    connection
                );
                insertCommand.Parameters.AddWithValue("@category", budget.category);
                insertCommand.Parameters.AddWithValue("@Limit", budget.Limit);
                insertCommand.Parameters.AddWithValue("@Spent", budget.Spent);
                insertCommand.Parameters.AddWithValue("@UserId", budget.UserId);

                insertCommand.ExecuteNonQuery();
            }
        }

        public void UpdateBudget(BudgetEntity budget)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Budget SET category = @category, Limit = @Limit, Spent = @Spent WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@category", budget.category);
                command.Parameters.AddWithValue("@Limit", budget.Limit);
                command.Parameters.AddWithValue("@Spent", budget.Spent);
                command.Parameters.AddWithValue("@Id", budget.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteBudget(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Budget WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
