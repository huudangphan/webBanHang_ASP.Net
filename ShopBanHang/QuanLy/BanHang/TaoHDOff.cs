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
    public partial class TaoHDOff : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        List<GioHang> lst = new List<GioHang>();
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public TaoHDOff(Session sess)
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
        public void loadDataTK(string tensp)
        {
            string baseURL = "http://localhost:55543/api/SanPham/getSP?tensp="+tensp;
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
            if (lst.Count == 0)
                MessageBox.Show("Giỏ hàng không được để trống");
            else
            {
                try
                {
                    foreach (var item in lst)
                    {
                        string url = "http://localhost:55543/api/dhofline/taocthdoff?mahd=" + GlobalData.madh + "&masp=" + item.masp + "&giaban=" + item.giaBan + "&sl=" + item.soluong + "&makho=" + item.makho;
                        Services.POST(url, sess.token);
                        
                    }
                    MessageBox.Show("Tạo đơn hàng thành công");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            string kho = comboBox1.Text;
            if (kho == "")
            {
                MessageBox.Show("Vui lòng chọn kho ");
            }
            else
            {
                int soluong = Int32.Parse(numericUpDown1.Value.ToString());
                int masp = Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
                double giaban = double.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString());
                int makho = 0;
                if (kho == "Sư Vạn Hạnh")
                    makho = 1;
                if (kho == "Nguyễn Đình Chiểu")
                    makho = 2;
                else
                    makho = 3;
                try
                {

                    lst.Add(new GioHang() { masp = masp.ToString(), mahd = GlobalData.madh, soluong = soluong.ToString(), giaBan = giaban.ToString(), makho = makho.ToString() });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }          
                
                    


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            dataGridView2.DataSource = lst;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //lst.Add(new GioHang() { masp = masp.ToString(), mahd = GlobalData.madh, soluong = soluong.ToString(), giaBan = giaban.ToString(), makho = makho.ToString() });
            List<GioHangTemp> lstTemp = new List<GioHangTemp>();
            string masp, sl, gia, kho;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                masp = dataGridView2.Rows[i].Cells[0].Value.ToString();
                sl = dataGridView2.Rows[i].Cells[1].Value.ToString();
                gia = dataGridView2.Rows[i].Cells[2].Value.ToString();
                kho= dataGridView2.Rows[i].Cells[3].Value.ToString();
                lstTemp.Add(new GioHangTemp() { masp=masp,mahd=GlobalData.madh,soluong=sl,giaBan=gia,makho=kho});
            }
            lst.Clear();
            foreach (var item in lstTemp)
            {
                lst.Add(new GioHang() { masp=item.masp,mahd=item.mahd,soluong=item.soluong,giaBan=item.giaBan,makho=item.makho });
            }

            foreach (var item in lst)
            {
                MessageBox.Show(item.soluong.ToString());
            }
            lstTemp.Clear();
            dataGridView2.DataSource = lst;
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData==Keys.Delete)
            {
                lst.Clear();
                MessageBox.Show("Xóa giỏ hàng thành công");
            }    
        }
    }
}