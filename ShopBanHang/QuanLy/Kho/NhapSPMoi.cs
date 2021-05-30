using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using QuanLy.Model;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;

namespace QuanLy.Kho
{
    public partial class NhapSPMoi : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        string filename = "";
        public NhapSPMoi(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            LoadData();
            LoadData2();
        }
        public void LoadData()
        {
            //http://localhost:55543/api/SanPham/GetHang
            string baseURL = "http://localhost:55543/api/SanPham/GetHang";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);
                    var data = JsonConvert.DeserializeObject<List<Hang>>(json);
                    // List<Hang>

                    
                    foreach (var item in data)
                    {
                        
                        cbHang.Items.Add(item.tenHang);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }



        }
        public void LoadData2()
        {
           
            string baseURL = "http://localhost:55543/api/SanPham/GetLoai";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);
                    var data = JsonConvert.DeserializeObject<List<LoaiSp>>(json);
                    // List<Hang>


                    foreach (var item in data)
                    {

                        cbLoai.Items.Add(item.TenLoaiSP);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }



        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opnfd = new OpenFileDialog();
                opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
                if (opnfd.ShowDialog() == DialogResult.OK)
                {
                    filename = opnfd.FileName;
                    image.Image = new Bitmap(opnfd.FileName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                
                File.Copy(filename, Path.Combine(@"D:\webBanHang_ASP\ShopBanHang\ShopBanHang\Assit\img", Path.GetFileName(filename)), true);
                string hang = "";
                string loai = "";
                string tensp = txttensp.Text;
                string giasp = txtgiasp.Text;
                string mota = txtmota.Text;
                string anh = format(filename);

                MessageBox.Show("Thêm sản phẩm thành công");
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public string format(string filename)
        {
            string name = "";
            int index = filename.LastIndexOf(@"\");
           
            for (int i = index+1; i < filename.Length; i++)
            {
                name += filename[i];
            }
            return name;
        }
        
    }
}