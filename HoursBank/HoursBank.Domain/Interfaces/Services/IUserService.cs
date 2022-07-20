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
        Task<UserResponse> GetByEmail(string email);
        Task<IEnumerable<UserResponse>> GetByTeam(int id);
        Task<UserResponse> Post(UserDto user);
        Task<UserResponse> Put(UserDto user);
        Task<bool> Delete(int id);
    }
}
