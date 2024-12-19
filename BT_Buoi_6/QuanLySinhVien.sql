create database QuanLySinhVien
go 
use QuanLySinhVien
go

create table Faculty(
	FacultyID int primary key,
	FacultyName nvarchar(200)
)

create table Student(
	StudentID nvarchar(20) primary key,
	FullName nvarchar(200),
	AverageScore float,
	FacultyID int,
	foreign key (FacultyID) references Faculty(FacultyID),
)

insert into Faculty(FacultyID, FacultyName)
values(
	1,
	N'Công nghệ thông tin'
)
insert into Faculty(FacultyID, FacultyName)
values(
	2,
	N'Ngôn ngữ Anh'
)
insert into Faculty(FacultyID, FacultyName)
values(
	3,
	N'Quản trị kinh doanh'
)
insert into Student(StudentID, FullName, AverageScore,FacultyID)
values(
	N'1611061916',
	N'Nguyễn Trần Hoàng Lan',
	4.5,
	1
)
insert into Student(StudentID, FullName, AverageScore,FacultyID)
values(
	N'1711060596',
	N'Đàm Minh Đức',
	2.5,
	1
)
insert into Student(StudentID, FullName, AverageScore,FacultyID)
values(
	N'1711061004',
	N'Nguyễn Quốc An',
	10,
	2
)

