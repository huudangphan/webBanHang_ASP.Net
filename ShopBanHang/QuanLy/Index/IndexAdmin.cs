using DevExpress.XtraBars;
using Newtonsoft.Json;
using QuanLy.Model;
using QuanLy.QuanLy;
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
            string baseURL = "http://localhost:55543/api/Admin/getNhanVien";
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

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            fThemTaiKhoan f = new fThemTaiKhoan(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void Update_ItemClick(object sender, ItemClickEventArgs e)
        {
            GlobalData.username = dtgvAcc.Rows[dtgvAcc.CurrentRow.Index].Cells[0].Value.ToString();
            GlobalData.tennv= dtgvAcc.Rows[dtgvAcc.CurrentRow.Index].Cells[1].Value.ToString();
            fSuathongtin f = new fSuathongtin(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}