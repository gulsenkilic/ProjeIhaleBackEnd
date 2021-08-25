using Microsoft.EntityFrameworkCore;
using ProjeIhale.Models;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace ProjeIhale.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Complete> Completes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tender>().HasKey(lc => new { lc.TenderId });
            modelBuilder.Entity<Admin>().HasKey(lc => new { lc.AdminId });
            modelBuilder.Entity<User>().HasKey(lc => new {  lc.UserId});
            modelBuilder.Entity<Bid>().HasKey(lc => new { lc.BidId });
            modelBuilder.Entity<Complete>().HasKey(lc => new { lc.CompleteId });
        }
    }
}
