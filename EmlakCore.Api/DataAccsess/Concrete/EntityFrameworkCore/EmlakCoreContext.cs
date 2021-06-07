using EmlakCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.DataAccsess.Concrete.EntityFrameworkCore
{
    public class EmlakCoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer("data source=DESKTOP-K1HIF0J\\SQLEXPRESS;initial catalog=EmlakCore;integrated security=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblEmlakResimleri>()
               .HasKey(c => new { c.EmlakID, c.ResimID });
        }
        public DbSet<TblKullaniciler> TblKullaniciler { get; set; }
        public DbSet<TblMusteriler> TblMusteriler { get; set; }
        public DbSet<TblIsyeri> TblIsyeri { get; set; }
        public DbSet<TblKiralikEmlaklar> TblKiralikEmlaklar { get; set; }
        public DbSet<TblSatilikEmlaklar> TblSatilikEmlaklar { get; set; }
        public DbSet<TblYetkililer> TblYetkililer { get; set; }
        public DbSet<TblAdresler> TblAdresler { get; set; }
        public DbSet<TblEmlakTurleri> TblEmlakTurleri { get; set; }
        public DbSet<TblEmlaklar> TblEmlaklar { get; set; }
        public DbSet<TblEmlakResimleri> TblEmlakResimleri { get; set; }
        public DbSet<TblResimler> TblResimler { get; set; }

    }
}
