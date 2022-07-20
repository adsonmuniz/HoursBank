using HoursBank.Data.Context;
using HoursBank.Data.Repository;
using HoursBank.Domain.Entities;
using HoursBank.Domain.Interfaces.Repository;
using HoursBank.Util;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HoursBank.Data.Implementations
{
    public class UserImplementation : Repository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataset;

        public UserImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<UserEntity>();
        }

        public async Task<UserEntity> Login(string email, string password)
        {
            var pass = BHCrypto.Encode(password);
            return await _dataset.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(pass));
        }
    }
}
