using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using kulturPRO.ViewModels;
using AutoMapper;

namespace kulturPRO.Utils
{
    public class MapperConfig
    {
        public static void Config()
        {
            Mapper.CreateMap<User, UserViewModel>();
        }
    }
}
