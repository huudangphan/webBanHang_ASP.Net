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
    
    public partial class CTTonKho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CTTonKho()
        {
            this.CTHDOnlines = new HashSet<CTHDOnline>();
            this.CTHDOnlines1 = new HashSet<CTHDOnline>();
            this.CTHDTGs = new HashSet<CTHDTG>();
        }
    
        public int MaKho { get; set; }
        public int MaSP { get; set; }
        public Nullable<int> SL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHDOnline> CTHDOnlines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHDOnline> CTHDOnlines1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHDTG> CTHDTGs { get; set; }
        public virtual Kho Kho { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
