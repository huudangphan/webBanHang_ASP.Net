
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace API
{
    public static class Execute
    {

        private static string conStr = @"workstation id=DBHuuDang.mssql.somee.com;packet size=4096;user id=huudang2412_SQLLogin_1;pwd=zz1bl999px;data source=DBHuuDang.mssql.somee.com;persist security info=False;initial catalog=DBHuuDang ";
        //private static string conStr = @"Data Source=ADMIN\HUUDANG;Initial Catalog=ShopDoCongNghe;Integrated Security=True";


        public static string ExcuteQuery(string query)
        {
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
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

        
        public static string ExcuteQueryReead(string query, string str)
        {
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            string result = "";
            try
            {
                conn.Open();
                adapter.Fill(dt);
                SqlDataReader dRead = cmd.ExecuteReader();
                while (dRead.Read())
                {
                    result = dRead[str].ToString();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return result;

        }
       
        public static List<string> ExcuteQueryListReead(string query, string str)
        {
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            List<string> result = new List<string>();
            try
            {
                conn.Open();
                adapter.Fill(dt);
                SqlDataReader dRead = cmd.ExecuteReader();
                while (dRead.Read())
                {

                    result.Add(dRead[str].ToString());
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return result;

        }


        
        public static void ExcuteNonquery(string querry)
        {
           SqlCommand cmd = new SqlCommand(querry);

            using (SqlConnection conn = new SqlConnection(conStr))
            {
                try
                {

                    cmd.Connection = conn;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());
                }
            }

        }

    }
}
