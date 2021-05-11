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

update PhieuTraGop
set ngayTra='2021-5-4'
where MaPhieu=32

	select *
	from PhieuTraGop
where MaPhieu=32



insert into HDTraGop(MaKH,NgayCoc,TienCoc,SoThang,laiSuat)
values(4,'2021-1-1',30050,3,3)

insert into CTHDTG(MaKho,MaSP,MaHD,SL,GiaBan)
values(1,1,23,1,5000000)
select * from HDTraGop
select * from CTHDTG
select sum(SL*GiaBan)
from CTHDTG
where MaHD=1