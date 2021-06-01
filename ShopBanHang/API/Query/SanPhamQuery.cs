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
        public void ThemSP(string maHang,string maLoai,string tenSP,string anh,string giaSP,string MoTa)
        {

            string query = "insert into SanPham(maHang,maLoai,tenSP,anh,giaSP,Mota) values("+maHang+","+maLoai+","+tenSP+","+anh+","+giaSP+","+MoTa+") ";
            Execute.ExcuteNonquery(query);
        }
        public string ChiTietKho(string makho)
        {
            string query = "Select MaSp,tenSP,SL from SanPham, CTTonKho where MaKho=" + makho;
            return Execute.ExcuteQuery(query);
        }
        public void ThemKho(int makho,int sl)
        {
            string masp = getMaxSP();
            string query = "insert into CTTonKho(MaKho,MaSP,SL) values(" + makho + "," + masp + "," + sl + ")";
            Execute.ExcuteNonquery(query);
        }
        public string getMaxSP()
        {
            string query = "select max(MaSP) as masp from SanPham";
            return Execute.ExcuteQueryReead(query, "masp");
        }


    }
}
