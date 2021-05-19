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

namespace QuanLy.QuanLy
{
    public partial class fSuathongtin : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public fSuathongtin(Session sess)
        {
            InitializeComponent();
            txtUsername.Text = GlobalData.username;
            txtTennv.Text = GlobalData.tennv;
            this.sess = sess;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

            string ten = txtTennv.Text;
            string password = txtpassword.Text;
            string loainv = cbLoai.Text;
            int type = 0;
            if (loainv == "Thủ kho")
                type = 2;
            else
                type = 3;
            string urlbase = "http://localhost:55543/api/Admin/UpdateNhanVien?username=" + GlobalData.username+"&password="+password+"&ten="+ten+"&loai="+type+"";
            Services.PUT(urlbase, sess.token);
            MessageBox.Show("Sửa thông tin thành công");
        }
    }
}