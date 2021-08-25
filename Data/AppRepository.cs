using LinqToDB.Data;
using ProjeIhale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjeIhale.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context; //injection yapımı

        public AppRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void UpDate<T>(T entity) where T : class
        {
            var ent= _context.Entry(entity);
            ent.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Bid> GetBidsByUser(int UserId)
        {
            var bids = _context.Bids.Where(p => p.UserId == UserId).Include(p=>p.Tender).ToList();
            return bids;
        }
       

        public List<Complete> GetCompletes() //buraya bir bak
        {
            var completes = _context.Completes.Include(p=>p.Tender).ToList();  //tamamlanış ihaleler  gelsin
            return completes; 
        }

        public List<Complete> GetCompletesByUser(int UserId)
        {
            var completes = _context.Completes.Where(p => p.UserId== UserId).Include(p=>p.Tender).ToList();
            return completes;
        }

      
        public List<Bid> GetBidsByTender(int TenderId)
        {
            var bids = _context.Bids.Include(p=>p.Tender).Where(p => p.TenderId == TenderId).ToList();
            return bids;
        }
        public Tender GetTenderById(int tenderID)
        {
            var tender = _context.Tenders.FirstOrDefault(c => c.TenderId== tenderID);

            return tender;
        }
        
        public List<Tender> GetTenders()
        {
            var tenders = _context.Tenders.Where(p=>p.Status =="Aktif").ToList(); //ihaleleri fotolarıyla birlikte listeleme
            return tenders;
        }

        public List<Tender> GetTendersByAdminId(int adminId)
        {
            var tenders = _context.Tenders.Where(p => p.AdminId == adminId).ToList();
            return tenders;
        }

        public List<Tender> GetTendersByStatus()
        {
            var tenders = _context.Tenders.Where(p => p.Status == "Iptal").ToList();
            return tenders;
        }
       
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

      
    }
}
