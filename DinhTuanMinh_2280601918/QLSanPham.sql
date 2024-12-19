create database QLSanpham
go 
use QLSanpham
go

create table LoaiSP(
	MaLoai char(2) primary key,
	TenLoai nvarchar(30)
)
create table Sanpham(
	MaSP char(6) primary key,
	TenSP nvarchar(30),
	Ngaynhap DateTime,
	MaLoai char(2),
	foreign key (MaLoai) references LoaiSP (MaLoai)
)

insert into LoaiSP(MaLoai, TenLoai)
values
('01', N'Giải khát'),
('02', N'Bánh kẹo')
insert into Sanpham(MaSP, TenSP, Ngaynhap, MaLoai)
values
('SP0001', N'Bánh quy bơ sữa', CONVERT(DateTime, '20-08-2014', 105), '02'),
('SP0002', N'Bánh mì kẹp thịt', CONVERT(DateTime, '24-05-2014', 105), '02'),
('SP0003', N'Bia 333', CONVERT(DateTime, '20-04-2014', 105), '01')

