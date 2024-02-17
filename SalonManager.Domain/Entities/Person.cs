namespace SalonManager.Domain.Entities
{
    public abstract class Person : BaseEntity
    {
        public string? Cpf { get; set; }
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
