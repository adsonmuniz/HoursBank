using HoursBank.Domain.Dtos;
using HoursBank.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursBank.Domain.Interfaces.Services
{
    public interface IBankService
    {
        Task<IEnumerable<BankResponse>> Get(int id, DateTime? init, DateTime? end, bool? approved);
        Task<IEnumerable<BankResponse>> GetAll();
        Task<BankResponse> Post(BankDto bank);
        Task<BankResponse> Put(BankDto bank);
        Task<bool> Delete(int id);
    }
}
