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
        private readonly IRepository<CoordinatorEntity> _coordinatorRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public TeamService(
            IRepository<TeamEntity> repository,
            IRepository<CoordinatorEntity> coordinatorRepository,
            IRepository<UserEntity> userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _coordinatorRepository = coordinatorRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<TeamResponse> Get(int id)
        {
            var result = await _repository.SelectAsync(id);
            if (result != null)
            {
                var team = _mapper.Map<TeamResponse>(result);

                var coordinators = await _coordinatorRepository.SelectAsync();
                var users = await _userRepository.SelectAsync();

                var coords = coordinators.Where(c => c.TeamId == team.Id)
                                .Select(c => new CoordinatorDto
                                {
                                    Id = c.Id,
                                    UserId = c.UserId,
                                    TeamId = c.TeamId,
                                    UserName = users.Single(u => u.Id == c.UserId).Email
                                }).ToList();
                if (coords.Any())
                {
                    team.Coordinators = coords;
                }
                return team;
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

        public async Task<IEnumerable<TeamResponse>> GetTeamsCoordinators()
        {
            var response = new List<TeamResponse>();

            var teams = await _repository.SelectAsync();
            var coordinators = await _coordinatorRepository.SelectAsync();
            var users = await _userRepository.SelectAsync();
                
            return teams.Select(t => new TeamResponse
                   {
                        Id = t.Id,
                        Name = t.Name,
                        Coordinators = coordinators.Where(c => c.TeamId == t.Id)
                            .Select(c => new CoordinatorDto
                            {
                                Id = c.Id,
                                UserId = c.UserId,
                                TeamId = c.TeamId,
                                UserName = users.Single(u => u.Id == c.UserId).Email
                            }).ToList()
                   }).ToList();
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
                // Adiciona todos coordenadores que foram enviados da pagina
                var insertTasks = new List<Task>();
                foreach (var c in team.Coordinators)
                {
                    insertTasks.Add(_coordinatorRepository.InsertAsync(new CoordinatorEntity()
                    {
                        TeamId = result.Id,
                        UserId = c.UserId
                    }));
                }
                await Task.WhenAll(insertTasks);

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
                var coords = _mapper.Map<List<CoordinatorResponse>>(
                    (await _coordinatorRepository.SelectAsync()).ToList().Where(c => c.TeamId == result.Id));
                
                var tasks = new List<Task>();
                if (team.Coordinators.Count() == 0)
                {
                    foreach (var c in coords)
                    {
                        tasks.Add(_coordinatorRepository.DeleteAsync(c.Id));
                    }
                }
                else
                {
                    foreach (var coord in coords)
                    {
                        if (!team.Coordinators.Where(c => c.UserId == coord.UserId).Any())
                        {
                            tasks.Add(_coordinatorRepository.DeleteAsync(coord.Id));
                        }
                        else
                        {
                            // Encontrar o item correspondente na lista team.Coordinators
                            var matchingCoord = team.Coordinators.FirstOrDefault(c => c.UserId == coord.UserId);

                            // Remover o item correspondente da lista team.Coordinators
                            team.Coordinators.Remove(matchingCoord);
                        }
                    }

                    foreach (var coord in team.Coordinators)
                    {
                        tasks.Add(_coordinatorRepository.InsertAsync(new CoordinatorEntity()
                        {
                            TeamId = result.Id,
                            UserId = coord.UserId
                        }));
                    }
                }
                
                await Task.WhenAll(tasks);
                // Aguardar a conclusão de todas as tarefas
                await _coordinatorRepository.SaveChangesAsync();

                return _mapper.Map<TeamResponse>(result);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var users = await _userRepository.SelectAsync();
            var coordinators = await _coordinatorRepository.SelectAsync();
            if (users.Where(u => u.TeamId == id).Any() || coordinators.Where(c => c.TeamId == id).Any())
            {
                return false;
            }
            return await _repository.DeleteAsync(id);
        }
    }
}
