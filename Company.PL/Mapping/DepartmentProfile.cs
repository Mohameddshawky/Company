using AutoMapper;
using Company.DAL.Models;
using Company.PL.DTos;

namespace Company.PL.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Departments>().ReverseMap();
        }

        
    }
}
