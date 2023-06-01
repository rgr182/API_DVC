using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MIPrimerAPI.DataAccess;
using MIPrimerAPI.Entities;
using System;

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

        [HttpGet("date/{date}")]
        public IActionResult GetDonationByDate(DateTime date)
        {
            var donations = _donationRepository.GetDonationByDate(date);

            return Ok(donations);
        }

        [HttpGet("email/{email}")]
        public IActionResult GetDonationByEmail(string email)
        {
            var donations = _donationRepository.GetDonationByEmail(email);

            return Ok(donations);
        }
    }
}
