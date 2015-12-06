using System;
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
    public class Event
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Czas wydarzenia przechowywany jest w bazie po liczbie ticków, a aplikacji tłumaczony jest na czas (typ TimeSpan)
        /// </summary>
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
        public string ImagePath { get; set; }

        /// <summary>
        /// opis wydarzenia
        /// </summary>
        public string Description { get; set; }
    }
}
