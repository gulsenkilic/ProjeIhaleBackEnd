using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Dtos
{
    public class TenderForListDtos
    {
        public int TenderId { get; set; }
        public string Description { get; set; }

        public string Category { get; set; }
        public string Url { get; set; }
        public Decimal Price { get; set; }


    }
}
