using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Models
{
    public class Tender
    {
        public Tender()
        {
            Bids = new List<Bid>();
        }

        public int TenderId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
         public string Url { get; set; }
        
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int AdminId{ get; set; }
        public string Description2 { get; set; }
        public List<Bid> Bids { get; set; }
        public Admin Admin { get; set; }
        public Complete Complete { get; set; }
    }
}
