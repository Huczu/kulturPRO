using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using kulturPRO.ViewModels;
using AutoMapper;
using log4net;
namespace kulturPRO.Utils

{
    public class MapperConfig
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Config()
        {
            Mapper.CreateMap<User, UserViewModel>();
            //log.Error("hahaahha");
        }

    }
}
