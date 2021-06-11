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
        public bool check()
        {
            string url1 = "http://apidnh.somee.com/api/DHOfline/GetMaHD";
            int mahd = Int32.Parse(Services.GET(url1, sess.token));
            string url2 = "http://apidnh.somee.com/api/DHOfline/GetMaCTHD";
            int macthd = Int32.Parse(Services.GET(url2, sess.token));
            if (mahd == macthd)
                return true;
            return false;
        }
        public bool CheckSLTon(string masp, string makho, int slMua)
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
                if (CheckSLTon(item.masp, item.makho, Int32.Parse(item.soluong)) == false)
                {
                    key = 0;
                }

            }
            return key;
        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lst.Count == 0)
                MessageBox.Show("Giỏ hàng không được để trống");
            else
            {
                if(checkSLTon2()==1)
                {
                    try
                    {
                        foreach (var item in lst)
                        {
                            string url = "http://apidnh.somee.com/api/dhofline/taocthdoff?masp=" + item.masp + "&giaban=" + item.giaBan + "&sl=" + item.soluong + "&makho=" + item.makho;
                            Services.POST(url, sess.token);
                            string url2 = "http://apidnh.somee.com/api/HDTG/UpdateSL?makho=" + item.makho + "&masp=" + item.masp + "&sl=" + item.soluong;
                            Services.PUT(url2, sess.token);

                        }
                        MessageBox.Show("Tạo đơn hàng thành công");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());
                    }
                }    
                else
                {
                   
                    string url = "http://apidnh.somee.com/api/DHOfline/XoaDHThua";
                    Services.DELETE(url, sess.token);
                    MessageBox.Show("Số lượng hàng tồn kho không đủ");
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            dataGridView2.DataSource = lst;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //lst.add(new giohang() { masp = masp.tostring(), mahd = globaldata.madh, soluong = soluong.tostring(), giaban = giaban.tostring(), makho = makho.tostring() });
            List<GioHangTemp> lstTemp = new List<GioHangTemp>();
            string masp, sl, gia, kho;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                masp = dataGridView2.Rows[i].Cells[0].Value.ToString();
                sl = dataGridView2.Rows[i].Cells[2].Value.ToString();
                gia = dataGridView2.Rows[i].Cells[3].Value.ToString();
                kho= dataGridView2.Rows[i].Cells[4].Value.ToString();
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

            List<GioHangTemp> lstTemp = new List<GioHangTemp>();
            if (e.KeyData==Keys.Delete)
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
    }
}