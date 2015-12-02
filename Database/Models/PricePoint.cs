using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class PricePoint
    {
        [Key]
        public virtual long Id { get; set; }

        [Required]
        public virtual string Type { get; set; }

        [Required]
        public virtual float Price { get; set; }

        
    }
}
