using SalonManager.Application.Helpers;
using SalonManager.Domain.Enums;

namespace SalonManager.Domain.Entities
{
    public class User : Person
    {
        public string? Login { get; set; } //
        public string? Email { get; set; } //
        public string? Password { get; set; } //
        public EUserRole Role { get; set; }

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
            string? Name, string? CPF, string? Login, string? Email, string? Password, EUserRole Role, int CompanyId
        );
    public record EditUserModel
        (
            string? Name, string? Email, string? Password
        );
}
