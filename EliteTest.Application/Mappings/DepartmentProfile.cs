using AutoMapper;
using EliteTest.Application.Commands.Department;
using EliteTest.Application.DTO;
using EliteTest.Domain.Entities;


namespace EliteTest.Application.Mappings;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<CreateDepartmentCommand, Department>()
            .ConstructUsing(src=> new Department(src.name));

        CreateMap<Department, DepartmentDto>().ReverseMap();
    }
}
