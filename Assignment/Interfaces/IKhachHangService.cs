using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Interfaces
{
    public interface IKhachHangService
    {
        List<KhachHang> GetKhachHangAll();
        KhachHang GetKhachHang(int id);
        int AddKhachHang(KhachHang khachHang);
        int EditKhachHang(int id, KhachHang khachHang);
        KhachHang Login(ViewWebLogin viewWebLogin);
    }
}
