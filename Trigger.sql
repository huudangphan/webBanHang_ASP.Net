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

insert into HDTraGop(MaKH,NgayCoc,TienCoc,SoThang,laiSuat)
values(4,'2021-1-1',30050,3,3)

insert into CTHDTG(MaKho,MaSP,MaHD,SL,GiaBan)
values(1,1,23,1,5000000)
select * from HDTraGop
select * from CTHDTG
select sum(SL*GiaBan)
from CTHDTG
where MaHD=1