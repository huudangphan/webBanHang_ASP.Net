﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShopDoCongNgheEntities : DbContext
    {
        public ShopDoCongNgheEntities()
            : base("name=ShopDoCongNgheEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<CTHDOff> CTHDOffs { get; set; }
        public virtual DbSet<CTHDOnline> CTHDOnlines { get; set; }
        public virtual DbSet<CTHDTG> CTHDTGs { get; set; }
        public virtual DbSet<CTPN> CTPNs { get; set; }
        public virtual DbSet<CTTonKho> CTTonKhoes { get; set; }
        public virtual DbSet<Hang> Hangs { get; set; }
        public virtual DbSet<HDNhapSP> HDNhapSPs { get; set; }
        public virtual DbSet<HDOffLine> HDOffLines { get; set; }
        public virtual DbSet<HDOnline> HDOnlines { get; set; }
        public virtual DbSet<HDTraGop> HDTraGops { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<Kho> Khoes { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<LoaiAdmin> LoaiAdmins { get; set; }
        public virtual DbSet<Phat> Phats { get; set; }
        public virtual DbSet<PhieuTraGop> PhieuTraGops { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
