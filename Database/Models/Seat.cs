using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Seat
    {
        [Key]
        public long id { get; set; }
        public long SeatStatusId { get; set; }
        
        [ForeignKey("SeatStatusId")]
        public SeatStatus SeatStatus { get; set; }

        public long PricePointId { get; set; }
        [ForeignKey("PricePointId")]
        public PricePoint PricePoint { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public int Column { get; set; }

    }
}
