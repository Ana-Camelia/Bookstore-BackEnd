using Bookstore.Application.Mapping;
using Bookstore.Application.Models.Employee;
using Bookstore.Application.Services;
using Bookstore.Application.Services.Implementations;
using Bookstore.Application.Validators;
using FluentValidation;
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
            services.AddScoped<IDistributorService, DistributorService>();

            services.AddScoped<AbstractValidator<EmployeeRequestModel>, EmployeeRequestModelValidator>();

            return services;
        }
    }
}
