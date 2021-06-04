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
            string baseURL = "http://localhost:55543/api/SanPham/getAllSanPham";
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

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            string ngaycoc = dateTimePicker1.Value.ToString();
            string tiencoc = txttiencoc.Text;
            string thang = sothang.Text;
            string thangcoc = "";
            double check = 0;
           
            if (thang == "3 tháng")
                thangcoc = "3";
            else
                thangcoc = "6";
            if (lst.Count == 0)
                MessageBox.Show("Giỏ hàng không được để trống");
              
            else
            {

                try
                {
                    string urltaohd = "http://localhost:55543/api/HDTG/TaoHDTG?makh=" + GlobalData.makh + "&tiencoc=" + tiencoc + "&sothang=" + thangcoc;
                    Services.POST(urltaohd, sess.token);

                    foreach (var item in lst)
                    {
                        
                        string baseurl = "http://localhost:55543/api/HDTG/TaoCTHDTG?makho="+item.makho+"&masp="+item.masp+"&sl="+item.soluong+"&giaban="+item.giaBan;
                        Services.POST(baseurl, sess.token);

                    }
                    string urlday = "http://localhost:55543/api/HDTG/UpdateNgayTra";
                    Services.POST(urlday, sess.token);
                    MessageBox.Show("Tạo đơn hàng thành công");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }

            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            

        }

        private void barButtonItem4_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            dataGridView2.DataSource = lst;
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
    }
}