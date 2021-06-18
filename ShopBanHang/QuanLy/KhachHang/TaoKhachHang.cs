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

namespace QuanLy.KhachHang
{
    public partial class TaoKhachHang : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public TaoKhachHang(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            string ten = txtTen.Text;
            string diachi = txtDiaChi.Text;
            string email = txtEmail.Text;
            string sdt = txtSDT.Text;
            string url = "http://apidnh.somee.com/api/KhachHang/ThemKhachHangkUser?tenkh=" + ten + "&diachi=" + diachi + "&sdt=" + sdt + "&email=" + email;
            try
            {
                Services.POST(url, sess.token);
                MessageBox.Show("Thêm khách hàng thành công");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            if (!string.IsNullOrEmpty(txtSDT.Text) &&
                 !int.TryParse(txtSDT.Text, out i)
              )
            {
                MessageBox.Show("Hãy nhập đúng định dạng số điện thoại");
            }
        }
    }
}