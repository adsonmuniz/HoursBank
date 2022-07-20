using HoursBank.Domain.Interfaces.Services;
using HoursBank.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HoursBank.CrossCutting.DependencyInjection
{
    public class ConfigureServiceInjection
    {
        public static void ConfigureDependencyServices(IServiceCollection services)
        {
            // Injection
            services.AddTransient<IBankService, BankService>();
            services.AddTransient<ICoordinatorService, CoordinatorService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<ITypeService, TypeService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();
        }
    }
}
