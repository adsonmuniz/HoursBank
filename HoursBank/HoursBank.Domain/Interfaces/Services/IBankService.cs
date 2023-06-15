using HoursBank.Domain.Dtos;
using HoursBank.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursBank.Domain.Interfaces.Services
{
    public interface IBankService
    {
        Task<BankResponse> Get(int id);
        Task<IEnumerable<BankResponse>> Get(BankDto bank);
        Task<IEnumerable<BankResponse>> GetAll();
        Task<IEnumerable<BankResponse>> GetByCoordinator(int id);
        Task<BankResponse> Post(BankDto bank);
        Task<BankResponse> Put(BankDto bank);
        Task<bool> Delete(int id);
    }
}
