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

namespace QuanLy.Index
{
    public partial class IndexAdmin : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public IndexAdmin(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            
        }
        public void loadData()
        {
            string baseURL = "https://localhost:44373/api/Account/getAccount";
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelNV>>(json);

                    dtgvAcc.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}