using AutoMapper;
using Bookstore.Application.Models.Employee;
using Bookstore.DataAccess.Entities;

namespace Bookstore.Application.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeRequestModel, Employee>()
                .ForMember(dest => dest.Id, options => options.Ignore())
                .ForMember(dest => dest.Role, options => options.Ignore());
            CreateMap<Employee, EmployeeResponseModel>()
                .ForMember(dest => dest.Role, options => options.MapFrom(source => source.Role.Name));
        }
    }
}
