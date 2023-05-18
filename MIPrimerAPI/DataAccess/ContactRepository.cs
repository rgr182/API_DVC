using Microsoft.Extensions.Configuration;
using MIPrimerAPI.Entities;
using System.Data.SqlClient;

namespace MIPrimerAPI.DataAccess
{

    public interface IContactRepository
    {
        public bool CreateContact(Contact contact);
    }


    public class ContactRepository : IContactRepository
    {
        IConfiguration _configuration;
        public ContactRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static void CreateCommand(string script, string connectionString)
        {            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(script, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool CreateContact(Contact contact)
        {
            string script = $"insert into contact (Name, Email,Comment, CreationDate) " +
                            $"values ('{contact.Name}','{contact.Email}','{contact.Comment}','{DateTime.UtcNow}')";
            string connectionString = _configuration.GetConnectionString("SchoolConnection");
            CreateCommand(script, connectionString);
            return true;
        }
    }


}
