using MIPrimerAPI.Entities;
using System.Data.SqlClient;

namespace MIPrimerAPI.DataAccess
{

    public interface IDonationRepository
    {
        public bool CreateDonation(Donation contact);
    }


    public class DonationRepository : IDonationRepository
    {
        IConfiguration _configuration;
        public DonationRepository(IConfiguration configuration)
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

        public bool CreateDonation(Donation donation)
        {
            string script = $"insert into Donation (Name,Email, Amount, Card, CreationDate) " +
                            $"values ('{donation.Name}','{donation.Email}','{donation.Amount}','{donation.Card}','{DateTime.UtcNow}')";
           
            Execute(script);
            return true;
        }


        //TODO:GetDonationByDate



    }


}
