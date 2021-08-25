using ProjeIhale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Dtos
{
    public class BidForListDto
    {
        public int TenderId { get; set; }
        public int BidId { get; set; }
        public int UserId { get; set; }
        public int Bids { get; set; }
        public Tender Tender { get; set; }

    }
}
