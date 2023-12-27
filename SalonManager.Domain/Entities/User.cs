using SalonManager.Application.Helpers;
using SalonManager.Domain.Enums;

namespace SalonManager.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? CPF { get; set; }

        public Company? Company { get; set; }
        public int CompanyId { get; set; }

        public EUserRole Role { get; set; }

        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public bool ValidPassword(string password)
        {
            return Password == password.GenerateHash();
        }

        public void SetPasswordHash(string password)
        {
            Password = password.GenerateHash();
        }

        public void SetCreatorId()
        { 
            UserCreatorId = Id;
        }

    }

    public record InputUserModel 
        (
            string? Name, int CompanyId, EUserRole Role, string? Login, string? Email, string? Password
        );
    public record EditUserModel
        (
            string? Name, string? CompanyName, string? Email, string? Password
        );
}
