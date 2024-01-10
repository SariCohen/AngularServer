using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;
//using System.Data.Entity;


namespace AngularServer1.DAL
{
    public class ChiniesSaleContext:DbContext
    {
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<present> Presents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Winner> Winners { get; set; }
        public DbSet<Donation> Donations { get; set; }
       
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-E0FAPSB\\SQLEXPRESS;Initial Catalog=MyChiniesSale;Integrated Security=True");



        //}

        //protected override void OnModelCreating(DbContextOptionsBuilder options)
        //{
        //    options.Entity<Donor>()
        //        .HasMany(d => d.Donations)
        //        .WithRequired(d => d.Donor)
        //        .HasForeignKey(d => d.DonorId)
        //        .WillCascadeOnDelete(true);

        //    base.OnModelCreating(modelBuilder);
        //}
        public ChiniesSaleContext(DbContextOptions<ChiniesSaleContext> options) : base(options)
        {

        }
      
        }


    
}
