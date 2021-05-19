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
    public partial class fThemTaiKhoan : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public fThemTaiKhoan(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            string username = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;
            string tenNv = txtTen.Text;
            string loai = cbViTri.Text;
            int type = 0;
            if (loai == "Thủ kho")
                type = 2;
            else
                type = 3;
            string query = "http://localhost:55543/api/Admin/InsertNhanVien?username="+username+"&password="+password+"&tennv="+tenNv+"&loai="+type;
            try
            {
                Services.POST(query, sess.token);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            MessageBox.Show("Tạo tài khoản thành công");
            
        }
    }
}