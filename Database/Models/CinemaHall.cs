using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    //[TableAttribute("CinemaHall")]
    public class CinemaHall
    {
        [Key]
        public long CinemaHallId { get; set; }
        public string CinemaHallName { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
