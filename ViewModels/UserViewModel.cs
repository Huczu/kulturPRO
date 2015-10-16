using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulturPRO.ViewModels
{
    public class UserViewModel
    {
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
                return FirstName + " " + Surname;
            }
        }
    }
}
