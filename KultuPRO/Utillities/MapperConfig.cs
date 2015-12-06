using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using KulturPRO.Models;
using KulturPRO.ViewModels;
using AutoMapper;
using TrackerEnabledDbContext.Common.Models;

namespace KulturPRO.Utillities
{
    public class MapperConfig
    {
        public static void Config()
        {
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<AuditLogDetail, AuditDetails>()
                .ForMember(o => o.PropertyName, 
                    o => o.MapFrom(
                        src =>
                            ((DescriptionAttribute)Type.GetType(src.Log.TypeFullName + ", Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
                                .GetProperty(src.PropertyName).GetCustomAttributes(typeof(DescriptionAttribute), true)[0]).Description));
        }
    }
}
