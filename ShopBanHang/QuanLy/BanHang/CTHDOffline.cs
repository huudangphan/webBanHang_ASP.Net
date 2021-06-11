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
    public partial class CTHDOffline : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public CTHDOffline(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
            loadData2();
            binding();
        }
        public void binding()
        {
            txtmahd.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "MaHD"));
            dateTimePicker1.DataBindings.Add(new Binding("Value", dataGridView2.DataSource, "NgayMua"));

            txtTensp.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "tenSP"));
            txtsl.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "SL"));
            txtthanhtien.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "thanhTien"));
            txtkho.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaKho"));
        }
        public void loadData()
        {
            string baseURL = "http://apidnh.somee.com/api/DHOfline/getCTHDOff?mahd=" + ModelCT.mahd;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelCTDHOff>>(json);


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
            string baseURL = "http://apidnh.somee.com/api/DHOfline/HDOff?makh=2" + ModelCT.mahd;
            using (WebClient wc = new WebClient())
            {
                try
                {

                    wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);

                    var data = JsonConvert.DeserializeObject<List<ModelDHOffline>>(json);


                    dataGridView2.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);
                int col = dataGridView2.Columns.Count;
                for (int i = 1; i < col + 2; i++)
                {
                    if (i < 4)
                    {
                        xcelApp.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
                    }
                    else
                    {
                        for (int j = 1, l = 4; j < dataGridView1.Columns.Count + 1; j++, l++)
                        {
                            xcelApp.Cells[1, l] = dataGridView1.Columns[j - 1].HeaderText;
                        }

                    }
                }

                int p = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < col + dataGridView1.Columns.Count; j++)
                    {
                        if (j < 3)
                        {
                            xcelApp.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            xcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[p].Value.ToString();
                            p++;
                        }

                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }
    }
}