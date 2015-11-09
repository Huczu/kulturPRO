using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    [TableAttribute("CinemaHall")]
    public class CinemaHall
    {
        [Key]
        public int CinemaHallId { get; set; }
        public string CinemaHallName { get; set; }
    }
}
