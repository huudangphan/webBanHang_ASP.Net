using DevExpress.XtraBars;
using Newtonsoft.Json;
using QuanLy.Model;
using QuanLy.Model.DonHang;
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

namespace QuanLy.BanHang
{
    public partial class TaoCTHDTG : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        List<GioHang> lst = new List<GioHang>();
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public TaoCTHDTG(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
        }
        public void loadData()
        {
            string baseURL = "http://apidnh.somee.com/api/SanPham/getAllSanPham";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelSanPham>>(json);


                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
              if (comboBox1.Text == "")
            {
                MessageBox.Show("Vui lòng chọn kho");
            }
            else
            {
                string kho = comboBox1.Text;
                int soluong = Int32.Parse(numericUpDown1.Value.ToString());
                int masp = Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
                double giaban = double.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString());
                int makho = 3;
                if (kho == "Sư Vạn Hạnh")
                    makho = 1;
                if (kho == "Nguyễn Đình Chiểu")
                    makho = 2;
                try
                {

                    lst.Add(new GioHang() { masp = masp.ToString(), mahd = GlobalData.madh, soluong = soluong.ToString(), giaBan = giaban.ToString(), makho = makho.ToString() });
                    MessageBox.Show("Thêm vào giỏ hàng thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            
           
        }
        public bool check()
        {
            string url1 = "http://apidnh.somee.com/api/HDTG/GetMaHDMax";
            int mahd = Int32.Parse(Services.GET(url1, sess.token));
            string url2 = "http://apidnh.somee.com/api/HDTG/GetMaCTHDMax";
            int macthd= Int32.Parse(Services.GET(url2, sess.token));
            if (mahd == macthd)
                return true;
            return false;
        }
        public bool CheckSLTon(string masp,string makho,int slMua)
        {
            //GetSLTon
            string url = "http://apidnh.somee.com/api/HDTG/GetSLTon?masp=" + masp + "&makho=" + makho;
            int slTon = Int32.Parse(Services.GET(url, sess.token));
            if (slMua <= slTon)
                return true;
            return false;
            
        }
        public int checkSLTon2()
        {
            int key = 1;
            foreach (var item in lst)
            {
                if(CheckSLTon(item.masp,item.makho,Int32.Parse(item.soluong))==false)
                {
                    key = 0;
                }                 
                    
            }
            return key;
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            double tongTien = 0;
            string ngaycoc = dateTimePicker1.Value.ToString();
            string tiencoc = txttiencoc.Text;
            string thang = sothang.Text;
            string thangcoc = "";
            
            foreach (var item in lst)
            {
                tongTien += double.Parse(item.soluong) * double.Parse(item.giaBan);
            }
            if (thang == "3 tháng")
                thangcoc = "3";
            else
                thangcoc = "6";
            if (lst.Count == 0)
                MessageBox.Show("Giỏ hàng không được để trống");
            
              
            else
            {
                if(tongTien<double.Parse(tiencoc))
                {
                    MessageBox.Show("Tiền cọc bị thừa");

                }
                else
                {
                    
                    if(checkSLTon2()==1)
                    {
                        string urltaohd = "http://apidnh.somee.com/api/HDTG/TaoHDTG?makh=" + GlobalData.makh + "&tiencoc=" + tiencoc + "&sothang=" + thangcoc;
                        Services.POST(urltaohd, sess.token);
                        foreach (var item in lst)
                        {
                            //UpdateSL

                            string baseurl = "http://apidnh.somee.com/api/HDTG/TaoCTHDTG?makho=" + item.makho + "&masp=" + item.masp + "&sl=" + item.soluong + "&giaban=" + item.giaBan;
                            Services.POST(baseurl, sess.token);
                            string url2 = "http://apidnh.somee.com/api/HDTG/UpdateSL?makho=" + item.makho + "&masp=" + item.masp + "&sl=" + item.soluong;
                            Services.PUT(url2, sess.token);

                        }
                        string urlday = "http://apidnh.somee.com/api/HDTG/UpdateNgayTra";
                        Services.POST(urlday, sess.token);
                        if (check())
                        {
                            MessageBox.Show("Tạo đơn hàng thành công");

                        }
                        else
                        {

                            MessageBox.Show("Tiền cọc phải đạt ít nhất 20 % giá trị đơn hàng");
                            //DeleteHD
                            string baseurl = "http://apidnh.somee.com/api/HDTG/DeleteHD";
                            Services.DELETE(baseurl, sess.token);
                        }
                    }    
                    else
                    {
                        MessageBox.Show("Số lượng hàng tồn không đủ");
                    }                    
                    
                                         
                    
                }
               

            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            

        }

        private void barButtonItem4_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            dataGridView2.DataSource = lst;
            double tongTien = 0;
            foreach (var item in lst)
            {
                tongTien += double.Parse(item.soluong) * double.Parse(item.giaBan);
            }
            double min = tongTien * 0.2;
            txtCocMin.Text = min.ToString();
            txtTongTien.Text = tongTien.ToString();
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            List<GioHangTemp> lstTemp = new List<GioHangTemp>();
            if (e.KeyData == Keys.Delete)
            {
                string masp = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();
                int index = lst.FindIndex(x => x.masp == masp);
                foreach (var item in lst)
                {
                    if (item.masp != masp)
                    {
                        lstTemp.Add(new GioHangTemp() { masp = masp, mahd = GlobalData.madh, soluong = item.soluong, giaBan = item.giaBan, makho = item.makho });
                    }
                }
                lst.Clear();
                foreach (var item in lstTemp)
                {
                    lst.Add(new GioHang() { masp = item.masp, mahd = item.mahd, soluong = item.soluong, giaBan = item.giaBan, makho = item.makho });
                }

                dataGridView2.DataSource = lstTemp;
            }

            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            List<GioHangTemp> lstTemp = new List<GioHangTemp>();
            string masp, sl, gia, kho;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                masp = dataGridView2.Rows[i].Cells[0].Value.ToString();
                sl = dataGridView2.Rows[i].Cells[2].Value.ToString();
                gia = dataGridView2.Rows[i].Cells[3].Value.ToString();
                kho = dataGridView2.Rows[i].Cells[4].Value.ToString();
                lstTemp.Add(new GioHangTemp() { masp = masp, mahd = GlobalData.madh, soluong = sl, giaBan = gia, makho = kho });
            }
            lst.Clear();
            foreach (var item in lstTemp)
            {
                lst.Add(new GioHang() { masp = item.masp, mahd = item.mahd, soluong = item.soluong, giaBan = item.giaBan, makho = item.makho });
            }

            foreach (var item in lst)
            {
                MessageBox.Show(item.soluong.ToString());
            }
            lstTemp.Clear();
            dataGridView2.DataSource = lst;
        }

        private void txttiencoc_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            if (!string.IsNullOrEmpty(txttiencoc.Text) &&
                 !int.TryParse(txttiencoc.Text, out i)
              )
            {
                MessageBox.Show("Hãy nhập đúng định dạng");
            }
        }
        public void loadDataTK(string tensp)
        {
            string baseURL = "http://apidnh.somee.com/api/SanPham/getSP?tensp=" + tensp;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelSanPham>>(json);


                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string tenSP = txtTenSP.Text;
            loadDataTK(tenSP);
        }
    }
}