using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    [TableAttribute("SoldPlaces")]
    public class SoldPlaces
    {
        [Key]
        public int PlaceId { get; set; }
        public int PlaceNumber { get; set; }
        public int Hall { get; set; }
        public int ShowTime { get; set; }
        public int Movie { get; set; }
    }
}
