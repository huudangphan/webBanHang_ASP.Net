using DevExpress.XtraBars;
using Newtonsoft.Json;
using QuanLy.Model;
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
    public partial class QLSP : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public QLSP(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            Binding();
        }
        public void Binding()
        {
            txtmasp.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "maSP"));
            txtten.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "tenSP"));
            txtgia.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "giaSP"));
            txtmota.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MoTa"));
            //textBox1.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "anh"));
            



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

                    var data = JsonConvert.DeserializeObject<List<ModelSPKho>>(json);


                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
            string url = @"D:\webBanHang_ASP\ShopBanHang\ShopBanHang\Assit\img\" + textBox1.Text;
            pictureBox1.Image = Image.FromFile(url);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            NhapSPMoi f = new NhapSPMoi(Sess);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            string kho = comboBox1.Text;
            if (string.IsNullOrEmpty(kho))
            {
                MessageBox.Show("Vui lòng chọn kho muốn xem");
            }
            else
            {
                int makho = 3;
                if (kho == "Sư Vạn Hạnh")
                {
                    makho = 1;
                }

                if (kho == "Nguyễn Đình Chiểu")
                {
                    makho = 2;
                }
                GlobalData.makho = makho.ToString();
                XemCTKho f = new XemCTKho(Sess);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            

        }
    }
}