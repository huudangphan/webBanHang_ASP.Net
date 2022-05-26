using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;
using PagedList;
using PagedList.Mvc;
using PayPal.Api;

namespace ShopBanHang.Controllers
{
    public class HomeController : Controller
    {
        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();
        GioHangController gh = new GioHangController();
        public PartialViewResult ListSanPham(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 12;
            var list = db.SanPhams.OrderBy(c => c.tenSP).ToPagedList(pageNum, pageSize);
            return PartialView(list);
        }
        public ActionResult Index(string search)
        {
            var kq = db.SanPhams.Where(x => x.tenSP.StartsWith(search)).ToList();

            return View(kq);
        }
        public PartialViewResult timKiem(string search)
        {
            var kq = db.SanPhams.Where(x => x.tenSP.Contains(search)).ToList();
            

            return PartialView(kq);
        }
      
        public PartialViewResult DienthoaiHot()
        {

            var list = db.SanPhams.Where(c => c.maLoai ==1 ).Take(4);

            return PartialView("DienThoaiHot", list);
        }
        public PartialViewResult LaptopHot()
        {

            var list = db.SanPhams.Where(c => c.maLoai == 2).Take(4);
            
            return PartialView("LaptopHot", list);
        }
        public PartialViewResult DongHoHot()
        {

            var list = db.SanPhams.Where(c => c.maLoai == 3).Take(4);

            return PartialView("DongHoHot", list);
        }

        public ActionResult AppleLaptop()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==2&&c.maHang==1
                       select c;
            return View(list);
        }
        public ActionResult DellLaptop()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==2 &&c.maHang==2
                       select c;
            return View(list);
        }
        public ActionResult HPLaptop()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==2&&c.maHang==3
                       select c;
            return View(list);
        }
        public ActionResult AsusLaptop()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==2 &&c.maHang==4
                       select c;
            return View(list);
        }

        public ActionResult ApplePhone()
        {
            var list = from c in db.SanPhams
                       where c.maHang==1&&c.maLoai==1
                       select c;
            return View(list);
        }
        public ActionResult SamsungPhone()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==1&&c.maHang==5 
                       select c;
            return View(list);
        }
        public ActionResult XiaomiPhone()
        {

            var list = from c in db.SanPhams
                       where c.maLoai==1&&c.maHang==6
                       select c;
            return View(list);
        }
        public ActionResult OppoPhone()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 1 && c.maHang == 7
                       select c;
            return View(list);
        }
        public ActionResult CannonCamera()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 4 && c.maHang == 8
                       select c;
            return View(list);
        }
        public ActionResult SonyCamera()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 4 && c.maHang == 9
                       select c;
            return View(list);
        }
        public ActionResult AppleWatch()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 3 && c.maHang == 1
                       select c;
            return View(list);
        }
        public ActionResult XiaomiWatch()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 3 && c.maHang == 6
                       select c;
            return View(list);
        }
        public ActionResult HuaweiWatch()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 3 && c.maHang == 10
                       select c;
            return View(list);
        }
        public ActionResult LGWatch()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 3 && c.maHang == 11
                       select c;
            return View(list);
        }
        public ActionResult BaoHanh()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
        public ActionResult TimKiemKQ(string tukhoa)
        {
            
            var kq = db.SanPhams.Where(x=>x.tenSP.Contains(tukhoa)).ToList();
            return View(kq);
        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {

            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {

                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {

                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/PaymentWithPayPal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {

                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
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

           
             gh.DatHang(1);
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


            var itemList = new ItemList()
            {
                items = new List<Item>()
            };


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
            string tongtien = user.tongtien.ToString();
            var amount = new Amount()
            {
                currency = "USD",
                total = tongtien // Total must be equal to sum of tax, shipping and subtotal.  33


            };

            var transactionList = new List<Transaction>();

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