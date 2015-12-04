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

    [TrackChanges]
    [Description("Miejsce")]
    public class Seat
    {
        [Key]
        [Description("Id")]
        public long Id { get; set; }

        [Description("Status miejsca")]
        public SeatState State { get; set; }

        [Required]
        [Description("Rząd")]
        public int Row { get; set; }

        [Required]
        [Description("Kolumna")]
        public int Column { get; set; }

        [Description("Id sali")]
        public long CinemaHallId { get; set; }
        [ForeignKey("CinemaHallId")]
        public virtual CinemaHall CinemaHall { get; set; }

    }
}
