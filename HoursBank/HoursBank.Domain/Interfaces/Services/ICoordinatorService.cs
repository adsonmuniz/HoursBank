using HoursBank.Domain.Dtos;
using HoursBank.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursBank.Domain.Interfaces.Services
{
    public interface ICoordinatorService
    {
        Task<CoordinatorResponse> Get(int id);
        Task<IEnumerable<CoordinatorResponse>> GetByUser(int id);
        Task<IEnumerable<CoordinatorResponse>> GetByTeam(int id);
        Task<IEnumerable<CoordinatorResponse>> GetAll();
        Task<bool> Exists(int userId, int teamId);
        Task<CoordinatorResponse> Post(CoordinatorDto coordinator);
        Task<CoordinatorResponse> Put(CoordinatorDto coordinator);
        Task<bool> Delete(int id);
        Task<bool> DeleteByTeam(int teamId);
    }
}
