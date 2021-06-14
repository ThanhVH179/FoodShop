using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
  
        }
        public DbSet<MonAn> MonAns { get; set; }

        public DbSet<NguoiDung> NguoiDungs { get; set; }

        public DbSet<DonHang> DonHangs { get; set; }

        public DbSet<KhachHang> KhachHangs { get; set; }

        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
    }
}
