namespace HoursBank.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public long Hours { get; set; }
        public bool Admin { get; set; }
        public bool Active { get; set; }
#nullable enable
        public int? TeamId { get; set; }
#nullable disable
    }
}
