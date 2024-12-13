using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExpenseTracker;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public UserEntity GetUserProfile(int userId)
        {
            UserEntity user = new UserEntity
            {
                UserId = 0,
                Username = string.Empty,
                Password = string.Empty,
                Email = string.Empty,
                PhoneNo = string.Empty,
                CreatedDate = DateTime.MinValue,
                ProfilePicturePath = new byte[0] // Default to empty byte array
            };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.Username = reader["Username"]?.ToString() ?? string.Empty;
                    user.Password = reader["Password"]?.ToString() ?? string.Empty;
                    user.Email = reader["Email"]?.ToString() ?? string.Empty;
                    user.PhoneNo = reader["PhoneNo"]?.ToString() ?? string.Empty;
                    user.CreatedDate = reader["CreatedDate"] != DBNull.Value
                        ? Convert.ToDateTime(reader["CreatedDate"])
                        : DateTime.MinValue;
                    user.ProfilePicturePath = reader["ProfilePicturePath"] as byte[] ?? new byte[0];
                }
            }

            return user;
        }

        public void updateUserProfile(UserEntity user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
            UPDATE Users 
            SET 
                Username = @Username, 
                Password = @Password, 
                Email = @Email, 
                PhoneNo = @PhoneNo, 
                CreatedDate = @CreatedDate,
                ProfilePicturePath = @ProfilePicturePath
            WHERE 
                UserId = @UserId";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Add parameters for the query
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password); // Ensure password is hashed in real applications
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PhoneNo", user.PhoneNo);
                cmd.Parameters.AddWithValue("@CreatedDate", user.CreatedDate);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);

                // Handle ProfilePicturePath (binary data)
                if (user.ProfilePicturePath != null)
                {
                    cmd.Parameters.Add("@ProfilePicturePath", SqlDbType.VarBinary).Value = user.ProfilePicturePath;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ProfilePicturePath", DBNull.Value);
                }

                // Open the connection and execute the query
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void deleteProfile(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Users WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public UserEntity GetUserByEmail(string email)
        {
            UserEntity user = new UserEntity
            {
                UserId = 0,
                Username = string.Empty,
                Password = string.Empty,
                Email = string.Empty,
                PhoneNo = string.Empty,
                CreatedDate = DateTime.MinValue,
                ProfilePicturePath = new byte[0]
            };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.Username = reader["Username"]?.ToString() ?? string.Empty;
                    user.Password = reader["Password"]?.ToString() ?? string.Empty;
                    user.Email = reader["Email"]?.ToString() ?? string.Empty;
                    user.PhoneNo = reader["PhoneNo"]?.ToString() ?? string.Empty;
                    user.CreatedDate = reader["CreatedDate"] != DBNull.Value
                        ? Convert.ToDateTime(reader["CreatedDate"])
                        : DateTime.MinValue;
                    user.ProfilePicturePath = reader["ProfilePicturePath"] as byte[] ?? new byte[0];
                }
            }

            return user;
        }

        public void AddUser(UserEntity user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (Username, Password, Email, PhoneNo, CreatedDate, ProfilePicturePath) VALUES (@Username, @Password, @Email, @PhoneNo, @CreatedDate, @ProfilePicturePath)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password); // Ensure password is hashed in real applications
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PhoneNo", user.PhoneNo);
                cmd.Parameters.AddWithValue("@CreatedDate", user.CreatedDate);
                cmd.Parameters.AddWithValue("@ProfilePicturePath", (object)user.ProfilePicturePath ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool CheckUserExists(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0;
            }
        }
    }
}
