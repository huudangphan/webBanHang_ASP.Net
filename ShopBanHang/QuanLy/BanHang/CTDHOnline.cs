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
using QuanLy.Model.DonHang;

namespace QuanLy.BanHang
{
    public partial class CTDHOnline : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        BindingSource list = new BindingSource();
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public CTDHOnline(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            loadData2();
            Binding();
            formatt();
        }

        public void Binding()
        {
            txtmahd.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "MaHD"));
            txtngaydat.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "NgayDat"));
            dateTimePicker1.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "NgayGiao"));
            checkBox1.DataBindings.Add(new Binding("Checked", dataGridView2.DataSource, "TinhTrang"));

            txtTensp.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "tenSP"));
            txtsl.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "SL"));
            txtthanhtien.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "thanhTien"));
            txtkho.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaKho"));
           

        }
        public void loadData()
        {
            string baseURL = "http://localhost:55543/api/DHOnline/GetCTHDOnline?mahd="+ModelCT.mahd;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelCTHDOnline>>(json);
                    

                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void loadData2()
        {
            string baseURL = "http://localhost:55543/api/DHOnline/GetHDOnlineID?mahd="+ModelCT.mahd;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelDHOnline>>(json);
                    list.DataSource = data;

                    dataGridView2.DataSource = data;
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void formatt()
        {
            if (txtkho.Text == "1")
                txtkho.Text = "Sư Vạn Hạnh";
            else if (txtkho.Text == "2")
                txtkho.Text = "Nguyễn Đình Chiểu";
            else
                txtkho.Text = "Xô Viết Nghệ Tĩnh";
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (checkBox1.Checked == true)
                MessageBox.Show("Đơn hàng đã được giao");
            else
            {
                try
                {
                    string query = "http://localhost:55543/api/DHOnline/GuiHang?MaDH=" + ModelCT.mahd + "&MaKho=" + txtkho.Text;
                    Services.POST(query, Sess.token);
                    MessageBox.Show("Gửi hàng thành công");
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Sản phẩm trong kho đã hết,vui lòng chọn kho khác");
                }
                
                
            }
        }
    }
}