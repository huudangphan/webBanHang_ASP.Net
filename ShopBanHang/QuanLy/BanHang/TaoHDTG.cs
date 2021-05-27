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

namespace QuanLy.BanHang
{
    public partial class TaoHDTG : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public TaoHDTG(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            string url = "http://localhost:55543/api/HDTG/updateMaHD";
            Services.PUT(url, sess.token);
            GlobalData.madh = Services.GET("http://localhost:55543/api/HDTG/test", sess.token);
        }
    }
}