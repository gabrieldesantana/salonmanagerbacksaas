using SalonManager.Domain.Enums;

namespace SalonManager.Domain.Entities
{
    public class Employee : Person
    {
        public User? User { get; private set; }
        public int UserId { get; set; }//
        public Company? Company { get; private set; }//
        public int CompanyId { get; set; }//
        public List<Appointment>? Appointments { get; set; }
    }
}
