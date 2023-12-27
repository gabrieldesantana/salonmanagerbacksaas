using System.ComponentModel.DataAnnotations;

namespace SalonManager.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string? Name { get; set; }
        public string? CNPJ { get; set; }
        List<User>? Owners { get; set; }
        List<User>? Employees { get; set; }
    }

    public record InputCompanyModel
    (
    string? Name, string? CNPJ
    );
    public record EditCompanyModel
    (
    string? Name, string? CNPJ
    );
}
