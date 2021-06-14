using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Interfaces
{
    public interface INguoiDungService
    {
        List<NguoiDung> GetNguoiDungAll();
        NguoiDung GetNguoiDung(int id);
        int AddNguoiDung(NguoiDung nguoiDung);
        int EditNguoiDung(int id, NguoiDung nguoiDung);
        public NguoiDung Login(ViewLogin login);
    }
}
