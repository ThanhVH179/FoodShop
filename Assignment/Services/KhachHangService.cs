using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Interfaces;
using Assignment.Models;

namespace Assignment.Services
{
    public class KhachHangService : IKhachHangService
    {
        protected DataContext _context;
        protected IMaHoaHelper _maHoaHelper;
        public KhachHangService(DataContext context, IMaHoaHelper maHoaHelper)
        {
            _context = context;
            _maHoaHelper = maHoaHelper;
        }
        public List<KhachHang> GetKhachHangAll()
        {
            List<KhachHang> list = new List<KhachHang>();
            list = _context.KhachHangs.ToList();
            return list;
        }
        public KhachHang GetKhachHang(int id)
        {
            KhachHang khachHang = null;
            khachHang = _context.KhachHangs.Find(id);
            return khachHang;
        }
        public int AddKhachHang(KhachHang khachHang)
        {
            int ret = 0;
            try
            {
                khachHang.Password = _maHoaHelper.Mahoa(khachHang.Password);
                khachHang.ConfirmPassword = khachHang.Password;
                _context.Add(khachHang);
                _context.SaveChanges();
                ret = khachHang.KhachHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public int EditKhachHang(int id, KhachHang khachHang)
        {
            int ret = 0;
            try
            {
                KhachHang _kh = null;
                _kh = _context.KhachHangs.Find(id);

                _kh.EmailKH = khachHang.EmailKH;
                _kh.FullName = khachHang.FullName;
                _kh.BirthDay = khachHang.BirthDay;
                _kh.PhoneNumber = khachHang.PhoneNumber;
                _kh.Address = khachHang.Address;
                if (khachHang.Password != null)
                {
                    khachHang.Password = _maHoaHelper.Mahoa(khachHang.Password);
                    _kh.Password = khachHang.Password;
                    _kh.ConfirmPassword = khachHang.Password;
                }
                _kh.Describe = khachHang.Describe;

                _context.Update(_kh);
                _context.SaveChanges();
                ret = _kh.KhachHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public KhachHang Login(ViewWebLogin viewWebLogin)
        {
            var u = _context.KhachHangs.Where(p => p.EmailKH.Equals(viewWebLogin.Email) 
                    && p.Password.Equals(_maHoaHelper.Mahoa(viewWebLogin.Password))
                    ).FirstOrDefault();
            return u;
        }
    }
}
