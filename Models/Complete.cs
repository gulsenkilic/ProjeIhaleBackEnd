using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Models
{
    public class Complete
    {
        public int  CompleteId { get; set; }
        public int TenderId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }

        public Tender Tender { get; set; }

    }
}
