using Microsoft.EntityFrameworkCore;
using StudentManager.Models;

namespace StudentManager.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<HocSinh> HocSinh { get; set; }
        public DbSet<LopHoc> LopHoc { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Preferences.Get("DATABASE_PATH", "");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HocSinh>().Property(h => h.Mssv).HasColumnName("MSSV");
            modelBuilder.Entity<HocSinh>().Property(h => h.HoTen).HasColumnName("TEN");
            modelBuilder.Entity<HocSinh>().Property(h => h.NgaySinh).HasColumnName("NGSINH");
            modelBuilder.Entity<HocSinh>().Property(h => h.MaLop).HasColumnName("MALOP");

            modelBuilder.Entity<LopHoc>().Property(l => l.MaLop).HasColumnName("MALOP");
            modelBuilder.Entity<LopHoc>().Property(l => l.TenLop).HasColumnName("TEN");
        }
    }
}
