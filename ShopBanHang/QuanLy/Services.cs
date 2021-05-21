using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            string r;
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
                        r = result;
                    }
                }
                return r;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}
