using Bookstore.DataAccess.Persistence;
using Bookstore.DataAccess.Repositories;
using Bookstore.DataAccess.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.DataAccess
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(configuration["Database:ConnectionString"]);
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IDistributorRepository, DistributorRepository>();

            return services;
        }
    }
}
