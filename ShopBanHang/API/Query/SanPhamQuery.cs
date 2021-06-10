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
            string query = "Select CTTonKho.MaSp,SanPham.tenSP,CTTonKho.SL from SanPham, CTTonKho where CTTonKho.MaSP=SanPham.maSP and MaKho=" + makho;
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
        public string xemCTkho(int makho)
        {
            string query = "select * from CTTonKho where MaKho = " + makho;
            return Execute.ExcuteQuery(query);


        }
        public void themphieunhap(string ngaynhap,string makho)
        {
            string query = "insert into HDNhapSP(ngayNhap,maKho) values('" + ngaynhap + "'," + makho + ")";
            Execute.ExcuteNonquery(query);
        }
        public string getmapn()
        {
            string query = "select max(maPhieuNhap) as maphieu from HDNhapSP";
            return Execute.ExcuteQueryReead(query, "maphieu");
        }
        public void themctphieunhap(string masp,string slnhap,string gianhap,string thanhtien)
        {
            int maph = Int32.Parse(getmapn());
            string queryKho="select maKho from HDNhapSP where maPhieuNhap=" + maph;
            string makho = Execute.ExcuteQueryReead(queryKho, "maKho");
            string queryUpdate = "Update  CTTonKho  set SL+="+slnhap+" where MaKho = "+makho+" and MaSP = "+masp;
            Execute.ExcuteNonquery(queryUpdate);
            string query = "insert into CTPN(maPhieuNhap,maSP,SLNhap,giaNhap,thanhTien) values("+maph+"," + masp + "," + slnhap + "," + gianhap + "," + thanhtien + ")";
            Execute.ExcuteNonquery(query);

        }
        public void SuaGia(string masp,string giasp)
        {
            string query = "update SanPham set giaSP = "+giasp+" where maSP = "+masp;
            Execute.ExcuteNonquery(query);


        }

    }
}
