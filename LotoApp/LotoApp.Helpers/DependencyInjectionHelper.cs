using LotoApp.DataAccess;
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
    }
}
