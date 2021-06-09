using API.Global;
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
        private static string conte = @"Data Source=ADMIN\HUUDANG;Initial Catalog=ShopDoCongNghe;Integrated Security=True";
        private static string conStr = @"Data Source=ADMIN\HUUDANG;Initial Catalog=ShopDoCongNghe;Integrated Security=True";

        //public static string ExcuteQuery(string query)
        //{
        //    OdbcConnection conn = new OdbcConnection(conStr);
        //    OdbcCommand cmd = new OdbcCommand(query, conn);
        //    OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        conn.Open();
        //        adapter.Fill(dt);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.ToString());
        //    }
        //    return JsonConvert.SerializeObject(dt);

        //}
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

        //public static string ExcuteQueryReead(string query,string str)
        //{
        //    OdbcConnection conn = new OdbcConnection(GlobalData.conStr);
        //    OdbcCommand cmd = new OdbcCommand(query, conn);
        //    OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    string result = "";
        //    try
        //    {
        //        conn.Open();
        //        adapter.Fill(dt);
        //        OdbcDataReader dRead = cmd.ExecuteReader();
        //        while (dRead.Read())
        //        {
        //            result = dRead[str].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.ToString());
        //    }
        //    return result;

        //}
        public static string ExcuteQueryReead(string query, string str)
        {
            SqlConnection conn = new SqlConnection(GlobalData.conStr);
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
        //public static List<string> ExcuteQueryListReead(string query, string str)
        //{
        //    OdbcConnection conn = new OdbcConnection(GlobalData.conStr);
        //    OdbcCommand cmd = new OdbcCommand(query, conn);
        //    OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    List<string> result = new List<string>();
        //    try
        //    {
        //        conn.Open();
        //        adapter.Fill(dt);
        //        OdbcDataReader dRead = cmd.ExecuteReader();
        //        while (dRead.Read())
        //        {

        //            result.Add(dRead[str].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.ToString());
        //    }
        //    return result;

        //}
        public static List<string> ExcuteQueryListReead(string query, string str)
        {
            SqlConnection conn = new SqlConnection(GlobalData.conStr);
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


        //public static void ExcuteNonquery(string querry)
        //{
        //    OdbcCommand cmd = new OdbcCommand(querry);

        //    using (OdbcConnection conn = new OdbcConnection(conStr))
        //    {
        //        try
        //        {

        //            cmd.Connection = conn;
        //            if (conn.State == ConnectionState.Closed)
        //            {
        //                conn.Open();
        //            }
        //            cmd.ExecuteNonQuery();


        //        }
        //        catch (Exception ex)
        //        {

        //            Console.WriteLine(ex.ToString());
        //        }
        //    }

        //}
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
