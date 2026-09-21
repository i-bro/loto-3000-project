using LotoApp.DataAccess;
using LotoApp.DataAccess.Implementations;
using LotoApp.DataAccess.Interfaces;
using LotoApp.Services.Implementation;
using LotoApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LotoApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<LotoAppDbContext>(x => x.UseSqlServer("Server=.\\;Database=LotoAppDb;Trusted_Connection=True;TrustServerCertificate=True"));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IWinnerRepository, WinnerRepository>();
            services.AddScoped<IDrawRepository, DrawRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IDrawService, DrawService>();
            services.AddScoped<ISessionService, SessionService>();
        }
    }
}
