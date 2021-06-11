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
using QuanLy.KhachHang;

namespace QuanLy.BanHang
{
    public partial class DHTG : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public DHTG(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            Binding();
        }
        public void Binding()
        {
            txtmahd.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaHD"));
            txtmakh.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaKH"));
            txttiencoc.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "TienCoc"));
            dateTimePicker1.DataBindings.Add(new Binding("value", dataGridView1.DataSource, "NgayCoc"));

        }
        public void loadData()
        {
            string baseURL = "http://apidnh.somee.com/api/HDTG/getHDTG";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelHDTG>>(json);
                   

                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            GlobalData.madh = txtmahd.Text;
            CTHDTG f = new CTHDTG(sess);
            this.Hide();
            f.ShowDialog();
            this.Show();

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            KhachHangOff f = new KhachHangOff(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            TaoKhachHang f = new TaoKhachHang(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();

        }
    }
}