using ProjeIhale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Dtos
{
    public class CompleteForListDto
    {
        public int CompleteId { get; set; }
        public int TenderId { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public Tender Tender { get; set; }
    }
}
