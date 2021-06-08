using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Query
{
    public class AdminQuery
    {
        public string GetNhanVien()
        {
            string query = "select * from Admin where Loai=2 or Loai =3";
            return Execute.ExcuteQuery(query);
        }
        public void UpdateNhanVien(string username,string password,string tenAdmin,int loai)
        {
            string query = "Update Admin set passAdmin= '" + password + "',tenAdmin='"+tenAdmin+"',Loai="+loai+" where userAdmin='" + username + "'";
            Execute.ExcuteNonquery(query);
        }
        public void InsertNhanVien(string username,string password,string tennhanvien,int loai)
        {
            string query = "insert into Admin(userAdmin,passAdmin,tenAdmin,Loai) values('" + username + "','" + password + "','" + tennhanvien + "'," + loai + ")";
            Execute.ExcuteNonquery(query);
        }
        public string DoanhThuOnline(string thang,string nam)
        {
            string query = "select sum(CTHDOnline.thanhTien) as doanhThuOnline from CTHDOnline where CTHDOnline.MaHD in(select hd.MaHD from HDOnline hd where YEAR(hd.NgayGiao)= "+nam+" and MONTH(hd.NgayGiao)= "+thang+")";
            return Execute.ExcuteQuery(query);
            
        }
        public string DoanhThuOffline(string thang, string nam)
        {
            string query = "select sum(CTHDOff.thanhTien) as doanhthuoff from CTHDOff where CTHDOff.MaHD in(select hd.MaHD from HDOffLine hd where YEAR(hd.NgayMua)= "+nam+" and MONTH(hd.NgayMua)= "+thang+")";

            return Execute.ExcuteQuery(query);

        }
        public string DoanhThuTG(string thang, string nam)
        {
            string query = "select sum(HDTraGop.TienCoc) as doanhthuTG from HDTraGop where YEAR(HDTraGop.NgayCoc) = "+nam+" and MONTH(HDTraGop.NgayCoc)= "+thang+"";


            return Execute.ExcuteQuery(query);

        }
        public string DoanhThuTGHangThang(string thang, string nam)
        {
            string query = "select sum(tg.TienDong) as tientragop from PhieuTraGop tg where YEAR(tg.NgayTra) = "+nam+" and MONTH(tg.NgayTra)= "+thang+"";

            return Execute.ExcuteQuery(query);

        }
        public string DoanhThuPhat(string thang, string nam)
        {
            string query = "select sum(tg.TienPhat) as tientragop from PhieuTraGop tg where YEAR(tg.NgayTra) = " + nam + " and MONTH(tg.NgayTra)= " + thang + "";

            return Execute.ExcuteQuery(query);

        }
        public string TongChi(string thang, string nam)
        {
            string query = "select sum(pn.thanhTien) as tongChi from CTPN pn where pn.maPhieuNhap in (select hd.maPhieuNhap from HDNhapSP hd where year(hd.ngayNhap)= "+nam+" and MONTH(hd.ngayNhap)= "+thang+")";


            return Execute.ExcuteQuery(query);

        }
        public string GetPass(string password)
        {
            string query="select * from Admin where userAdmin='Admin' and passAdmin='"+password+"'";
            return Execute.ExcuteQuery(query);
        }
        public void DoiMatKhau(string password)
        {
            string query = "Update Admin set passAdmin='" + password + "' where userAdmin='Admin'";
            Execute.ExcuteNonquery(query);
        }
    }
}
