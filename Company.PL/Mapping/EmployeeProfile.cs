using AutoMapper;
using Company.DAL.Models;
using Company.PL.DTos;

namespace Company.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
            //CreateMap<EmployeeDTO, Employee>().ForMember(d=>d.Name,
            //    d=>d.MapFrom(s=>s.Name));
        }

        
    }
}
