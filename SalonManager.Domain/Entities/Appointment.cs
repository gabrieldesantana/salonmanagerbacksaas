using SalonManager.Domain.Enums;

namespace SalonManager.Domain.Entities;

public class Appointment : BaseEntity
{
    public int EmployeeAppointmentId { get; set; } //
    public Employee EmployeeAppointment { get; private set; }
    public int CustomerAppointmentId { get; set; } //
    public Customer? CustomerAppointment { get; private set; }
    public int ServiceAppointmentId { get; set; }  //
    public SalonService? ServiceAppointment { get; private set; }

    public DateTime Date { get; set; } //
    public EAppointmentStatus Status { get; set; } //
    public string? Description { get; set; } //
    public string? PaymentMethod { get; set; } //
    public string? PaymentWay { get; set; } //
    public double Cost { get; set; } //
    public bool Finished { get; set; } //
    public DateTime? FinishedDate { get; set; } //

    /// <summary>
    /// Construtor padrão. 
    /// </summary>
    public Appointment()
    {
        Status = EAppointmentStatus.Pendente;
        Cost = 0;
    }

    /// <summary>
    /// ### Construtor para InputAppointmentModel. ###
    /// </summary>
    public Appointment(Customer customerAppointment, SalonService serviceAppointment, Employee employeeAppointment) 
    { 
        CustomerAppointment = customerAppointment;
        ServiceAppointment = serviceAppointment;
        EmployeeAppointment = employeeAppointment;

        CustomerAppointmentId = customerAppointment.Id;
        ServiceAppointmentId = serviceAppointment.Id;
        EmployeeAppointmentId = employeeAppointment.Id;

        Status = EAppointmentStatus.Pendente;
        Cost = 0;
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
    int Id, Customer? CustomerAppointment, int CustomerAppointmentId,
    int ServiceAppointmentId,  SalonService? ServiceAppointment, string Description,
    DateTime Date, double Cost, string TenantId, int UserCreatorId
);

public record EditAppointmentModel 
(
    int Id, EAppointmentStatus Status, DateTime Date, string TenantId
);

public record FinishAppointmentModel
(
    int Id,string? PaymentMethod, string? PaymentWay,
    double Cost, bool Finished, int CustomerAppointmentId, 
    string? Description, string TenantId
);

public record FinanceAppointmentModel
(
   DateTime StartDate,
   DateTime EndDate,
   string? TenantId
);

public record FinanceAppointmentViewModel
(
   List<Appointment>? Appointments,
   double Total,
   DateTime StartDate,
   DateTime EndDate,
   string? StartDateString,
   string? EndDateString
);