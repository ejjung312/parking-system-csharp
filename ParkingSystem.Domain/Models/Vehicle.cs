namespace ParkingSystem.Domain.Models
{
    public class Vehicle : DomainObject
    {
        public string LicenseNumber { get; set; }
        public byte[] LicensePlateImg { get; set; }
        public DateTime EnterDate { get; set; }
    }
}
