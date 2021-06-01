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
                switch (cbHang.Text)
                {
                    case "Apple":
                        hang = "1";
                        break;
                    case "Dell":
                        hang = "2";
                        break;
                    case "HP":
                        hang = "3";
                        break;
                    case "Asus":
                        hang = "4";
                        break;
                    case "SamSung":
                        hang = "5";
                        break;
                    case "Xiaomi":
                        hang = "6";
                        break;
                    case "Oppo":
                        hang = "7";
                        break;
                    case "Cannon":
                        hang = "8";
                        break;
                    case "Sony":
                        hang = "9";
                        break;
                    case "Huawei":
                        hang = "10";
                        break;
                    case "LG":
                        hang = "11";
                        break;



                }
                switch (cbLoai.Text)
                {
                    case "Dien Thoai":
                        loai = "1";
                        break;
                    case "Laptop":
                        loai = "2";
                        break;
                    case "Dong Ho":
                        loai = "3";
                        break;
                    case "Camera":
                        loai="4";
                        break;
                    
                }
                //url    ThemSP string maHang,string maLoai,string tenSP,string anh,string giaSP,string MoTa
                string baseURL = "http://localhost:55543/api/SanPham/ThemSP?maHang='"+hang+"'&maLoai='"+loai+"'&tenSP='"+tensp+"'&anh='"+anh+"'&giaSP='"+giasp+"'&MoTa='"+mota+"'";
                Services.POST(baseURL, sess.token);

                #region them chi tiet ton kho
                string sl1 = numericUpDown1.Value.ToString();
                string sl2 = numericUpDown2.Value.ToString();
                string sl3 = numericUpDown3.Value.ToString();
                string url1 = "http://localhost:55543/api/SanPham/themCTTonKho?makho=2&sl=" + sl1;
                string url2 = "http://localhost:55543/api/SanPham/themCTTonKho?makho=1&sl=" + sl2;
                string url3 = "http://localhost:55543/api/SanPham/themCTTonKho?makho=3&sl=" + sl3;
                Services.POST(url1, sess.token);
                Services.POST(url2, sess.token);
                Services.POST(url3, sess.token);


                #endregion

                MessageBox.Show("Thêm sản phẩm thành công");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ảnh bị trùng tên, vui lòng chọn ảnh khác!!");
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