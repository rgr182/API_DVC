using System.Text.Json.Serialization;

namespace MIPrimerAPI.Entities
{
    public class Donation
    {
        [JsonIgnore]
        public int DonationId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Amount { get; set; }

        public string Card { get; set; }

        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
