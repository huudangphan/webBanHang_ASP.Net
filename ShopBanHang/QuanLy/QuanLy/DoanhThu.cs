using DevExpress.XtraBars;
using Newtonsoft.Json;
using QuanLy.Model;
using QuanLy.Model.DoanhThu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;


namespace QuanLy.QuanLy
{
    public partial class DoanhThu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public DoanhThu(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
         
        }
        public void loadData()
        {
            string month = dateTimePicker1.Value.Month.ToString();
            string year = dateTimePicker1.Value.Year.ToString();
            loadDataOnline(month, year);
            loadDataOffline(month, year);
            loadDataTG(month, year);
            loadDataPhieu(month, year);
            loadDataPhat(month, year);
            loadDataChi(month, year);
        }
        public void loadDataOnline(string month,string year)
        {
            string baseURL = "http://apidnh.somee.com/api/Admin/DoanhThuOnline?thang=" + month+"&nam="+year;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    //wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    string url = string.Format(baseURL);
                    WebRequest request = WebRequest.Create(url);
                    request.Headers.Add("Authorization", "Bearer " + sess.token);
                    request.ContentType = "application/json";
                    //request.Method = "GET";
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var obj = JsonConvert.DeserializeObject<JArray>(result).ToObject<List<JObject>>().FirstOrDefault();
                        //txtOnline.Text = (string)obj["doanhThuOnline"];
                        txtOnline.Text = String.Format("{0:0,0}", (string)obj["doanhThuOnline"]);


                        //var data = (JObject)JsonConvert.DeserializeObject(result);
                        ////JObject
                        //txtOnline.Text = data["doanhThuOnline"].Value<int>().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void loadDataOffline(string month, string year)
        {
            string baseURL = "http://apidnh.somee.com/api/Admin/DoanhThuOffline?thang=" + month + "&nam=" + year;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    //wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    string url = string.Format(baseURL);
                    WebRequest request = WebRequest.Create(url);
                    request.Headers.Add("Authorization", "Bearer " + sess.token);
                    request.ContentType = "application/json";
                    //request.Method = "GET";
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var obj = JsonConvert.DeserializeObject<JArray>(result).ToObject<List<JObject>>().FirstOrDefault();
                        txtOff.Text = (string)obj["doanhthuoff"];


                        //var data = (JObject)JsonConvert.DeserializeObject(result);
                        ////JObject
                        //txtOnline.Text = data["doanhThuOnline"].Value<int>().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void loadDataTG(string month, string year)
        {
            //DoanhThuTG
            string baseURL = "http://apidnh.somee.com/api/Admin/DoanhThuTG?nam=" + month + "&thang=" + year;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    //wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    string url = string.Format(baseURL);
                    WebRequest request = WebRequest.Create(url);
                    request.Headers.Add("Authorization", "Bearer " + sess.token);
                    request.ContentType = "application/json";
                    //request.Method = "GET";
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var obj = JsonConvert.DeserializeObject<JArray>(result).ToObject<List<JObject>>().FirstOrDefault();
                        txttg.Text = (string)obj["doanhthuTG"];


                        //var data = (JObject)JsonConvert.DeserializeObject(result);
                        ////JObject
                        //txtOnline.Text = data["doanhThuOnline"].Value<int>().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void loadDataPhieu(string month, string year)
        {
            string baseURL = "http://apidnh.somee.com/api/Admin/DoanhThuTGThang?nam=" + month + "&thang=" + year;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    //wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    string url = string.Format(baseURL);
                    WebRequest request = WebRequest.Create(url);
                    request.Headers.Add("Authorization", "Bearer " + sess.token);
                    request.ContentType = "application/json";
                    //request.Method = "GET";
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var obj = JsonConvert.DeserializeObject<JArray>(result).ToObject<List<JObject>>().FirstOrDefault();
                        txtthang.Text = (string)obj["tientragop"];


                        //var data = (JObject)JsonConvert.DeserializeObject(result);
                        ////JObject
                        //txtOnline.Text = data["doanhThuOnline"].Value<int>().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void loadDataPhat(string month, string year)
        {
            string baseURL = "http://apidnh.somee.com/api/Admin/DoanhThuPhat?nam=" + month + "&thang=" + year;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    //wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    string url = string.Format(baseURL);
                    WebRequest request = WebRequest.Create(url);
                    request.Headers.Add("Authorization", "Bearer " + sess.token);
                    request.ContentType = "application/json";
                    //request.Method = "GET";
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var obj = JsonConvert.DeserializeObject<JArray>(result).ToObject<List<JObject>>().FirstOrDefault();
                        txtphat.Text = (string)obj["tientragop"];


                        //var data = (JObject)JsonConvert.DeserializeObject(result);
                        ////JObject
                        //txtOnline.Text = data["doanhThuOnline"].Value<int>().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void loadDataChi(string month, string year)
        {
            string baseURL = "http://apidnh.somee.com/api/Admin/TongChi?nam=" + month + "&thang=" + year;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    //wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    string url = string.Format(baseURL);
                    WebRequest request = WebRequest.Create(url);
                    request.Headers.Add("Authorization", "Bearer " + sess.token);
                    request.ContentType = "application/json";
                    //request.Method = "GET";
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var obj = JsonConvert.DeserializeObject<JArray>(result).ToObject<List<JObject>>().FirstOrDefault();
                        txtchi.Text = (string)obj["tongChi"];


                        //var data = (JObject)JsonConvert.DeserializeObject(result);
                        ////JObject
                        //txtOnline.Text = data["doanhThuOnline"].Value<int>().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadData();
        }
    }
}