using DevExpress.XtraBars;
using Newtonsoft.Json;
using QuanLy.Model;
using QuanLy.Model.DonHang;
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

namespace QuanLy.BanHang
{
    public partial class CTHDOffline : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public CTHDOffline(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            loadData2();
            binding();
        }
        public void binding()
        {
            txtmahd.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "MaHD"));
            dateTimePicker1.DataBindings.Add(new Binding("Value", dataGridView2.DataSource, "NgayMua"));

            txtTensp.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "tenSP"));
            txtsl.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "SL"));
            txtthanhtien.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "thanhTien"));
            txtkho.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaKho"));
        }
        public void loadData()
        {
            string baseURL = "http://localhost:55543/api/DHOfline/getCTHDOff?mahd=" + ModelCT.mahd;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelCTDHOff>>(json);


                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void loadData2()
        {
            string baseURL = "http://localhost:55543/api/DHOfline/HDOff?makh=2" + ModelCT.mahd;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelDHOffline>>(json);


                    dataGridView2.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}