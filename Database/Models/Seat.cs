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
    public enum SeatState
    {
        NotExists = 0,
        Free = 1,
        Taken = 2
    }

    public class Seat
    {
        [Key]
        public long Id { get; set; }

        public SeatState State { get; set; }

        [Required]
        public int Row { get; set; }
        [Required]
        public int Column { get; set; }

        public long CinemaHallId { get; set; }
        [ForeignKey("CinemaHallId")]
        public virtual CinemaHall CinemaHall { get; set; }

    }
}
