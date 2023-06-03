using HoursBank.Domain.Dtos;
using HoursBank.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursBank.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponse> Get(int id);
        Task<IEnumerable<UserResponse>> GetAll();
        Task<IEnumerable<UserResponse>> GetUsersToApprove();
        Task<UserResponse> GetByEmail(string email);
        Task<IEnumerable<UserResponse>> GetByTeam(int id);
        Task<bool> IsAdmin(int id);
        Task<UserResponse> Post(UserDto user);
        Task<UserResponse> Put(UserDto user);
        Task<UserResponse> Approve(UserDto user);
        Task<bool> Delete(int id);
    }
}
