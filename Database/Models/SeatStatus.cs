using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class SeatStatus
    {
        [Key]
        public  long Id { get; set; }

        [Required]
        public  string Status { get; set; }

        
    }
}
