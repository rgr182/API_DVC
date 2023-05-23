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

        private void Execute(string script)
        {
            string connectionString = _configuration.GetConnectionString("SchoolConnection");
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
            Execute(script);
            return true;
        }


        //TODO:GetContactByDate



    }


}
