using AutoMapper;
using CRUD_Design.Models;
using CRUD_Design.Models.DBModel;
using Sportsmeter_frontend.Models;

namespace CRUD_Design.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ApplicationUser, ApiUserDto>().ReverseMap();
            CreateMap<RunInfo, CreateRunInfoDTO>().ReverseMap();
            CreateMap<RunInfo, GenericRunInfoDTO>().ReverseMap();
            CreateMap<RunInfo, UpdateInfoDTO>().ReverseMap();
        }
    }
}
