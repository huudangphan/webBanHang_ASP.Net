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

namespace QuanLy.Kho
{
    public partial class QLSP : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public QLSP(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            Binding();
        }
        public void Binding()
        {
            txtmasp.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "maSP"));
            txtten.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "tenSP"));
            txtgia.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, " giaSP"));
            txtmota.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MoTa"));
            //pictureBox1.DataBindings.Add(new Binding("Image", dataGridView1.DataSource, "anh"));


        }
        public void loadData()
        {
            string baseURL = "http://localhost:55543/api/SanPham/getAllSanPham";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelSPKho>>(json);


                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

    }
}