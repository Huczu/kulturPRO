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
        public long Id { get; set; }
        public string Name { get; set; }
        public int MaxRows { get; set; }
        public int MaxColumns { get; set; }
    }
}
