using SalonManager.Domain.Enums;

namespace SalonManager.Domain.Entities;

public class Appointment : BaseEntity
{
    //public int EmployeeId { get; set; }
    public int CustomerAppointmentId { get; set; }
    public Customer? CustomerAppointment { get; set; }
    public int ServiceAppointmentId { get; set; } 
    public SalonService? ServiceAppointment { get; set; }
    public DateTime Date { get; set; }
    public double Value { get; set; }
    public EAppointmentStatus Status { get; set; }
    public string? Description { get; set; }
    public Appointment()
    {
        Status = EAppointmentStatus.Pendente;
        Value = 0;
    }

    public void ValidateStatus()
    {
        if (Status == EAppointmentStatus.Pendente)
        {
            this.Status = (Date <= DateTime.Now) ? EAppointmentStatus.Iniciado : EAppointmentStatus.Pendente;
        }
    }
}

public record InputAppointmentModel 
(
    int Id, Customer? CustomerAppointment, int CustomerAppointmentId, int ServiceAppointmentId,  SalonService? ServiceAppointment, string Description, DateTime Date, double Value
);

public record EditAppointmentModel 
(
    int Id, EAppointmentStatus Status, DateTime Date
); 
