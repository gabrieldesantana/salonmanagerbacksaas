namespace SalonManager.Domain.Entities
{
    public class BaseEntity
    {

        public BaseEntity()
        {
            CreatedAt= DateTime.Now;
            Actived = true;
        }
        public string TenantId { get; set; }
        public int UserCreatorId { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Actived { get; set; }
    }
}
