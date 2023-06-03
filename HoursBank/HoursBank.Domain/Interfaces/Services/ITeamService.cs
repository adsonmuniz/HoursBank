using HoursBank.Domain.Dtos;
using HoursBank.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursBank.Domain.Interfaces.Services
{
    public interface ITeamService
    {
        Task<TeamResponse> Get(int id);
        Task<IEnumerable<TeamResponse>> GetAll();
        Task<IEnumerable<TeamResponse>> GetTeamsCoordinators();
        Task<TeamResponse> GetByName(string name);
        Task<TeamResponse> Post(TeamDto team);
        Task<TeamResponse> Put(TeamDto team);
        Task<bool> Delete(int id);
    }
}
