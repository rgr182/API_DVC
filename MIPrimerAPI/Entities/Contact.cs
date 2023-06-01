using System.Text.Json.Serialization;

namespace MIPrimerAPI.Entities
{
    public class Contact
    {
        [JsonIgnore]
        int ContactId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
