using DevExpress.XtraBars;
using QuanLy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy.Model.DonHang;
using Newtonsoft.Json;
using System.Net;
using QuanLy.KhachHang;

namespace QuanLy.BanHang
{
    public partial class DHOffline : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public DHOffline(Session sess)
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
            txtngaymua.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "NgayMua"));
        }
        public void loadData()
        {
            string baseURL = "http://apidnh.somee.com/api/DHOfline/getHDOff";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelDHOffline>>(json);
                   

                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ModelCT.mahd = txtmahd.Text;         
            CTHDOffline f = new CTHDOffline(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            TaoKhachHang f = new TaoKhachHang(Sess);            
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            KhachHangOff f = new KhachHangOff(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}