using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Query.DonHangQuery
{
    public class DHOfflineQuery
    {
        public void TaoDHOffline(int maKH)
        {
            string date = DateTime.Now.ToString();
            string query = "insert into HDOffline(MaKH,NgayMua) values('" + maKH + "','" + date + "')";
            Execute.ExcuteNonquery(query);

        }
        public string getHDOffline()
        {
            string query = "select * from HDOffline";
            return Execute.ExcuteQuery(query);
        }
        public void  TaoCTDOff(int makho, int mahd,int masp,int sl,double giaban)
        {
            string query = "insert into CTHDOff(MaKho,MaSP,MaHD,SL,GiaBan) values(" + makho + "," + masp + "," + mahd + "," + sl + "," + giaban + ")";
            Execute.ExcuteNonquery(query);
        }
        public string HDOff(int maKH)
        {
            string query = "select * from HDOffline where MaHD=" + maKH;
            return Execute.ExcuteQuery(query);
        }
       
        public string GetLastDH()
        {

            string query = "select MaHD from HDOffline order by MaHD asc";
            return Execute.ExcuteQueryReead(query, "MaHD");
        }
       public string getCTHDOff(int mahd)
        {
            string query = "select SanPham.tenSP,CTHDOff.SL,CTHDOff.GiaBan,CTHDOff.thanhTien,CTHDOff.thanhTien,CTHDOff.MaKho from SanPham, CTHDOff where CTHDOff.MaSP = SanPham.maSP and CTHDOff.MaHD =" + mahd;

            return Execute.ExcuteQuery(query);
        }
      
    }
}
