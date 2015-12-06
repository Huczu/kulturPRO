using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    [TrackChanges]
    [Description("Użytkownik")]
    public class User
    {
        [Key]
        [Description("Id")]
        public long Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [Description("Login")]
        public string Login { get; set; }

        [Required]
        [Description("Hasło")]
        public string Password { get; set; }

        [Required]
        [Description("Imię")]
        public string FirstName { get; set; }

        [Required]
        [Description("Nazwisko")]
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
