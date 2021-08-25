using ProjeIhale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Dtos
{
    public class TenderForDetailDto
    {
        public int TenderId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public Decimal Price { get; set; }
        public string Description2 { get; set; }
        public string Url { get; set; }
    }
}
