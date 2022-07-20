using HoursBank.Domain.Entities;
using System.Threading.Tasks;

namespace HoursBank.Domain.Interfaces.Repository
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> Login(string email, string password);
    }
}
