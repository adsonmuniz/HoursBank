using AutoMapper;
using HoursBank.CrossCutting.Mappings;

namespace HoursBank.Service.Test
{
    public abstract class BaseTestService
    {
        public IMapper Mapper { get; set; }

        public BaseTestService()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new DtoToEntityProfile());
                    cfg.AddProfile(new EntityToResponseProfile());
                });
                return config.CreateMapper();
            }

            public void Dispose()
            {
                
            }
        }
    }
}