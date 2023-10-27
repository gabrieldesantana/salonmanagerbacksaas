using SalonManager.Domain.Enums;

namespace SalonManager.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public EUserRole Role { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }


        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }

    public record InputUserModel 
        (
            string? Name, string? CompanyName, EUserRole Role, string? Login, string? Email, string? Password
        );
    public record EditUserModel
        (
            string? Name, string? CompanyName, string? Email, string? Password
        );
}
