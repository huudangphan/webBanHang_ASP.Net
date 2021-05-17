using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Query.DonHangQuery
{
    public class DHTGQuery
    {
        public string DHTG(int makh)
        {
            string query = "select * from HDTraGop where MaKH=" + makh;
            return Execute.ExcuteQuery(query);
        }
        public void TaoHDTG(int makh,double tienCoc,int SoThang)
        {
            try
            {
                int LaiSuat = 0;
                string date = DateTime.Now.ToString();
                if (SoThang == 3)
                {
                    LaiSuat = 3;
                }
                else
                    LaiSuat = 2;
                string query = "insert into HDTraGop(MaKH,NgayCoc,TienCoc,SoThang,laiSuat)  values (" + makh + ",'" + date + "'," + tienCoc + "," + SoThang + "," + LaiSuat + ") " +
                    "";
                Execute.ExcuteNonquery(query);
                
                int mahd = /*Int32.Parse(getmahd());*/48;
                int day = DateTime.Now.Day;
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int j = 0;
                for (int i = 1; i <= SoThang; i++)
                {
                    string time = "";
                    j = month + i;
                    if (j > 12)
                    {
                        year = 2022;
                        int count = SoThang - (12 - month);
                        for (int a = 1; a <= count; a++)
                        {
                            time = year + "-" + a + "-" + day;
                            if (thang30(a) && day == 31)
                            {
                                time = year + "-" + a + "-30";
                            }
                            if (a == 2 && day > 29)
                            {
                                time = year + "-" + a + "-28";
                            }
                            string q = "insert into PhieuTraGop(MaHD,NgayTra,NgayDenHan,Ki,MaMucPhat,TienDong,TienPhat)" +
                                "values(" + mahd + ",''," + time + "," + i + ",1," + 1 + ",0)";
                            Execute.ExcuteNonquery(q);
                        }
                    }
                    else
                    {
                        time = year + "-" + j + "-" + day;
                        if (thang30(j) && day == 31)
                        {
                            time = year + "-" + j + "-30";
                        }
                        if (j == 2 && day > 29)
                        {
                            time = year + "-" + j + "-28";
                        }
                        string q = "insert into PhieuTraGop(MaHD,NgayTra,NgayDenHan,Ki,MaMucPhat,TienDong,TienPhat)" +
                                "values(" + mahd + ",'','" + time + "'," + i + ",1," + 1 + ",0)";
                        Execute.ExcuteNonquery(q);
                    }
                }
               
            }
            catch
            {

            }
        }
        public bool thang30(int month)
        {
            int[] m = new int[] { 4, 6, 9, 11 };
            for (int i = 0; i < m.Length; i++)
            {
                if (month == m[i])
                    return true;
            }
            return false;
        }
        public string tongTien()
        {
            string query = "select sum(thanhTien) as tongtien from CTHDTG where MaHD=" + getmahd();
            return Execute.ExcuteQueryReead(query, "tongtien");
        }

        public string getmahd()
        {
            string date = DateTime.Now.ToString();
            string query = "select MAX(MaHD) from HDTraGop where NgayCoc='" + date + "'";
            return Execute.ExcuteQueryReead(query, "MaHD");
        }
        public void TaoCTTG(int makho, int mahd, int masp, int sl, double giaban)
        {
            string query = "insert into CTHDTG(MaKho,MaSP,MaHD,SL,GiaBan) values(" + makho + "," + masp + "," + mahd + "," + sl + "," + giaban + ")";
            Execute.ExcuteNonquery(query);
        }
        public void TaoPhieuTraGop(int mahd,int sothang,double tienDong)
        {

        }
    }
}
