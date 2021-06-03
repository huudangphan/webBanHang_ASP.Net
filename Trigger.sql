use ShopDoCongNghe

alter trigger check_username
on KhachHang
for insert,update
as
begin
	declare @username nvarchar(50),@count int
	select @username= taiKhoan from inserted
	select @count=COUNT(*) from KhachHang where taiKhoan=@username
	if @count>1
	begin
		rollback
		raiserror('tai khoan bi trung',16,1)
	end
end


alter trigger check_tienCoc
on CTHDTG
for insert,update
as
begin
	declare @tongTien decimal(18,0),@mahd int,@tienCoc decimal(18,0)
	select @mahd=MaHD from inserted
	select @tienCoc=TienCoc from HDTraGop hd where hd.MaHD=@mahd
	select @tongTien=sum(SL*GiaBan) from inserted
	if(@tienCoc<(@tongTien/2))
	begin
		delete from CTHDTG where MaHD=@mahd
		delete from HDTraGop where MaHD=@mahd
	
		raiserror('tien coc khong du',16,1)
		rollback
		

	end
end




alter trigger check_tien_Phat
on PhieuTraGop
after insert,update
as 
begin
	declare @ngayDenHan date,@ngayTre int,@tiendong decimal(18,0),@maPhieu int,@ngayTra date
	select @ngayTra= NgayTra from inserted
	select @ngayDenHan= NgayDenHan from inserted
	select @tiendong= TienDong from inserted
	SELECT @ngayTre= DATEDIFF(day, @ngayDenHan, @ngayTra)
	select @maPhieu=MaPhieu from inserted
	if(@ngayTre<3)
	begin
		update PhieuTraGop
		set MaMucPhat=1,TienPhat=0
		where MaPhieu=@maPhieu
	end
	if(@ngayTre>3 and @ngayTre<=5)
	begin 
		update PhieuTraGop
		set MaMucPhat=2,TienPhat=TienDong*0.4
		where MaPhieu=@maPhieu
		
	end
	if(@ngayTre>5 and @ngayTre<=7)
	begin 
		update PhieuTraGop
		set MaMucPhat=3,TienPhat=TienDong*0.6
		where MaPhieu=@maPhieu
	end
	if(@ngayTre>7)
	begin 
		update PhieuTraGop
		set MaMucPhat=4,TienPhat=TienDong*0.9
	end

	
end

alter trigger check_km
on CTHDOff
after insert,update
as
begin
	declare @maKH int,@giaBan decimal(18,0),@TongMua decimal(18,0),@maHD int
	select @maHD = MaHD from inserted
	select @maKH= MaKH from HDOffLine where MaHD=@maHD
	select @TongMua =tongMua from KhachHang where MaKH=@maKH
	select @giaBan =GiaBan from inserted
	if(@TongMua>30000000 )
	begin
		update CTHDOff
		set GiaBan=@giaBan*0.95
		where MaHD=@maHD
		update KhachHang
		set tongMua+=@giaBan*0.95
	end
	if(@TongMua>=20000000 and @TongMua<=30000000)
	begin
		update CTHDOff
		set GiaBan=@giaBan*0.97
		where MaHD=@maHD
		update KhachHang
		set tongMua+=@giaBan*0.97
	end
	
	if(@TongMua<20000000)
	begin
		update KhachHang
		set tongMua+=@giaBan
	end


end


alter trigger ttien_online
on CTHDOnline
after insert
as
begin
	declare @sl int,@giaBan decimal(18,0),@maHD int,@maSP int
	select @sl=SL from inserted
	select @giaBan= GiaBan from inserted
	select @maHD=MaHD from inserted
	select @maSP=MaSP from inserted

	update CTHDOnline
	set thanhTien=@sl*@giaBan
	where MaHD=@maHD and MaSP=@maSP
	
end

 create trigger ttien_offline
 on CTHDOff
 after insert
 as
 begin
	declare @sl int,@giaBan decimal(18,0),@maHD int,@maSP int
	select @sl=SL from inserted
	select @giaBan= GiaBan from inserted
	select @maHD=MaHD from inserted
	select @maSP=MaSP from inserted
	update CTHDOff
	set thanhTien=@sl*@giaBan
	where MaHD=@maHD and MaSP=@maSP
 end

 create trigger ttien_tg
 on CTHDTG
 after insert
 as
 begin
	declare @sl int,@giaBan decimal(18,0),@maHD int,@maSP int
	select @sl=SL from inserted
	select @giaBan= GiaBan from inserted
	select @maHD=MaHD from inserted
	select @maSP=MaSP from inserted
	update CTHDTG
	set thanhTien=@sl*@giaBan
	where MaHD=@maHD and MaSP=@maSP
 end


 create trigger ttien_nhap
 on CTPN
 after insert
 as
 begin
	declare @sl int,@giaBan decimal(18,0),@maHD int,@maSP int
	select @sl=SLNhap from inserted
	select @giaBan= giaNhap from inserted
	select @maHD=maPhieuNhap from inserted
	select @maSP=MaSP from inserted
	update CTPN
	set thanhTien=@sl*@giaBan
	where maPhieuNhap=@maHD and MaSP=@maSP
 end

 select *
 from HDOnline

 insert into HDOnline(MaKH,NgayDat,NgayGiao,TinhTrang)
 values(4,'2021-1-1','2021-1-1','0')
 

 alter Table HDOnline
 add constraint PK_HDOnline primary key(MaHDCT)

 delete 
 from CTHDOnline
 
 select * from CTHDOnline

 alter table CTHDOnline
 drop column MaHD

 select *
 from HDOnline

 insert into HDOnline(MaKH,NgayDat,NgayGiao,TinhTrang)
 values(4,'2021-1-1','2021-1-1','0')



 create trigger update_SL_TonKho
 on CTPN
 after insert,update
 as
 begin
	declare @slNhap int,@maKho int,@maSP int,@maphieu int
	select @maphieu=maPhieuNhap from inserted
	select @maKho=maKho from HDNhapSP,CTPN  where HDNhapSP.maPhieuNhap=@maphieu
	select @maSP= maSP from inserted
	select @slNhap=SLNhap from inserted
	update CTTonKho
	set SL+=@slNhap
	where MaKho=@maKho and MaSP=@maSP

 end