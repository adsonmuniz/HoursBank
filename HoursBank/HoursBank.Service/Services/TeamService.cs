using AutoMapper;
using HoursBank.Domain.Dtos;
using HoursBank.Domain.Entities;
using HoursBank.Domain.Interfaces;
using HoursBank.Domain.Interfaces.Services;
using HoursBank.Domain.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoursBank.Service.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<TeamEntity> _repository;
        private readonly IMapper _mapper;

        public TeamService(IRepository<TeamEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TeamResponse> Get(int id)
        {
            var result = await _repository.SelectAsync(id);
            if (result != null)
            {
                return _mapper.Map<TeamResponse>(result);
            }
            return null;
        }

        public async Task<IEnumerable<TeamResponse>> GetAll()
        {
            var result = await _repository.SelectAsync();
            if (result.Any())
            {
                return _mapper.Map<List<TeamResponse>>(result);
            }
            return null;
        }

        public async Task<TeamResponse> GetByName(string name)
        {
            var teams = await _repository.SelectAsync();
            if (teams.Any())
            {
                return _mapper.Map<TeamResponse>(teams.FirstOrDefault(c => c.Name == name));
            }
            return null;
        }

        public async Task<TeamResponse> Post(TeamDto team)
        {
            var entity = _mapper.Map<TeamEntity>(team);
            var result = await _repository.InsertAsync(entity);
            if (result != null)
            {
                return _mapper.Map<TeamResponse>(result);
            }
            return null;
        }

        public async Task<TeamResponse> Put(TeamDto team)
        {
            var teamOld = await _repository.SelectAsync(team.Id);
            teamOld.Name = team.Name;

            var result = await _repository.UpdateAsync(teamOld);
            if (result != null)
            {
                return _mapper.Map<TeamResponse>(result);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
