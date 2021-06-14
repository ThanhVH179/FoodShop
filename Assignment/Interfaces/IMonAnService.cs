using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Interfaces
{
    public interface IMonAnService
    {
        List<MonAn> GetMonAnAll();
        public MonAn GetMonAn(int id);
        public int AddMonAn(MonAn monAn);
        public int EditMonAn(int id, MonAn monAn);
    }
}
