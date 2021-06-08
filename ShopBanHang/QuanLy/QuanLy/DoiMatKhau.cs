using DevExpress.XtraBars;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuanLy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy.QuanLy
{
    public partial class DoiMatKhau : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public DoiMatKhau(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {

        }
        public bool checkCurPass(string curPass)
        {
                    
                       
            if (curPass == GlobalData.pass)
                return true;

            return false;
        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            string curPass = txtcurPass.Text;
            string newPass = txtNewPass.Text;
            string conPass = txtConPass.Text;
            if(curPass!="" && newPass!="" && conPass != "")
            {
                if (checkCurPass(curPass))
                {
                    if(newPass==conPass)
                    {
                        string Urlbase = "http://localhost:55543/api/Admin/DoiMatKhau?password=" + newPass;
                        Services.PUT(Urlbase, sess.token);
                        MessageBox.Show("Đổi mật khẩu thành công");
                    }    
                }
                else
                {
                    MessageBox.Show("Mật khẩu hiện tại sai");
                }    
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
           
        }
    }
}