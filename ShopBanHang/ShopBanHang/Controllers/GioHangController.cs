﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;
using System.IO;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;

namespace ShopBanHang.Controllers
{
    public class GioHangController : Controller
    {

        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();

        //databaseEntities db = new databaseEntities();

        // GET: GioHang
        public List<GioHang> getGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }// nếu chưa có giỏ hàng
            return listGioHang;
        }
        public ActionResult ThemGioHang(int masp, string url)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.maSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = getGioHang();
            // Kiểm tra sản phẩm đã tồn tại trong giỏ hàng hay chưa
            GioHang sanpham = listGioHang.Find(x => x.maSP == masp);
            if (sanpham == null)
            {
                sanpham = new GioHang(masp);
                // add sản phẩm mới
                listGioHang.Add(sanpham);
                return Redirect(url);
            }
            else
            {
                sanpham.soLuong++;
                return Redirect(url);
            }

        }
        // cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int masp, FormCollection f)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(a => a.maSP == masp);
            // kiểm tra sản phẩm có tồn tại trong giỏ hàng hay không
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = getGioHang();
            GioHang lsp = listGioHang.SingleOrDefault(x => x.maSP == masp);
            if (lsp != null)
            {
                lsp.soLuong = Int32.Parse(f["txtSoLuong"].ToString());
            }

            return RedirectToAction("GioHang");
        }
        // Xóa giỏ hàng
        public  ActionResult XoaGioHang(int masp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.maSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<GioHang> listGioHang = getGioHang();
            GioHang lsp = listGioHang.SingleOrDefault(x => x.maSP == masp);
            if (lsp != null)
            {
                listGioHang.RemoveAll(x => x.maSP == masp);

            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public static void XoaGH(int masp, List<GioHang> listGioHang)
        {
                      
            GioHang lsp = listGioHang.SingleOrDefault(x => x.maSP == masp);
            if (lsp != null)
            {
                listGioHang.RemoveAll(x => x.maSP == masp);

            }
        }
        // Giao diện giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");
            List<GioHang> listGioHang = getGioHang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        public ActionResult GioHangAdmin()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("SanPham", "NhanVien");
            List<GioHang> listGioHang = getGioHang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        public PartialViewResult PartialGioHang()
        {
            
            List<GioHang> listGioHang = getGioHang();

            return PartialView(listGioHang);
        }
        // tính tổng số lượng và tổng tiền 
        private int TongSoLuong()
        {
            int tongSL = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tongSL = listGioHang.Sum(x => x.soLuong);
            }
            return tongSL;

        }
        private double TongTien()
        {
            double tongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tongTien = listGioHang.Sum(x => x.thanhTien);
            }
            ViewBag.tongtien = tongTien;
            return tongTien;
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");
            List<GioHang> listGioHang = getGioHang();

            return View(listGioHang);
        }   
                

        [HttpPost]
        public ActionResult DatHang()
        {
            // Kiểm tra đăng nhập
            if (Session["taiKhoan"] == null || Session["taiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            // Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            HDOnline hd = new HDOnline();
            List<GioHang> gioHang = getGioHang();
            KhachHang kh = (KhachHang)Session["taiKhoan"];
            hd.MaKH = kh.MaKH;
            hd.NgayDat = DateTime.Now;
            hd.TinhTrang = false;
            
            db.HDOnlines.Add(hd);
            foreach (var item in gioHang)
            {
                CTHDOnline cthd = new CTHDOnline();
                cthd.MaKho = 1;
                cthd.SL = item.soLuong;
                cthd.MaHD = hd.MaHD;
                cthd.MaSP = item.maSP;
                cthd.GiaBan = item.donGia;
                db.CTHDOnlines.Add(cthd);

            }
            Session["GioHang"] = null;
            db.SaveChanges();

            return RedirectToAction("DatHangThanhCong", "GioHang");
        }
        public ActionResult DatHangThanhCong()
        {
            return View();
        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {

            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return View("FailureView");
            }
            DatHang();
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };

            //Adding Item Details like name, currency, price etc  
            
            //itemList.items.Add(new Item()
            //{
            //    name = "Item Name comes here",
            //    currency = "USD",
            //    price = "1",
            //    quantity = "1",
            //    sku = "sku"
            //});
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = TongTien().ToString() // Total must be equal to sum of tax, shipping and subtotal.  33


            };
            //var amount = new Amount()
            //{
            //    currency = "USD",
            //    total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
            //    details = details
            //};
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No  
                amount = amount,
                item_list = null
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }

    }
}