using System;

namespace HoursBank.Domain.Entities
{
    public class BankEntity : BaseEntity
    {
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Approved { get; set; }
        public DateTime? DateApproved { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
    }
}
