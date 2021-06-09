using DevExpress.XtraBars;
using Newtonsoft.Json;
using QuanLy.Model;
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
using QuanLy.Model.DonHang;

namespace QuanLy.BanHang
{
    public partial class PTG : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public PTG(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
        }
        public void loadData()
        {
            string baseURL = "http://localhost:55543/api/HDTG/GetPTG?mahd=" + GlobalData.madh;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<PhieuTraGop>>(json);


                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            GlobalData.madh = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            string url = "http://localhost:55543/api/HDTG/TraGop?maphieu=" + GlobalData.madh;
            Services.POST(url, sess.token);
            CTPhieuTraGop f = new CTPhieuTraGop(sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            GlobalData.madh = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            CTPhieuTraGop f = new CTPhieuTraGop(sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}