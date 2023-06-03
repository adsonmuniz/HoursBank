using HoursBank.Domain.Responses;
using System.Collections.Generic;

namespace HoursBank.Domain.Dtos
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CoordinatorDto> Coordinators { get; set; }
    }
}
