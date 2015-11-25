using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Database.Models
{
    public class Seat
    {
        [Key]
        public long id { get; set; }
        public Seat()
            {
                this.Status= "1";
            }
        
        public string Status { get; set; }
        
        

        //public long PricePointId { get; set; }
        //[ForeignKey("PricePointId")]
        //public PricePoint PricePoint { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public int Column { get; set; }

        public long CinemaHallId { get; set; }
        [ForeignKey("CinemaHallId")]
        public virtual CinemaHall CinemaHall { get; set; }

    }
}
