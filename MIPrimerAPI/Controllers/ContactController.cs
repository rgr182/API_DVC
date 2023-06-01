using Microsoft.AspNetCore.Mvc;
using MIPrimerAPI.DataAccess;
using MIPrimerAPI.Entities;

namespace MIPrimerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactRepository _contactRepository;

        public ContactController(ILogger<ContactController> logger, IContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository;
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] Contact contact)
        {
            _contactRepository.CreateContact(contact);

            return Ok("Thanks for your contact! Someone will get in touch with you shortly.");
        }

        [HttpGet("date/{date}")]
        public IActionResult GetContactByDate(DateTime date)
        {
            var contacts = _contactRepository.GetContactByDate(date);

            return Ok(contacts);
        }

        [HttpGet("email/{email}")]
        public IActionResult GetContactByEmail(string email)
        {
            var contacts = _contactRepository.GetContactByEmail(email);

            return Ok(contacts);
        }
    }
}