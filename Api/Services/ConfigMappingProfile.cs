using AutoMapper;
using DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ConfigMappingProfile : Profile
    {
        public ConfigMappingProfile()
        {
            CreateMap<User, UsersDTO>();
            CreateMap<UsersDTO, User>();
            //CreateMap<List<User>, List<UsersDTO>>();
            //CreateMap<List<UsersDTO>, List<User>>();
        }
    }
}
