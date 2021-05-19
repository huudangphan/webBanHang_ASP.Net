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
    public partial class DHOnline : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        BindingSource list = new BindingSource();   
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public DHOnline(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            Binding();
        }
        public void Binding()
        {
            txtmadh.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaHD"));
            txtngaydat.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "NgayDat"));
            txtngaygiao.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "NgayGiao"));
            checkBox1.DataBindings.Add(new Binding("Checked",dataGridView1.DataSource,"TinhTrang"));
                
        }
        public void loadData()
        {
            string baseURL = "http://localhost:55543/api/DHOnline/GetDHOnline";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelDHOnline>>(json);
                    list.DataSource = data;

                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ModelCT.mahd = txtmadh.Text;
            CTDHOnline f = new CTDHOnline(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}