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
        public long Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
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
