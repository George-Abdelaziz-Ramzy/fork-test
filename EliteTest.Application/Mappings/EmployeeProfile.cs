using AutoMapper;
using EliteTest.Application.Commands.Employee;
using EliteTest.Application.DTO;
using EliteTest.Domain.Entities;


namespace EliteTest.Application.Mappings;
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeCommand, Employee>()
            .ConstructUsing(src => new Employee(src.Name,src.Email , src.DepartmentId , src.HireDate));


        CreateMap<Employee, EmployeeDto>()
            .ReverseMap();
    }

}
