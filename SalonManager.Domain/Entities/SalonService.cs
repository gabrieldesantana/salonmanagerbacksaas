namespace SalonManager.Domain.Entities;

public class SalonService : BaseEntity
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public double Price { get; set; }

}
public record InputSalonServiceModel 
(
    int Id, string? Name, string? Category, double Price, string TenantId
);

public record EditSalonServiceModel 
(
    int Id, string? Name, string? Category, double Price, string TenantId
);
