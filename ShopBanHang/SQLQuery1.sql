create database ShopDoCongNghe
use ShopDoCongNghe
go
create table KhachHang(
MaKH int identity(1,1)  not null,
tenKH nvarchar(50),
DiaChi nvarchar(50),
SDT int,
email nvarchar(50),
taiKhoan nvarchar(50),
matKhau nvarchar(50),
tongMua decimal(18,0)
constraint PK_KhachHang primary key(MaKH)
)
create table Hang(
maHang int identity(1,1),
tenHang nvarchar(50)
constraint PK_Hang primary key(maHang)
)
create table Loai(
maLoai int identity(1,1),
TenLoaiSP nvarchar(50)
constraint PK_Loai primary key (maLoai)
)
create table SanPham(
maSP int identity(1,1),
maHang int,
maLoai int,
tenSP nvarchar(50),
anh nvarchar(50),
giaSP decimal(18,0)
constraint PK_SanPham primary key(maSP),
constraint FK_Hang foreign key(maHang) references Hang(maHang),
constraint FK_Loai foreign key(maLoai) references Loai(maLoai)
)
create table Kho(
maKho int identity(1,1),
tenKho nvarchar(50)
constraint PK_Kho primary key(maKho)
)
create table HDNhapSP(
maPhieuNhap int identity(1,1),
ngayNhap date,
maKho int 
constraint PK_HDNhapSP primary key(maPhieuNhap),
constraint FK_HDNhapSP foreign key(maKho) references Kho(maKho)
)
create table CTPN(
maPhieuNhap int,
maSP int,
SLNhap int,
giaNhap decimal(18,0)
constraint PK_CTPN primary key(maPhieuNhap,maSP),
constraint FK_HDNhapSP2 foreign key(maPhieuNhap) references HDNhapSP(maPhieuNhap),
constraint FK_SanPham foreign key(maSP) references SanPham(maSP)


)
create table CTTonKho
(MaKho int,
MaSP int, 
SL int
constraint PK_CTTonKho primary key(MaKho,MaSP),
constraint FK_CTTonKho foreign key(MaKho) references Kho(maKho),
constraint FK_CTTonKho2 foreign key(MaSP) references SanPham(maSP)
)
create table Phat(
maMucPhat int,
mucPhat int
constraint PK_Phat primary key(maMucPhat)
)
create table TinNhan(
maTN int identity(1,1),
ThoiGian date
constraint PK_TinNhan primary key(maTN)
)
create table CTTN(
maKHGui int,
maTN int,
NoiDung nvarchar(max),
idNguoiNhan int,
constraint PK_CTTN primary key(maKHGui,maTN)	,
constraint FK_CTTN foreign key(maKHGui) references KhachHang(MaKH),
constraint FK_CTTN2 foreign key(maTN) references TinNhan(maTN)
)
create table HDOffLine(
MaHD int identity(1,1),
MaKH int,
NgayMua date,
constraint PK_HDOffLine primary key(MaHD,MaKH),
constraint FK_HDOffLine foreign key (MaKH) references KhachHang(MaKH)
)
create table CTHDOff
(
MaKho int,
MaSP int,
MaHD int,
SL int,
GiaBan decimal(18,0),
constraint PK_CTHDOff primary key(MaKho,MaSP,MaHD)

)
create table HDOnline(
MaHD int identity(1,1),
MaKH int,
NgayDat date,
NgayGiao date,
TinhTrang bit,
constraint PK_HDOnline primary key(MaHD,MaKH),
constraint FK_HDOnline foreign key(MaKH) references KhachHang(MaKH)
)
create table CTHDOnline
(
MaKho int,
MaSP int,
MaHD int,
SL int,
GiaBan decimal(18,0),
constraint PK_CTHDOnline primary key(MaKho,MaSP,MaHD)

)
create table HDTraGop(
MaHD int identity(1,1),
MaKH int,
NgayCoc date,
TienCoc decimal(18,0),
SoThang int,
laiSuat int
)
create table CTHDTG(
MaKho int,
MaSP int,
MaHD int,
SL int,
GiaBan decimal(18,0),
constraint PK_CTHDTG primary key(MaKho,MaSP,MaHD)

)

create table PhieuTraGop(
MaPhieu int identity(1,1),
MaHD int,
NgayTra date,
NgayDenHan date,
Ki int,
MaMucPhat int,
TienDong decimal(18,0),
TienPhat decimal(18,0),
constraint	PK_PhieuTraGop primary key(MaPhieu)

)



