using DevExpress.XtraBars;
using Newtonsoft.Json;
using QuanLy.Model;
using QuanLy.Model.DoanhThu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string baseURL = "http://localhost:55543/api/Admin/DoanhThuOnline?thang="+month+"&nam="+year;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<DoanhThuOnline>>(json);

                    dtgvonline.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void loadDataOffline(string month, string year)
        {
            string baseURL = "http://localhost:55543/api/Admin/DoanhThuOffline?thang=" + month + "&nam=" + year;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<DoanhThuOffline>>(json);

                    dtgvoff.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void loadDataTG(string month, string year)
        {
            //DoanhThuTG
            string baseURL = "http://localhost:55543/api/Admin/DoanhThuTG?nam=" + month + "&thang=" + year; 
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<DoanhThuTG>>(json);

                    dtgvtg.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void loadDataPhieu(string month, string year)
        {
            string baseURL = "http://localhost:55543/api/Admin/DoanhThuTGThang?nam=" + month + "&thang=" + year;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<DoanThuPTG>>(json);

                    dtgvphieu.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void loadDataPhat(string month, string year)
        {
            string baseURL = "http://localhost:55543/api/Admin/DoanhThuPhat?nam=" + month + "&thang=" + year;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<DoanhThuPhat>>(json);

                    dtgvphat.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void loadDataChi(string month, string year)
        {
            string baseURL = "http://localhost:55543/api/Admin/TongChi?nam=" + month + "&thang=" + year;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<TongChi>>(json);

                    dtgvchi.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadData();
        }
    }
}