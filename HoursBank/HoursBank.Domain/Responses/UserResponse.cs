namespace HoursBank.Domain.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Hours { get; set; }
        public bool Admin { get; set; }
        public bool Active { get; set; }
#nullable enable
        public int? TeamId { get; set; }
#nullable disable
    }
}
