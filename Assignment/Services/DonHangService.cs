using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Interfaces;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class DonHangService : IDonHangService
    {
        protected DataContext _context;
        public DonHangService(DataContext context)
        {
            _context = context;
        }
        public List<DonHang> GetDonHangAll() 
        {
            List<DonHang> list = new List<DonHang>();
            list = _context.DonHangs.OrderByDescending(h => h.OrderDay)
                    .Include(h => h.KhachHang)
                    .Include(h => h.DonHangChiTiets).ToList();
            return list;
        }
        public List<DonHang> GetDonHangByKhach(int KHId)
        {
            List<DonHang> list = new List<DonHang>();
            list = _context.DonHangs.Where(h => h.KhachHangID == KHId).OrderByDescending(h => h.OrderDay)
                    .Include(h => h.KhachHang)
                    .Include(h => h.DonHangChiTiets).ToList();
            return list;
        }
        public DonHang GetDonHang(int id)
        {
            DonHang donHang = null;
            donHang = _context.DonHangs.Where(h => h.DonHangID == id)
                    .Include(h => h.KhachHang)
                    .Include(h => h.DonHangChiTiets).ThenInclude(y => y.MonAn)
                    .FirstOrDefault(); ;
            return donHang;
        }
        public int AddDonHang(DonHang donHang)
        {
            int ret = 0;
            try
            {
                _context.Add(donHang);
                _context.SaveChanges();
                ret = donHang.DonHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public int EditDonHang(int id, DonHang donHang)
        {
            int ret = 0;
            try
            {
                _context.Update(donHang);
                _context.SaveChanges();
                ret = donHang.DonHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
    }
}
