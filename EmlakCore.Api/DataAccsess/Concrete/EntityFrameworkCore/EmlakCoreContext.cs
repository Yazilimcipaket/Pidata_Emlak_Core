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

            optionsBuilder.UseSqlServer(@"workstation id=paketpatron.com;packet size=4096;user id=paketpat_Emlak;pwd=g8Hd9Aw3E;" +
                    "data source=paketpatron.com;persist security info=False;initial catalog=paketpat_Emlak;MultipleActiveResultSets=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmlakResimleri>()
               .HasKey(c => new { c.EmlakID, c.ResimID });
        }
        public DbSet<Kullaniciler> TblKullaniciler { get; set; }
        public DbSet<Musteriler> TblMusteriler { get; set; }
        public DbSet<Isyeri> TblIsyeri { get; set; }
        public DbSet<KiralikEmlaklar> TblKiralikEmlaklar { get; set; }
        public DbSet<SatilikEmlaklar> TblSatilikEmlaklar { get; set; }
        public DbSet<Yetkililer> TblYetkililer { get; set; }
        public DbSet<Adres> TblAdresler { get; set; }
        public DbSet<EmlakTurleri> TblEmlakTurleri { get; set; }
        public DbSet<Emlak> TblEmlaklar { get; set; }
        public DbSet<EmlakResimleri> TblEmlakResimleri { get; set; }
        public DbSet<Resimler> TblResimler { get; set; }

    }
}
