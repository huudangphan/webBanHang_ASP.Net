using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy.Model;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using QuanLy.Index;

namespace QuanLy
{
    public partial class fLogin : DevExpress.XtraEditors.XtraForm
    {
        public fLogin()
        {
            InitializeComponent();
        }
        ModelUser user = new ModelUser();
        Session s = new Session();
        public void Login(string username, string password)
        {


            try
            {
                string Urlbase = "http://localhost:55543/api/Account/Post";              

                user.username = username;
                user.password = password;
                string postData = JsonConvert.SerializeObject(user);
                string url = string.Format(Urlbase);
                WebRequest request = WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "POST";
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(postData);
                    streamWriter.Flush();
                    streamWriter.Close();
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var data = (JObject)JsonConvert.DeserializeObject(result);
                        s.role = data["loai"].Value<int>();
                        s.username = data["username"].Value<string>();                       
                        s.token = data["token"].Value<string>();
                        

                                       

                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Username or Password invalid");
            }


        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            Login(username, password);
            if (s.role == 1)
            {
                
                IndexAdmin f = new IndexAdmin(s);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else if (s.role == 2)
            {
                IndexKho f = new IndexKho(s);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else if(s.role==3)
            {
                IndexBanHang f = new IndexBanHang(s);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }    

        }
    }
}