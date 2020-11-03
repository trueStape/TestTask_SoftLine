using AutoMapper;
using TestTaskSoftline.BLL.Models;
using TestTaskSoftline.DAL.Entities;

namespace TestTaskSoftLine.Options
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>();
        }
    }
}