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
        //http://localhost:55543/api/SanPham
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
                        string url = "http://localhost:55543/api/DHOfline/TaoCTHDOff?mahd=" + GlobalData.madh + "&masp=" + item.masp + "&giaBan=" + item.giaBan + "&SL=" + item.soluong + "&makho=" + item.makho;
                        Services.POST(url, sess.token);
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
            int soluong = Int32.Parse(numericUpDown1.Value.ToString());
            int masp= Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            double giaban= double.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString());
            int makho = 3;
            if (kho == "Sư Vạn Hạnh")
                makho = 1;
            if (kho == "Nguyễn Đình Chiểu")
                makho = 2;
            try
            {

                lst.Add(new GioHang() { masp = masp, mahd = Int32.Parse(GlobalData.madh), soluong = soluong, giaBan = giaban, makho = makho });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                

          


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}