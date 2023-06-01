using Microsoft.AspNetCore.Mvc;
using MIPrimerAPI.DataAccess;
using MIPrimerAPI.Entities;

namespace MIPrimerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationController : ControllerBase
    {
        private readonly ILogger<DonationController> _logger;
        private readonly IDonationRepository _donationRepository;

        public DonationController(ILogger<DonationController> logger, IDonationRepository donationRepository)
        {
            _logger = logger;
            _donationRepository = donationRepository;
        }

        [HttpPost]
        public IActionResult CreateDonation([FromBody] Donation donation)
        {
            _donationRepository.CreateDonation(donation);

            return Ok("Thanks for your donation! Someone will contact you shortly.");
        }
    }
}