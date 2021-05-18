using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Query
{
    public class NhanVienQuery
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
    }
}
