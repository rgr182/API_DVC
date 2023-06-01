using Dapper;
using MIPrimerAPI.Entities;
using System.Data.SqlClient;

namespace MIPrimerAPI.DataAccess
{

    public interface IContactRepository
    {
        bool CreateContact(Contact contact);
        IEnumerable<Contact> GetContactByDate(DateTime date);
        IEnumerable<Contact> GetContactByEmail(string email);
    }


    public class ContactRepository : IContactRepository
    {
        IConfiguration _configuration;
        string _connectionString;
        public ContactRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SchoolConnection");
        }

        public bool CreateContact(Contact contact)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Contact (Name, Email, Comment, CreationDate) " +
                                   "VALUES (@Name, @Email, @Comment, @CreationDate)";

                    var parameters = new
                    {
                        Name = contact.Name,
                        Email = contact.Email,
                        Comment = contact.Comment,
                        CreationDate = DateTime.UtcNow
                    };

                    connection.Execute(query, parameters);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception and log the error
                return false;
            }
        }

        public IEnumerable<Contact> GetContactByDate(DateTime date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contact WHERE CreationDate >= @Date";

                var parameters = new
                {
                    Date = date.Date
                };

                return connection.Query<Contact>(query, parameters);
            }
        }

        public IEnumerable<Contact> GetContactByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contact WHERE Email = @Email";

                var parameters = new
                {
                    Email = email
                };

                return connection.Query<Contact>(query, parameters);
            }
        }
    }
}
