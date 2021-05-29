using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Query
{
    public class SanPhamQuery
    {
        public string getAllSP()
        {
            string query = "select * from SanPham";
            return Execute.ExcuteQuery(query);
                 
        }
        public string getSP(string tensp)
        {
            string query = "select * from SanPham where tenSP like '" + tensp + "%'";
            return Execute.ExcuteQuery(query);
        }
        public string getHang()
        {
            string query = "select * from Hang ";
            return Execute.ExcuteQuery(query);

        }
        public string GetLoaiSP()
        {
            string query = "select * from Loai ";
            return Execute.ExcuteQuery(query);
        }


    }
}
