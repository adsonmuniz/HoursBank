using HoursBank.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursBank.Domain.Interfaces.Services
{
    public interface ITypeService
    {
        Task<TypeResponse> Get(int id);
        Task<IEnumerable<TypeResponse>> GetAll();
    }
}
