using HoursBank.Data.Context;
using HoursBank.Data.Implementations;
using HoursBank.Data.Repository;
using HoursBank.Domain.Interfaces;
using HoursBank.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HoursBank.CrossCutting.DependencyInjection
{
    public class ConfigureRepositoryInjection
    {
        public static void ConfigureDependencyRepository(IServiceCollection services)
        {
            services.AddDbContext<MyContext>(
                options => options.UseSqlServer("Server=hoursbank.c0tllc9aptlh.sa-east-1.rds.amazonaws.com;Database=HoursBank;Uid=root;Pwd=*Bh2023db")
                //options => options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=HoursBank;Uid=root;Pwd=Asdfg123")
            );
            // Injection
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserImplementation>();
        }
    }
}
