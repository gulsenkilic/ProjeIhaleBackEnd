using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Models
{
    public class User
    {
        public User()
        {
            Bids = new List<Bid>();
           
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public List<Bid> Bids;
        
    }
}
