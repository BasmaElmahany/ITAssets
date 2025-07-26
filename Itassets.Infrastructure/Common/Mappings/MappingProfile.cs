using AutoMapper;
using Itasset.Application.DTOs;
using Itassets.Domain.Entities;
using Itassets.Infrastructure.Entities;

namespace Itassets.Infrastructure.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
       
            CreateMap<RegisterUserDto, ApplicationUser>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.DesiredRole, opt => opt.MapFrom(src => src.Role));


            CreateMap<BrandDTO, Brand>().ReverseMap();


            CreateMap<CategoryDTO, Category>().ReverseMap();

            CreateMap<OfficeDTO, Office>().ReverseMap();


            CreateMap<SupplierDTO, Supplier>().ReverseMap();

            CreateMap<EmployeeDTO, Employee>().ReverseMap();
            CreateMap<DeviceRequestsDTO, DeviceRequests>().ReverseMap();
        }
    }
}
