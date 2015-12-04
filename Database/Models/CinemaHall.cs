using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    [TrackChanges]
    public class CinemaHall
    {
        [Key]
        [Description("Id")]
        public long Id { get; set; }

        [Description("Nazwa")]
        public string Name { get; set; }

        [Description("Ilość rzędów")]
        public int MaxRows { get; set; }

        [Description("Ilość kolumn")]
        public int MaxColumns { get; set; }
    }
}
