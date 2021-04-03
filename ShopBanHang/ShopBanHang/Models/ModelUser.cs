using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopBanHang.Models
{
    public class ModelUser
    {
        [Required(ErrorMessage ="không thể bỏ trống")]
        public string tenKH { get; set; }
        [Required(ErrorMessage = "không thể bỏ trống")]
        public string diaChi { get; set; }
        [Required(ErrorMessage = "không thể bỏ trống")]
        public DateTime ngaySinh { get; set; }
        [Required(ErrorMessage = "không thể bỏ trống")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "không thể bỏ trống")]
        public int sdt { get; set; }
        [Required(ErrorMessage = "không thể bỏ trống")]
        public string username { get; set; }
        [Required(ErrorMessage = "không thể bỏ trống")]
        [StringLength(50,MinimumLength =6)]
        [DataType(DataType.Password)]      
        public string password { get; set; }
        [Required(ErrorMessage = "không thể bỏ trống")]
       
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage ="Mật khẩu và xác nhận mật khẩu phải bằng nhau")]
        public string confirmPassword { get; set; }
    }
}