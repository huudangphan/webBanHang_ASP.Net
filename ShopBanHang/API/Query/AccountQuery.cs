using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using API.Global;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace API.Query
{
    public class AccountQuery
    {
        string conStr = GlobalData.conStr;
        DataTable dt;
        private string ExcuteQuery(string query)
        {
            OdbcConnection conn = new OdbcConnection(conStr);
            OdbcCommand cmd = new OdbcCommand(query, conn);
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
            dt = new DataTable();
            try
            {
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return JsonConvert.SerializeObject(dt);

        }
        private void ExcuteNonquery(string querry)
        {
            OdbcCommand cmd = new OdbcCommand(querry);
            OdbcDataAdapter adapter = new OdbcDataAdapter(querry, conStr);
            using (OdbcConnection conn = new OdbcConnection(conStr))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public string getAccount()
        {
            string querry = "select * from Admin";
            return ExcuteQuery(querry);
        }
        public string loginAdmin(string username,string password,int role)
        {
            string querry = "select userAdmin from Admin where userAdmin='" + username + "' and passAdmin='" + password + "' and Loai='"+role+"'";
            return ExcuteQuery(querry);
        }
        public string loginBanHang(string username, string password, int role)
        {
            string querry = "select userAdmin from Admin where userAdmin='" + username + "' and passAdmin='" + password + "' and Loai='" + role + "'";
            return ExcuteQuery(querry);
        }
        public string loginKho(string username, string password, int role)
        {
            string querry = "select userAdmin from Admin where userAdmin='" + username + "' and passAdmin='" + password + "' and Loai='" + role + "'";
            return ExcuteQuery(querry);
        }
    }
}
