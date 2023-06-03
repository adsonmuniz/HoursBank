using AutoMapper;
using HoursBank.Domain.Dtos;
using HoursBank.Domain.Entities;
using HoursBank.Domain.Interfaces;
using HoursBank.Domain.Interfaces.Services;
using HoursBank.Domain.Responses;
using HoursBank.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoursBank.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Get(int id)
        {
            var result = await _repository.SelectAsync(id);
            if (result != null)
            {
                result.Password = BHCrypto.Decode(result.Password);
                return _mapper.Map<UserResponse>(result);
            }
            return null;
        }

        public async Task<bool> IsAdmin(int id)
        {
            var result = await _repository.SelectAsync(id);
            if (result != null)
            {
                return result.Admin;
            }
            return false;
        }

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            var result = await _repository.SelectAsync();
            if (result.Any())
            {
                var listResult = _mapper.Map<List<UserResponse>>(result);
                listResult.ForEach(r => { r.Password = ""; });
                return listResult;
            }
            return null;
        }

        public async Task<IEnumerable<UserResponse>> GetUsersToApprove()
        {
            var result = await _repository.SelectAsync();
            if (result.Any())
            {
                var listResult = _mapper.Map<List<UserResponse>>(result.Where(u => !u.Active.HasValue));
                listResult.ForEach(r => { r.Password = ""; });
                return listResult;
            }
            return null;
        }

        public async Task<UserResponse> GetByEmail(string email)
        {
            var users = await _repository.SelectAsync();
            if (users.Any())
            {
                var result = users.FirstOrDefault(c => c.Email == email);
                result.Password = BHCrypto.Decode(result.Password);
                return _mapper.Map<UserResponse>(result);
            }
            return null;
        }

        public async Task<IEnumerable<UserResponse>> GetByTeam(int id)
        {
            var users = await _repository.SelectAsync();
            if (users.Any())
            {
                var result = users.Where(c => c.TeamId == id).ToList();
                result.ForEach(r =>
                {
                    r.Password = BHCrypto.Decode(r.Password);
                });
                return _mapper.Map<List<UserResponse>>(result);
            }
            return null;
        }

        public async Task<UserResponse> Post(UserDto user)
        {
            user.Password = BHCrypto.Encode(user.Password);
            var entity = _mapper.Map<UserEntity>(user);
            var result = await _repository.InsertAsync(entity);
            if (result != null)
            {
                result.Password = BHCrypto.Decode(result.Password);
                return _mapper.Map<UserResponse>(result);
            }
            return null;
        }

        public async Task<UserResponse> Put(UserDto user)
        {
            if (user.Id != 1)
            {
                var userOld = await _repository.SelectAsync(user.Id);

                if (!string.IsNullOrEmpty(user.Password) && !user.Password.Equals(BHCrypto.Decode(userOld.Password)))
                {
                    userOld.Password = BHCrypto.Encode(user.Password);
                }

                userOld.Name = user.Name;
                userOld.ClientId = user.ClientId;
                userOld.ClientSecret = user.ClientSecret;
                userOld.Email = user.Email;
                userOld.TeamId = user.TeamId.HasValue ? user.TeamId : userOld.TeamId;
                userOld.Active = user.Active;

                var result = await _repository.UpdateAsync(userOld);
                if (result != null)
                {
                    result.Password = BHCrypto.Decode(result.Password);
                    return _mapper.Map<UserResponse>(result);
                }
            }
            return null;
        }

        public async Task<UserResponse> Approve(UserDto user)
        {
            var userOld = await _repository.SelectAsync(user.Id);

            userOld.Active = user.Active;
            if (user.Active.HasValue && user.Active.Value)
            {
                userOld.TeamId = user.TeamId.Value;
            }


            var result = await _repository.UpdateAsync(userOld);
            if (result != null)
            {
                result.Password = BHCrypto.Decode(result.Password);
                return _mapper.Map<UserResponse>(result);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            if (id != 1)
            {
                var user = await this.Get(id);
                if (user != null && (!user.Active.HasValue || !user.Active.Value)) {
                    return await _repository.DeleteAsync(id);
                }
            }
            return false;
        }
    }
}
