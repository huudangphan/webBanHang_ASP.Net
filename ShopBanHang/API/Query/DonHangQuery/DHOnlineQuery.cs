using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.Query.DonHangQuery
{
    public class DHOnlineQuery
    {
       
        public string GetHDOnline()
        {
            string query = "select * from  HDOnline";
            return Execute.ExcuteQuery(query);
        }
        public string GetCTHDOnline(int maHD)
        {
            string query = "select * from  HDOnline where MaHD='"+maHD+"'";
            return Execute.ExcuteQuery(query);
        }
        public int GuiHangOnline(int maDH,int maKho)
        {
            if(checkdh(maDH)==true)
            {
                foreach (var item in listSP(maDH))
                {
                    string query = "update CTTonKho set SL-= (select SL from CTHDOnline ct where ct.MaKho ='"+maKho+"' and ct.MaSP ='"+item+"'   ) where MaKho = '"+maKho+"' and MaSP = '"+item+"'";
                    Execute.ExcuteNonquery(query);
                }
                
               
            }
            // kiểm tra đơn hàng đã được giao chưa
            return 0;
            
        }
        private bool checkdh(int madh)
        {
            string query = "select TinhTrang from HDOnline where TinhTrang = False and MaHD='" + madh + "'";
            if (Execute.ExcuteQuery(query) != "[]")
                return false;
            return true;
        }
        public List<string> listSP(int MaHD)
        {
            string query = "select MaSP from CTHDOnline where MaHD='"+MaHD+"'";

            return Execute.ExcuteQueryListReead(query, "MaSP");

        }
    }
}
