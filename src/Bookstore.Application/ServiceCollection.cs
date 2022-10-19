using Bookstore.Application.Mapping;
using Bookstore.Application.Services;
using Bookstore.Application.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Application
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(EmployeeProfile));

            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
