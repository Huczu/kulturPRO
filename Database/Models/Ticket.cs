using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Ticket
    {
        public long Id { get; set; }

        public string TypeName { get; set; }

        public double Price { get; set; }

        public double DiscountPercentage { get; set; }

        public bool IsDefaultEventPrice { get; set; }
    }
}
