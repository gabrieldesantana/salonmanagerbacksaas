namespace SalonManager.Domain.Entities;

public class Appointment : BaseEntity
{
    //public int EmployeeId { get; set; }
    public int CustomerId { get; set; }
    public Customer? CustomerAppointment { get; set; }
    public int ServiceId { get; set; } 
    public SalonService? ServiceAppointment { get; set; }
    public DateTime EntryDate { get; set; }
    public double Value { get; set; }
    public bool Done { get; set; }

    public void UpdateStatus()
    {
        this.Done = Done ? false : true; 
    }
}

public record InputAppointmentModel 
(
    int Id, Customer? CustomerAppointment, SalonService? ServiceAppointment, DateTime EntryDate, double Value
);

public record EditAppointmentModel 
(
    int Id, SalonService? ServiceAppointment, DateTime EntryDate, double Value
); 
