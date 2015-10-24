using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
namespace kulturPRO.ViewModels
{
    
    public class UserViewModel
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public long Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }
        
        public string FullName
        {
            
            get
            {
                //log.Error("hehe");
                return FirstName + " " + Surname;
            }
        }
        
    }
}
