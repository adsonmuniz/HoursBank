using System;

namespace HoursBank.Domain.Dtos
{
    public class BankDto
    {
        public int? Id { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool? Approved { get; set; }
        public int? UserId { get; set; }
        public int? TypeId { get; set; }
        public string Description { get; set; }
    }
}
