namespace SalonManager.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string? Name { get; set; }
        public string? CNPJ { get; set; }
        public List<User>? Employees { get; set; }
    }

    public record InputCompanyModel
    (
    string? Name, string? CNPJ, string TenantId, int UserCreatorId
    );
    public record EditCompanyModel
    (
    string? Name, string? CNPJ, string TenantId
    );
}
