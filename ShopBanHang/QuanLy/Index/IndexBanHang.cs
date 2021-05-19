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
using QuanLy.BanHang;

namespace QuanLy.Index
{
    public partial class IndexBanHang : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public IndexBanHang(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            DHOnline f = new DHOnline(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            DHOffline f = new DHOffline(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            DHTG f = new DHTG(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            PTG f = new PTG(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}