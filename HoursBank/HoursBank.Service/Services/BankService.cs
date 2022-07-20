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
    public class BankService : IBankService
    {
        private readonly IRepository<BankEntity> _repository;
        private readonly IMapper _mapper;

        public BankService(IRepository<BankEntity> repository, IMapper mapper)
        {
            _repository = repository;
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

        public async Task<IEnumerable<BankResponse>> Get(int id, DateTime? init, DateTime? end, bool? approved)
        {
            var banks = await _repository.SelectAsync();
            if (banks.Any())
            {
                var entity = banks.Where(c => c.UserId == id
                    && (!init.HasValue || c.Start >= init.Value)
                    && (!end.HasValue || c.End <= end.Value)
                    && (!approved.HasValue || c.Approved == approved.Value)
                    ).ToList();
                return _mapper.Map<List<BankResponse>>(entity);
            }
            return null;
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
            var bankOld = await _repository.SelectAsync(bank.Id);

            bankOld.Start = bank.Start.Value;
            bankOld.End = bank.End.Value;
            bankOld.Approved = false; // to do - descontar caso aprovada
            bankOld.Description = bank.Description;
            bankOld.TypeId = bank.TypeId;
                
            var result = await _repository.UpdateAsync(bankOld);
            if (result != null)
            {
                return _mapper.Map<BankResponse>(result);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
