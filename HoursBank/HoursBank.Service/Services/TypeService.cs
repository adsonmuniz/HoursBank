using AutoMapper;
using HoursBank.Domain.Entities;
using HoursBank.Domain.Interfaces;
using HoursBank.Domain.Interfaces.Services;
using HoursBank.Domain.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoursBank.Service.Services
{
    public class TypeService : ITypeService
    {
        private readonly IRepository<TypeEntity> _repository;
        private readonly IMapper _mapper;

        public TypeService(IRepository<TypeEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TypeResponse> Get(int id)
        {
            var result = await _repository.SelectAsync(id);
            if (result != null)
            {
                return _mapper.Map<TypeResponse>(result);
            }
            return null;
        }

        public async Task<IEnumerable<TypeResponse>> GetAll()
        {
            var result = await _repository.SelectAsync();
            if (result.Any())
            {
                return _mapper.Map<List<TypeResponse>>(result);
            }
            return null;
        }
    }
}
