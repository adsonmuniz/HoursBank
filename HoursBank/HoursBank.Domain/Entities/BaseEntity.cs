using System;
using System.ComponentModel.DataAnnotations;

namespace HoursBank.Domain.Entities
{
    public class BaseEntity
    {
        [KeyAttribute]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
