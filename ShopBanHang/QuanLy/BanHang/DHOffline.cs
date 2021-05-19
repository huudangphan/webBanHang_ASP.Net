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
        }
        public void loadData()
        {
            string baseURL = "http://localhost:55543/api/DHOfline/getHDOff";
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
    }
}