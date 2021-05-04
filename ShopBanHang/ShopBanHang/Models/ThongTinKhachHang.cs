using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public class ThongTinKhachHang
    {
        [DisplayName("Tên khách hàng")]
        [Required(ErrorMessage = "không thể bỏ trống")]
        public string tenKH { get; set; }
        [Required(ErrorMessage = "không thể bỏ trống")]
        [DisplayName("Địa chỉ")]
        public string diaChi { get; set; }

        [Required(ErrorMessage = "không thể bỏ trống")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "không thể bỏ trống")]
        [DisplayName("Số điện thoại")]
        public int sdt { get; set; }

       
    }
}