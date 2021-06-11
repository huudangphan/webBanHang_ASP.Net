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

namespace QuanLy.Kho
{
    public partial class CTK : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        List<SanPhamThem> lst = new List<SanPhamThem>();

        public CTK(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            Binding();
        }
        public void loadData()
        {
            string baseURL = "http://apidnh.somee.com/api/SanPham/XemCTKho?makho=" + GlobalData.makho;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelTonKho>>(json);
                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void Binding()
        {
            txtmasp.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "maSP"));
            txttensp.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "tenSP"));
            txtslton.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "SL"));
            switch (GlobalData.makho)
            {
                case "1":
                    txtkho.Text = "Sư Vạn Hạnh";
                    break;
                case "2":
                    txtkho.Text = "Nguyễn Đình Chiểu";
                    break;
                case "3":
                    txtkho.Text = "Xô Viết Nghệ Tĩnh";
                    break;
            }

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lst.Count == 0)
                MessageBox.Show("Vui lòng chọn sản phẩm muốn nhập thêm");
            else
            {
                string date = dateTimePicker1.Value.ToString();
                
                string url1 = "http://apidnh.somee.com/api/SanPham/themphieunhap?ngaynhap=" + date+"&makho="+GlobalData.makho;
                Services.POST(url1, sess.token);
                foreach (var item in lst)
                {
                    double thanhtien = double.Parse(item.slnhap) * double.Parse(item.gianhap);
                    string url2 = "http://apidnh.somee.com/api/SanPham/themctpn?masp=" + item.masp+"&slnhap="+item.slnhap+"&gianhap=1&thanhtien="+thanhtien;
                    Services.POST(url2, sess.token);

                }
                MessageBox.Show("Nhập hàng thành công");
                loadData();
                
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            string ngaynhap = dateTimePicker1.Value.ToString();
            string masp = txtmasp.Text;
            string slnhap = numericUpDown1.Value.ToString();
            string gianhap = txtgianhap.Text;
            if(
                gianhap!=""

                )
            {
                try
                {
                    lst.Add(new SanPhamThem() { masp = masp.ToString(), slnhap = slnhap, gianhap = gianhap });
                    MessageBox.Show("Chọn sản phẩm thành công");
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                dataGridView2.DataSource = lst;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<SanPhamThemTemp> lstTemp = new List<SanPhamThemTemp>();
            string masp, sl, gia;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                masp = dataGridView2.Rows[i].Cells[0].Value.ToString();
                sl= dataGridView2.Rows[i].Cells[1].Value.ToString();
                gia= dataGridView2.Rows[i].Cells[2].Value.ToString();
                lstTemp.Add(new SanPhamThemTemp() { masp = masp, slnhap = sl, gianhap = gia });
            }
            lst.Clear();
            foreach (var item in lstTemp)
            {
                lst.Add(new SanPhamThem() { masp = item.masp, slnhap = item.slnhap, gianhap = item.gianhap });
            }
            lstTemp.Clear();
            dataGridView2.DataSource = lst;
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            List<SanPhamThemTemp> lstTemp = new List<SanPhamThemTemp>();
            if (e.KeyData == Keys.Delete)
            {
                string maspp = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();
                int index = lst.FindIndex(x => x.masp == maspp);
               
                try
                {
                    foreach (var item in lst)
                    {
                        if (item.masp != maspp)
                        {
                            lstTemp.Add(new SanPhamThemTemp() { masp = item.masp, slnhap = item.slnhap, gianhap = item.gianhap });
                        }
                    }
                    lst.Clear();
                    foreach (var item in lstTemp)
                    {
                        lst.Add(new SanPhamThem() { masp = item.masp, slnhap = item.slnhap, gianhap = item.gianhap });
                    }

                    dataGridView2.DataSource = lstTemp;

                    
                }
                catch (Exception ex)
                {

                    
                }
               


            }
           
        }
    }
}