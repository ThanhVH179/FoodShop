using Assignment.Interfaces;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public class NguoiDungService : INguoiDungService
    {
        protected DataContext _context;
        protected IMaHoaHelper _maHoaHelper;
        public NguoiDungService(DataContext context, IMaHoaHelper maHoaHelper)
        {
            _context = context;
            _maHoaHelper = maHoaHelper;
        }
        public List<NguoiDung> GetNguoiDungAll()
        {
            List<NguoiDung> list = new List<NguoiDung>();
            list = _context.NguoiDungs.ToList();
            return list;
        }
        public NguoiDung GetNguoiDung(int id)
        {
            NguoiDung nguoidung = null;
            nguoidung = _context.NguoiDungs.Find(id); //cách này chỉ dùng cho Khóa chính
            //product = _context.Products.Where(e=>e.Id==id).FirstOrDefault(); //cách tổng quát
            return nguoidung;
        }
        public int AddNguoiDung(NguoiDung nguoidung)
        {
            int ret = 0;
            try
            {
                nguoidung.Password = _maHoaHelper.Mahoa(nguoidung.Password);
                _context.Add(nguoidung);
                _context.SaveChanges();
                ret = nguoidung.NguoiDungID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public int EditNguoiDung(int id, NguoiDung nguoidung)
        {
            int ret = 0;
            try
            {
                NguoiDung _nguoidung = null;
                _nguoidung = _context.NguoiDungs.Find(id); //cách này chỉ dùng cho Khóa chính

                _nguoidung.UserName = nguoidung.UserName;
                _nguoidung.FullName = nguoidung.FullName;
                _nguoidung.Title = nguoidung.Title;
                _nguoidung.BirthDay = nguoidung.BirthDay;
                _nguoidung.Email = nguoidung.Email;
                _nguoidung.Admin = nguoidung.Admin;
                _nguoidung.Locked = nguoidung.Locked;
                if (nguoidung.Password != null)
                {
                    nguoidung.Password = _maHoaHelper.Mahoa(nguoidung.Password);
                    _nguoidung.Password = nguoidung.Password;
                    _nguoidung.ConfirmPass = nguoidung.ConfirmPass;
                }
                _context.Update(_nguoidung);
                _context.SaveChanges();
                ret = nguoidung.NguoiDungID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public NguoiDung Login(ViewLogin login)
        {
            var u = _context.NguoiDungs.Where(
                p => p.UserName.Equals(login.UserName) 
                && p.Password.Equals(_maHoaHelper.Mahoa(login.Password))
                ).FirstOrDefault();
            return u;
        }
    }
}

