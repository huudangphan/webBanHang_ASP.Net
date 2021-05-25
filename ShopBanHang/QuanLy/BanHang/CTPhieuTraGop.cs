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
    public partial class CTPhieuTraGop : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public CTPhieuTraGop(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
        }
        public void loadData()
        {
            string baseURL = "http://localhost:55543/api/HDTG/GetCTPTG?maphieu=" + GlobalData.madh;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<CTPhieuTraGop>>(json);


                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }
        }
    }
}