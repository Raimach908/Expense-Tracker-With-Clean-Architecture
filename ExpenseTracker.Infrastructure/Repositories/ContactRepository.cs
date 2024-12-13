using Microsoft.Data.SqlClient;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly string _connectionString;

        public ContactRepository(string connectionString)
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExpenseTracker;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        // Method to add a contact to the database
        public void AddContact(ContactEntity contact)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Contacts (Name, Email, Phone, Subject, Message, DateSubmitted) VALUES (@Name, @Email, @Phone, @Subject, @Message, @DateSubmitted)", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", contact.Name);
                    cmd.Parameters.AddWithValue("@Email", contact.Email);
                    cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                    cmd.Parameters.AddWithValue("@Subject", contact.Subject);
                    cmd.Parameters.AddWithValue("@Message", contact.Message);
                    cmd.Parameters.AddWithValue("@DateSubmitted", contact.DateSubmitted);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to retrieve all contacts from the database
        public IEnumerable<ContactEntity> GetContacts()
        {
            List<ContactEntity> contacts = new List<ContactEntity>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Contacts", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContactEntity contact = new ContactEntity
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Subject = reader["Subject"].ToString(),
                                Message = reader["Message"].ToString(),
                                DateSubmitted = (DateTime)reader["DateSubmitted"]
                            };

                            contacts.Add(contact);
                        }
                    }
                }
            }

            return contacts;
        }
    }
}
