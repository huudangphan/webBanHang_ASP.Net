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
    public partial class CTHDTG : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public CTHDTG(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            loadData2();
            binding();
        }
        public void binding()
        {
            txtmahd.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaHD"));
            dateTimePicker1.DataBindings.Add(new Binding("Value", dataGridView1.DataSource, "NgayCoc"));
            txtTienCoc.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "TienCoc"));
            txtSoThang.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "SoThang"));
            txtLaiSuat.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "laiSuat"));

            txtTensp.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "tenSP"));
            txtsl.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "SL"));
            txtThanhTien.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "thanhTien"));
            txtKho.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "MaKho"));
        }
        public void loadData()
        {
            string baseURL = "http://localhost:55543/api/HDTG/GetHDTGId?mahd="+GlobalData.madh;
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
        public void loadData2()
        {
            string baseURL = "http://localhost:55543/api/HDTG/GetCTHDTG?mahd="+GlobalData.madh;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelCTHDTG>>(json);


                    dataGridView2.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            PTG f = new PTG(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}