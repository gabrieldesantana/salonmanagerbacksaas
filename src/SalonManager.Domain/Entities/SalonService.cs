namespace SalonManager.Domain.Entities;

public class SalonService : BaseEntity
{
    public string? Name { get; set; }
    //public string? Description { get; set; }
    public string? Category { get; set; }
    public double Price { get; set; }
    public string? HaveTax { get; set; }
    public double Tax { get; set; } = 0;
    //public double Total { get; set; }
}

public record InputSalonServiceModel 
(
    int Id, string? Name, string? Category, double Price, string? HaveTax, double Tax
);

public record EditSalonServiceModel 
(
    int Id, string? Name, string? Category, double Price, string? HaveTax, double Tax
);
