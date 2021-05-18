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
        public void UpdateNhanVien(string username,string password)
        {
            string query = "Update Admin set passAdmin= '" + password + "' where userAdmin='" + username + "'";
            Execute.ExcuteNonquery(query);
        }
    }
}
