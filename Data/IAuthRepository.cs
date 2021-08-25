using ProjeIhale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeIhale.Data
{
   public interface IAuthRepository
    {
        Task<User> RegisterUser(User user, string password);
        Task<User> ChangePassUser(User user, string password);
       
        Task<User> LoginUser(string userName, string password);
        Task<bool> UserExists(string userName);

        Task<Admin> RegisterAdmin(Admin user, string password);
        Task<Admin> LoginAdmin(string userName, string password);
        Task<Admin> ChangePassAdmin(Admin user, string password);
        Task<bool> AdminExists(string userName);
    }
}
