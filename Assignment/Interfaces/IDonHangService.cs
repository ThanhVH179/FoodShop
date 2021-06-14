using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Interfaces
{
    public interface IDonHangService
    {
        List<DonHang> GetDonHangAll();
        List<DonHang> GetDonHangByKhach(int KHId);
        DonHang GetDonHang(int id);
        int AddDonHang(DonHang donHang);
        int EditDonHang(int id, DonHang donHang);
    }
}
