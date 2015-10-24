using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using KulturPRO.ViewModels;
using AutoMapper;

namespace kulturPRO.Utillities
{
    public class MapperConfig
    {
        public static void Config()
        {
            Mapper.CreateMap<User, UserViewModel>();
        }
    }
}
