using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QuanLy
{
    public class Services
    {
        public static void POST(string urlbase,string token)
        {
            try
            {
                string strUrl = String.Format(urlbase);
                WebRequest request = WebRequest.Create(strUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + token);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }
        public static void PUT(string urlbase, string token)
        {
            try
            {
                string strUrl = String.Format(urlbase);
                WebRequest request = WebRequest.Create(strUrl);
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + token);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void DELETE(string urlbase, string token)
        {
            try
            {
                string strUrl = String.Format(urlbase);
                WebRequest request = WebRequest.Create(strUrl);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + token);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static string GET(string urlbase, string token)
        {
            #region a
            //string r;
            //try
            //{
            //    string strUrl = String.Format(urlbase);
            //    WebRequest request = WebRequest.Create(strUrl);
            //    request.Method = "GET";
            //    //request.ContentType = "application/json";
            //    request.Headers.Add("Authorization", "Bearer " + token);
            //    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            //    {
            //        var response = request.GetResponse();
            //        using (var streamReader = new StreamReader(response.GetResponseStream()))
            //        {
            //            var result = streamReader.ReadToEnd();
            //            r = result;
            //        }
            //    }

            //    return r;
            #endregion
            string jsonString = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlbase);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
            request.Accept = "/";
            request.UseDefaultCredentials = true;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.ContentType = "application/x-www-form-urlencoded";

            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            jsonString = sr.ReadToEnd();
            sr.Close();
            return jsonString;
        }
            
        

    }
}
