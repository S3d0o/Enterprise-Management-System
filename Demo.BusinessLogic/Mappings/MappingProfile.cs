using AutoMapper;
using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.DataAccess.Models.EmployeeModule;

namespace Demo.BusinessLogic.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest=>dest.Gender,option=>option.MapFrom(src=>src.Gender))
                .ForMember(d =>d.EmployeeType,o=>o.MapFrom(s=>s.EmployeeType));
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.Gender, option => option.MapFrom(src => src.Gender))
                .ForMember(d => d.EmployeeType, o => o.MapFrom(s => s.EmployeeType))
                .ForMember(d=>d.HiringDate,o=>o.MapFrom(s=> DateOnly.FromDateTime( s.HiringDate)));
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(d=>d.HiringDate,o=>o.MapFrom(s=>s.HiringDate.ToDateTime(TimeOnly.MinValue)));
            CreateMap<UpdatedEmployeeDto, Employee>()
                .ForMember(d => d.HiringDate, o => o.MapFrom(s => s.HiringDate.ToDateTime(TimeOnly.MinValue)));

        }

    }
}
