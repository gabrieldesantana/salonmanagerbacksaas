namespace SalonManager.Domain.Entities;

public class Finance : BaseEntity
{
    public string? Type { get; set; }
    public string? Description { get; set; }
    public double Value { get; set; }
    public DateTime EntryDate { get; set; }
    public string? PaymentType { get; set; }
    public Appointment? Appointment { get; set; }
}

public record InputFinanceModel 
(
    int Id, string? Type, double Value, DateTime EntryDate, string? PaymentType
);

public record EditFinanceModel 
(
    int Id, string? Description, string? Type
);
