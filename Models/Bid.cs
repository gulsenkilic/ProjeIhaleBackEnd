using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Models
{
    public class Bid
    {
       public Bid()
        {
         
        }
        public int BidId { get; set; }
        public int TenderId { get; set; }
        public int  UserId { get; set; }
        public decimal Bids { get; set; }
        public User User { get; set; }
        public Tender Tender { get; set; }
    }
}



