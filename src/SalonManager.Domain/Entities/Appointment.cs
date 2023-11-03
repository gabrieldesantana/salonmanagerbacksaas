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
    public EAppointmentStatus Status { get; set; }
    public string? Description { get; set; }

    public string? PaymentMethod { get; set; }
    public string? PaymentWay { get; set; }
    public double Value { get; set; }
    public bool Finished { get; set; }


    public Appointment()
    {
        Status = EAppointmentStatus.Pendente;
        Value = 0;
    }

    public void ValidateStatus()
    {
        var isPending = DateTime.Now < Date;

        if (Status == EAppointmentStatus.Pendente)
        {
            this.Status = (Date <= DateTime.Now) ? EAppointmentStatus.Iniciado : Status;
        }
        else if (Status == EAppointmentStatus.Iniciado)
        {
            this.Status = (Date >= DateTime.Now) ? EAppointmentStatus.Pendente : Status;
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

public record FinishAppointmentModel
(
    int Id,string? PaymentMethod, string? PaymentWay, double Value, bool Finished
);
