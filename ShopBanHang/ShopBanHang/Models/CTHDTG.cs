//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopBanHang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTHDTG
    {
        public int MaKho { get; set; }
        public int MaSP { get; set; }
        public int MaHD { get; set; }
        public Nullable<int> SL { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public double thanhTien { get; set; }
        public virtual CTTonKho CTTonKho { get; set; }
        public virtual HDTraGop HDTraGop { get; set; }
    }
}
