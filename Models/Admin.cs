using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Models
{
    public class Admin
    {
        public Admin()
        {
            Tenders = new List<Tender>();
        }

        public int AdminId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public List <Tender> Tenders { get; set; }

    }
}
