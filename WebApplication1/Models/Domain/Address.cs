namespace WebApplication1.Models.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string StreetName { get; set; } 
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string FloorUnit { get; set; }
    }
}
    