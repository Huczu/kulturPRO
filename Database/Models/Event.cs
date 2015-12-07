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
    /// <summary>
    /// model wydarzenia
    /// </summary>
    [TrackChanges]
    [Description("Wydarzenie")]
    public class Event
    {
        [Key]
        [Description("Id")]
        public long Id { get; set; }

        [Required]
        [Description("Nazwa")]
        public string Name { get; set; }

        [Required]
        [Description("Data")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Czas wydarzenia przechowywany jest w bazie po liczbie ticków, a aplikacji tłumaczony jest na czas (typ TimeSpan)
        /// </summary>

        [Description("Czas trwania")]
        public Int64 TimeSpanTicks { get; set; }
        [NotMapped]
        public TimeSpan Time
        {
            get { return TimeSpan.FromTicks(TimeSpanTicks); }
            set { TimeSpanTicks = value.Ticks; }
        }

        /// <summary>
        /// ścieżka do obrazka
        /// </summary>
        [Description("Scieżka do obrazu")]
        public string ImagePath { get; set; }

        /// <summary>
        /// opis wydarzenia
        /// </summary>
        [Description("Opis")]
        public string Description { get; set; }

        [Description("Id domyślnego biletu")]
        public long DefaultTicketId { get; set; }

        [ForeignKey("DefaultTicketId")]
        public Ticket DefaultTicket { get; set; }

        [Description("Id sali")]
        public long CinemaHallId { get; set; }

        [ForeignKey("CinemaHallId")]
        public CinemaHall CinemaHall { get; set; }

        public string NameWithDate
        {
            get
            {
                return Name + " " + Date.AddTicks(TimeSpanTicks);
            }
        }
    }
}
