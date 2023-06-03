using AutoMapper;
using HoursBank.Domain.Dtos;
using HoursBank.Domain.Entities;
using HoursBank.Domain.Interfaces;
using HoursBank.Domain.Interfaces.Services;
using HoursBank.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoursBank.Service.Services
{
    public class CoordinatorService : ICoordinatorService
    {
        private readonly IRepository<CoordinatorEntity> _repository;
        private readonly IMapper _mapper;

        public CoordinatorService(IRepository<CoordinatorEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CoordinatorResponse> Get(int id)
        {
            var result = await _repository.SelectAsync(id);
            if (result != null)
            {
                return _mapper.Map<CoordinatorResponse>(result);
            }
            return null;
        }

        public async Task<IEnumerable<CoordinatorResponse>> GetAll()
        {
            var result = await _repository.SelectAsync();
            if (result.Any())
            {
                return _mapper.Map<List<CoordinatorResponse>>(result);
            }
            return null;
        }

        public async Task<IEnumerable<CoordinatorResponse>> GetByUser(int id)
        {
            var coordinators = await _repository.SelectAsync();
            if (coordinators.Any())
            {
                return _mapper.Map<List<CoordinatorResponse>>(coordinators.Where(c => c.UserId == id).ToList());
            }
            return null;
        }

        public async Task<bool> Exists(int userId, int teamId)
        {
            var coordinators = await _repository.SelectAsync();
            if (coordinators.Any())
            {
                return coordinators.Where(c => c.UserId == userId && c.TeamId == teamId).Any();
            }
            return false;
        }

        public async Task<IEnumerable<CoordinatorResponse>> GetByTeam(int id)
        {
            var coordinators = await _repository.SelectAsync();
            return _mapper.Map<List<CoordinatorResponse>>(coordinators.Where(c => c.TeamId == id).ToList());
        }

        public async Task<CoordinatorResponse> Post(CoordinatorDto coordinator)
        {
            var entity = _mapper.Map<CoordinatorEntity>(coordinator);
            var result = await _repository.InsertAsync(entity);
            if (result != null)
            {
                return _mapper.Map<CoordinatorResponse>(result);
            }
            return null;
        }

        public async Task<CoordinatorResponse> Put(CoordinatorDto coordinator)
        {
            var coordinatorOld = await _repository.SelectAsync(coordinator.Id);
            coordinatorOld.TeamId = coordinator.TeamId;
            
            var result = await _repository.UpdateAsync(coordinatorOld);
            if (result != null)
            {
                return _mapper.Map<CoordinatorResponse>(result);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> DeleteByTeam(int teamId)
        {
            try
            {
                IEnumerable<CoordinatorResponse> list = await this.GetByTeam(teamId);
                foreach (var coordinator in list)
                {
                    await _repository.DeleteAsync(coordinator.Id);
                }
                return true;

            }
            catch(Exception)
            { 
                return false;
            }
            
        }
    }
}
