﻿using AutoMapper;
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
    public class BankService : IBankService
    {
        private readonly IRepository<BankEntity> _repository;
        private readonly IRepository<CoordinatorEntity> _coordinatorRepository;
        private readonly IRepository<TeamEntity> _teamRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public BankService(
            IRepository<BankEntity> repository,
            IRepository<CoordinatorEntity> coordinatorRepository,
            IRepository<TeamEntity> teamRepository,
            IRepository<UserEntity> userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _coordinatorRepository = coordinatorRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankResponse>> GetAll()
        {
            var result = await _repository.SelectAsync();
            if (result.Any())
            {
                return _mapper.Map<List<BankResponse>>(result);
            }
            return null;
        }

        public async Task<BankResponse> Get(int id)
        {
            var bank = await _repository.SelectAsync(id);
            if (bank != null)
            {
                BankResponse bankResponse = _mapper.Map<BankResponse>(bank);

                var user = await _userRepository.SelectAsync(bank.UserId);
                var team = await _teamRepository.SelectAsync(user.TeamId.Value);
                bankResponse.UserName = user.Name;
                bankResponse.TeamName = team.Name;
                return bankResponse;
            }
            return null;
        }

        public async Task<IEnumerable<BankResponse>> Get(BankDto bank)
        {
            var banks = await _repository.SelectAsync();
            if (banks.Any())
            {
                var entity = banks.Where(c => (!bank.UserId.HasValue || c.UserId == bank.UserId.Value)
                    && (!bank.Id.HasValue || c.Id == bank.Id.Value)
                    && (!bank.Start.HasValue || c.Start >= bank.Start.Value)
                    && (!bank.End.HasValue || c.End <= bank.End.Value)
                    && (!bank.Approved.HasValue || c.Approved == bank.Approved.Value)
                    && (!bank.TypeId.HasValue || c.TypeId == bank.TypeId.Value)
                    ).ToList();
                return _mapper.Map<List<BankResponse>>(entity);
            }
            return null;
        }
        public async Task<IEnumerable<BankResponse>> GetByCoordinator(int id)
        {
            // Buscar os times relacionados ao coordenador pelo userId
            var coordinators = await _coordinatorRepository.SelectAsync();
            List<int> teamsId = new List<int>();
            if (coordinators.Any())
            {
                teamsId = coordinators.Where(t => t.UserId == id).Select(t => t.TeamId).ToList();
            }
            // Buscar todos os usuarios relacionados aos times pelo teamId
            var users = await _userRepository.SelectAsync();
            List<int> usersId = new List<int>();
            if (users.Any())
            {
                teamsId.ForEach(t =>
                {
                    var uIds = users.Where(u => u.TeamId == t).Select(u => u.Id);
                    if (uIds.Any()) { usersId.AddRange(uIds); }
                });

            }
            // Buscar todos os registros de banco relacionado aos usuarios
            var banks = await _repository.SelectAsync();
            if (banks.Any())
            {
                banks = banks.Where(b => usersId.Contains(b.UserId));
            }

            var banksResponse = _mapper.Map<List<BankResponse>>(banks);

            foreach (var bank in banksResponse)
            {
                var user = await _userRepository.SelectAsync(bank.UserId);
                var team = await _teamRepository.SelectAsync(user.TeamId.Value);
                bank.UserName = user.Name.Split(" ")[0] + $" <{user.Email}>";
                bank.TeamName = team.Name;
            }

            return banksResponse;
        }

        public async Task<BankResponse> Post(BankDto bank)
        {
            var entity = _mapper.Map<BankEntity>(bank);
            var result = await _repository.InsertAsync(entity);
            if (result != null)
            {
                return _mapper.Map<BankResponse>(result);
            }
            return null;
        }

        public async Task<BankResponse> Put(BankDto bank)
        {
            if (bank != null && bank.Id.HasValue)
            {
                var bankOld = await _repository.SelectAsync(bank.Id.Value);

                //bankOld.Start = bank.Start.Value;
                //bankOld.End = bank.End.Value;
                //bankOld.Description = bank.Description;
                //bankOld.TypeId = bank.TypeId.Value;
                bankOld.Approved = bank.Approved.Value;
                bankOld.DateApproved = DateTime.Now;

                var result = await _repository.UpdateAsync(bankOld);
                if (result != null)
                {
                    if (bank.Approved.Value)
                    {
                        var user = await _userRepository.SelectAsync(bankOld.UserId);
                        var totalMinutes = (long) TimeSpan.FromTicks(bankOld.End.Ticks - bankOld.Start.Ticks).TotalMinutes;
                        user.Hours += (bankOld.TypeId == 1) 
                            ? totalMinutes 
                            : (-1)*(totalMinutes);
                        await _userRepository.UpdateAsync(user);
                    }
                    return _mapper.Map<BankResponse>(result);
                }
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
