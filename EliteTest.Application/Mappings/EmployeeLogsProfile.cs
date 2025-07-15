using AutoMapper;
using EliteTest.Application.DTO;
using EliteTest.Domain.Entities;


namespace EliteTest.Application.Mappings;

public class EmployeeLogsProfile : Profile
{
    public EmployeeLogsProfile()
    {

        CreateMap<EmployeeHistoryLog, EmployeeLogsDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee!.Name))
            .ForMember(dest => dest.EmployeeEmail, opt => opt.MapFrom(src => src.Employee!.Email))
            .ForMember(dest => dest.EmployeeHireDate, opt => opt.MapFrom(src => src.Employee!.HireDate))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Employee!.Department!.Name))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Employee!.Department!.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Employee!.Status.ToString()))
            .ReverseMap();


    }
}
