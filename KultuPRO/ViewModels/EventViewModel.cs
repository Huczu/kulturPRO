using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KulturPRO.ViewModels
{
    class EventViewModel
    {
            public long Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public DateTime Date { get; set; }

            public string ImagePath { get; set; }

            public string Description { get; set; }
    }
}
