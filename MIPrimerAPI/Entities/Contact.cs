namespace MIPrimerAPI.Entities
{
    public class Contact
    {       
        int ContactId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }              
        
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
