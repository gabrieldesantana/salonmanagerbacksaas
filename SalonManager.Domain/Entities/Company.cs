namespace SalonManager.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string? Name { get; set; }
        public string? CNPJ { get; set; }
        public List<Employee>? Employees { get; set; }
    }

    public record InputCompanyModel
    (
    string? Name, string? CNPJ
    );
    public record EditCompanyModel
    (
    string? Name, string? CNPJ, string TenantId
    );
}
