using System;

namespace HoursBank.Domain.Responses
{
    public class BankResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Approved { get; set; }
        public DateTime? DateApproved { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public string UserName { get; set; }
        public string TeamName { get; set; }
    }
}
