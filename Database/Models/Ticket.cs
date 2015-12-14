using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    [TrackChanges]
    [Description("Bilet")]
    public class Ticket
    {
        [Description("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Description("Typ biletu")]
        public string TypeName { get; set; }

        [Description("Cena")]
        public double Price { get; set; }

        [Description("Procent zniżki")]
        public double DiscountPercentage { get; set; }

        [Description("Czy domyślny dla wydarzenia")]
        public bool IsDefaultEventPrice { get; set; }

        public ICollection<Event> EventsUsingTicket { get; set; } 

        public string TypeNameAndPrice
        {
            get { return TypeName + ", " + Price; }
        }
    }
}
