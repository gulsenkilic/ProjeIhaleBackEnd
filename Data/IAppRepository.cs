using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjeIhale.Models;

namespace ProjeIhale.Data
{
   public interface IAppRepository
    {
        void Add<T>(T entity) where T : class; //veri tabanına ekleme *
        void Delete<T>(T entity) where T : class; //veri tabanından silme *
        bool SaveAll();  //kaydet 
        void UpDate<T>(T entity) where T : class;
       
        List<Tender> GetTenders(); // **
        List<Tender> GetTendersByStatus(); //ihalenin durumuna göre listeleme **
        
        List<Tender> GetTendersByAdminId(int adminId);
        List<Bid> GetBidsByUser(int UserId); //kullanıcı verdiği teklifleri listeleyebilir 
        List<Bid> GetBidsByTender(int TenderId); //ihalelere göre verilen teklifler listelenebilir **
        List<Complete> GetCompletesByUser(int UserId); //kullanıcı kazandığı ihaleleri görüntüler
      
        List<Complete> GetCompletes(); //tamamlanmış ihaleler **
      
        Tender GetTenderById(int tenderID); //tek ihaleye ulaşma; **

        
    }
}
