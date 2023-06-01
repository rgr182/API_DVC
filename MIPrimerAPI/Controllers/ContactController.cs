using Microsoft.AspNetCore.Mvc;
using MIPrimerAPI.DataAccess;
using MIPrimerAPI.Entities;


namespace MIPrimerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        ILogger<ContactController> _logger;

        IContactRepository _contactRepository;

        public ContactController(ILogger<ContactController> logger,
                                 IContactRepository contactRepository) {

            _logger = logger;
            _contactRepository = contactRepository;
        }
        
       
        [HttpPost]
        public IActionResult Contact([FromBody] Contact contact)
        {          
            _contactRepository.CreateContact(contact);

                return Ok("Thanks for your comments!" +
                  "someone will contact you shortly");          
        }
    }
}