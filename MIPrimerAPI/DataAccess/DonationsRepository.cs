using Dapper;
using MIPrimerAPI.Entities;
using System.Data.SqlClient;

namespace MIPrimerAPI.DataAccess
{

    public interface IDonationRepository
    {
        bool CreateDonation(Donation donation);
        IEnumerable<Donation> GetDonationByDate(DateTime date);
        IEnumerable<Donation> GetDonationByEmail(string email);
    }



    public class DonationRepository : IDonationRepository
    {
        IConfiguration _configuration;
        string _connectionString;
        public DonationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SchoolConnection");
        }    

        public bool CreateDonation(Donation donation)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Donation (Name, Email, Amount, Card, CreationDate) " +
                                   "VALUES (@Name, @Email, @Amount, @Card, @CreationDate)";

                    var parameters = new
                    {
                        Name = donation.Name,
                        Email = donation.Email,
                        Amount = donation.Amount,
                        Card = donation.Card,
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



        public IEnumerable<Donation> GetDonationByDate(DateTime date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Donation WHERE CreationDate >= @Date";

                var parameters = new
                {
                    Date = date.Date // Considerar solo la fecha, sin la hora
                };

                return connection.Query<Donation>(query, parameters);
            }
        }

        public IEnumerable<Donation> GetDonationByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Donation WHERE Email = @Email";

                var parameters = new
                {
                    Email = email
                };

                return connection.Query<Donation>(query, parameters);
            }
        }
    }
}
