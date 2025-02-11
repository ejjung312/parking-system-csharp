using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Domain.Models
{
    public class DomainObject
    {
        [Key]
        public int Id { get; set; }
    }
}
