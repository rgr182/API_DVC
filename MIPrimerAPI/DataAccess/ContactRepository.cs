using Dapper;
using MIPrimerAPI.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Data.SqlClient;

namespace MIPrimerAPI.DataAccess
{
    public interface IContactRepository
    {
        Task<bool> CreateContact(Contact contact);
        IEnumerable<Contact> GetContactByDate(DateTime date);
        IEnumerable<Contact> GetContactByEmail(string email);
    }

    public class ContactRepository : IContactRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;
        private readonly string _emailSender;
        private readonly string _emailRecipient;

        public ContactRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SchoolConnection");
            _emailSender = _configuration.GetSection("EmailSettings:Sender").Value;
            _emailRecipient = _configuration.GetSection("EmailSettings:Recipient").Value;
        }

        public async Task<bool> CreateContact(Contact contact)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Contact (Name, Email, Comment, CreationDate) " +
                                   "VALUES (@Name, @Email, @Comment, @CreationDate)";

                    var parameters = new
                    {
                        contact.Name,
                        contact.Email,
                        contact.Comment,
                        CreationDate = DateTime.UtcNow
                    };

                    connection.Execute(query, parameters);
                }

                await SendEmail(contact); // Enviar el correo electrónico

                return true;
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrar el error
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

        private async Task SendEmail(Contact contact)
        {
            string apiKey = _configuration.GetSection("EmailSettings:ApiKey").Value;
            string senderEmail = _configuration.GetSection("EmailSettings:Sender").Value;
            string recipientEmail = "rgr_182@hotmail.com";
            string senderName = "Nombre Remitente";
            string recipientName = "Nombre Destinatario";
            string subject = "Asunto del correo";
            string plainTextContent = "Este es el contenido de texto sin formato.";
            string htmlContent = "<p>Este es el contenido HTML del correo.</p>";

            // Crear un cliente SendGrid
            var client = new SendGridClient(apiKey);

            // Construir el objeto Email
            var from = new EmailAddress(senderEmail, senderName);
            var to = new EmailAddress(recipientEmail, recipientName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            try
            {
                // Enviar el correo electrónico
                var response = await client.SendEmailAsync(msg);

                // Verificar el código de respuesta
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Correo enviado exitosamente");
                }
                else
                {
                    Console.WriteLine("Error al enviar el correo: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }

    }
}
