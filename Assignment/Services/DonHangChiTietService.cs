using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Interfaces;
using Assignment.Models;

namespace Assignment.Services
{
    public class DonHangChiTietService : IDonHangChiTietService
    {
        protected DataContext _context;
        public DonHangChiTietService(DataContext context)
        {
            _context = context;
        }
        public int AddDonHangChiTiet(DonHangChiTiet donHangChiTiet)
        {
            int ret = 0;
            try
            {
                _context.Add(donHangChiTiet);
                _context.SaveChanges();
                ret = donHangChiTiet.ChiTietID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
    }
}
