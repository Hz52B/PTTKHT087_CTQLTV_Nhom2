CREATE DATABASE [QuanLyThuVienDB]
GO
USE [QuanLyThuVienDB]
GO
/****** Object:  Table [dbo].[LoaiSach]    Script Date: 20/11/2023 4:36:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSach](
	[MaLoai] [nvarchar](10) NOT NULL,
	[TenLoaiSach] [nvarchar](50) NOT NULL,
	[GhiChu] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiSach] PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MuonTraSach]    Script Date: 20/11/2023 4:36:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MuonTraSach](
	[MaPhieuMuon] [int] IDENTITY(1,1) NOT NULL,
	[MaSV] [nvarchar](10) NOT NULL,
	[MaSach] [nvarchar](10) NOT NULL,
	[NgayMuon] [date] NOT NULL,
	[NgayTra] [date] NOT NULL,
	[GhiChu] [nvarchar](50) NULL,
 CONSTRAINT [PK_MuonTraSach] PRIMARY KEY CLUSTERED 
(
	[MaPhieuMuon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 20/11/2023 4:36:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNhanVien] [nvarchar](10) NOT NULL,
	[TenNhanVien] [nvarchar](50) NOT NULL,
	[SoDienThoai] [int] NOT NULL,
	[GioiTinh] [nvarchar](10) NOT NULL,
	[DiaChi] [nvarchar](100) NOT NULL,
	[MatKhau] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhaXuatBan]    Script Date: 20/11/2023 4:36:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaXuatBan](
	[MaXB] [nvarchar](10) NOT NULL,
	[NhaXuatBan] [nvarchar](50) NOT NULL,
	[GhiChu] [nvarchar](50) NULL,
 CONSTRAINT [PK_NhaXuatBan] PRIMARY KEY CLUSTERED 
(
	[MaXB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sach]    Script Date: 20/11/2023 4:36:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[MaSach] [nvarchar](10) NOT NULL,
	[TenSach] [nvarchar](50) NOT NULL,
	[MaTacGia] [nvarchar](10) NOT NULL,
	[MaXB] [nvarchar](10) NOT NULL,
	[MaLoai] [nvarchar](10) NOT NULL,
	[SoTrang] [int] NOT NULL,
	[GiaBan] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
 CONSTRAINT [PK_Sach] PRIMARY KEY CLUSTERED 
(
	[MaSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 20/11/2023 4:36:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MaSV] [nvarchar](10) NOT NULL,
	[TenSV] [nvarchar](50) NOT NULL,
	[NganhHoc] [nvarchar](50) NOT NULL,
	[KhoaHoc] [nvarchar](50) NOT NULL,
	[SoDienThoai] [int] NOT NULL,
 CONSTRAINT [PK_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TacGia]    Script Date: 20/11/2023 4:36:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TacGia](
	[MaTacGia] [nvarchar](10) NOT NULL,
	[TacGia] [nvarchar](50) NOT NULL,
	[GhiChu] [nvarchar](50) NULL,
 CONSTRAINT [PK_TacGia] PRIMARY KEY CLUSTERED 
(
	[MaTacGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'baitap', N'sach bai tap', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'giaokhoa', N'giao khoa', N'kh')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'giaotrinh', N'Giáo trình', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'L1', N'SGK', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'L4', N'SGK', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'L6', N'SBT', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'L7', N'Truyen Tranh', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'L8', N'Từ Điển', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'L9', N'SBT', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'loai1', N'truyen', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'Loai2', N'Bai Tap', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'loai3', N'SGK', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'tailieu', N'tài liệu', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'tamlinh', N'Tâm Linh', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'thieunhi', N'thieu nhi', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'tieuthuyet', N'Tiểu thuyết', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'tongiao', N'tôn giáo', N'Không')
INSERT [dbo].[LoaiSach] ([MaLoai], [TenLoaiSach], [GhiChu]) VALUES (N'truyen', N'truyen tieu thuyet', N'Không')
GO
SET IDENTITY_INSERT [dbo].[MuonTraSach] ON 

INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (2, N'1002', N'S2', CAST(N'2023-02-05' AS Date), CAST(N'2023-02-10' AS Date), N'Cũ')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (3, N'1003', N'S2', CAST(N'2023-02-05' AS Date), CAST(N'2023-02-06' AS Date), N'Mới')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (7, N'1006', N'S3', CAST(N'2023-02-06' AS Date), CAST(N'2023-02-09' AS Date), N'mới')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (8, N'1007', N'sach6', CAST(N'2023-02-07' AS Date), CAST(N'2023-02-10' AS Date), N'sach dang Mới')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (11, N'1001', N'sach 4', CAST(N'2023-02-08' AS Date), CAST(N'2023-02-09' AS Date), N'Cũ')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (12, N'1001', N'sachh', CAST(N'2023-02-08' AS Date), CAST(N'2023-02-10' AS Date), N'Cũ')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (13, N'1001', N'S3', CAST(N'2023-02-08' AS Date), CAST(N'2023-02-09' AS Date), N'Cũ')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (15, N'1002', N'S2', CAST(N'2023-02-08' AS Date), CAST(N'2023-02-09' AS Date), N'Cũ')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (16, N'1003', N'sachh', CAST(N'2023-02-08' AS Date), CAST(N'2023-02-10' AS Date), N'Cũ')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (17, N'1002', N'sach6', CAST(N'2023-02-08' AS Date), CAST(N'2023-02-10' AS Date), N'Cũ')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (1010, N'1001', N'S2', CAST(N'2023-02-08' AS Date), CAST(N'2023-02-10' AS Date), N'Cũ')
INSERT [dbo].[MuonTraSach] ([MaPhieuMuon], [MaSV], [MaSach], [NgayMuon], [NgayTra], [GhiChu]) VALUES (1012, N'1002', N'S2', CAST(N'2023-05-25' AS Date), CAST(N'2023-08-09' AS Date), N'Cũ')
SET IDENTITY_INSERT [dbo].[MuonTraSach] OFF
GO
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [GioiTinh], [DiaChi], [MatKhau]) VALUES (N'nv10', N'Tran Nguyễn Mạnh Tài', 987654321, N'Nam', N'Hà Nội', N'123456')
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [GioiTinh], [DiaChi], [MatKhau]) VALUES (N'NV3', N'Yến Nhi', 321456789, N'Nữ', N'Hà Nội', N'123456')
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [GioiTinh], [DiaChi], [MatKhau]) VALUES (N'NV6', N'Nguyễn Mạnh Tài', 987654321, N'Nam', N'Hà Nội', N'123456')
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [GioiTinh], [DiaChi], [MatKhau]) VALUES (N'admin', N'Nguyễn Mạnh Tài', 1234567890, N'Nam', N'Hà Nội', N'123456')
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [GioiTinh], [DiaChi], [MatKhau]) VALUES (N'admin1', N'Trần Huy', 987654321, N'Nam', N'Ha Noi', N'123456')
GO
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'4XB2', N'Viet Nam', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'4XB4', N'Nam Binh', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'4XB7', N'Ha Noi', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'4XB88', N'Quoc Linh', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'hanoi', N'Hà Nội', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'hongduc', N'Hồng Đức', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'laodong', N'Lao Động', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'ngayMới', N'Ngày Mới', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'NhaVan', N'Nhà Văn', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'tuoitre', N'Viet Nam', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'viet nam', N'viet nam', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'XB2', N'Kim Dong', N'Không')
INSERT [dbo].[NhaXuatBan] ([MaXB], [NhaXuatBan], [GhiChu]) VALUES (N'XB3', N'Van Dung', N'Không')
GO
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MaTacGia], [MaXB], [MaLoai], [SoTrang], [GiaBan], [SoLuong]) VALUES (N'S2', N'Sach Van', N'vanCũ', N'tuoitre', N'tieuthuyet', 1000, 20000, 6)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MaTacGia], [MaXB], [MaLoai], [SoTrang], [GiaBan], [SoLuong]) VALUES (N's22', N'Sach DG', N'TG5', N'4XB88', N'L6', 99, 50000, 99)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MaTacGia], [MaXB], [MaLoai], [SoTrang], [GiaBan], [SoLuong]) VALUES (N'S3', N'Sach Mac', N'vanCũ', N'hongduc', N'tailieu', 99, 20000, 8)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MaTacGia], [MaXB], [MaLoai], [SoTrang], [GiaBan], [SoLuong]) VALUES (N'sach 4', N'Sach Vat Ly', N'vanCũ', N'laodong', N'L1', 100, 20000, 99)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MaTacGia], [MaXB], [MaLoai], [SoTrang], [GiaBan], [SoLuong]) VALUES (N'sach6', N'Sach Lap Trình C', N'TG1', N'4XB7', N'L4', 1000, 20000, 5)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MaTacGia], [MaXB], [MaLoai], [SoTrang], [GiaBan], [SoLuong]) VALUES (N'sach7', N'Sach Toan', N'TG5', N'4XB7', N'L7', 1000, 50000, 9)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MaTacGia], [MaXB], [MaLoai], [SoTrang], [GiaBan], [SoLuong]) VALUES (N'sachh', N'Sach Hoa', N'TG1', N'4XB7', N'L4', 1000, 20000, 5)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MaTacGia], [MaXB], [MaLoai], [SoTrang], [GiaBan], [SoLuong]) VALUES (N'sachls', N'Lịch Sử', N'vanCũ', N'tuoitre', N'tieuthuyet', 99, 10000, 99)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MaTacGia], [MaXB], [MaLoai], [SoTrang], [GiaBan], [SoLuong]) VALUES (N'sachtin', N'Sách Tin', N'vanCũ', N'tuoitre', N'tieuthuyet', 99, 10000, 9)
GO
INSERT [dbo].[SinhVien] ([MaSV], [TenSV], [NganhHoc], [KhoaHoc], [SoDienThoai]) VALUES (N'1001', N'Nguyễn Mạnh Tài', N'CNTT', N'K63', 0868955825)
INSERT [dbo].[SinhVien] ([MaSV], [TenSV], [NganhHoc], [KhoaHoc], [SoDienThoai]) VALUES (N'1002', N'Trần Đình Dũng', N'CNTT', N'K62', 17623482)
INSERT [dbo].[SinhVien] ([MaSV], [TenSV], [NganhHoc], [KhoaHoc], [SoDienThoai]) VALUES (N'1003', N'Trần Anh Hoàng', N'CNTT', N'K62', 63623523)
INSERT [dbo].[SinhVien] ([MaSV], [TenSV], [NganhHoc], [KhoaHoc], [SoDienThoai]) VALUES (N'1004', N'Nguyễn Văn Nam', N'CNTT', N'K62', 0868955825)
INSERT [dbo].[SinhVien] ([MaSV], [TenSV], [NganhHoc], [KhoaHoc], [SoDienThoai]) VALUES (N'1005', N'Tran Văn Thắng', N'CNTT', N'K62', 0868955825)
INSERT [dbo].[SinhVien] ([MaSV], [TenSV], [NganhHoc], [KhoaHoc], [SoDienThoai]) VALUES (N'1006', N'Trần Huy Nam', N'CNTT', N'K62', 0868955825)
INSERT [dbo].[SinhVien] ([MaSV], [TenSV], [NganhHoc], [KhoaHoc], [SoDienThoai]) VALUES (N'1007', N'Trân Quang Đức', N'CNTT', N'K62', 0868955825)
INSERT [dbo].[SinhVien] ([MaSV], [TenSV], [NganhHoc], [KhoaHoc], [SoDienThoai]) VALUES (N'1008', N'Trần Anh Vinh', N'CNTT', N'K61', 63623523)
INSERT [dbo].[SinhVien] ([MaSV], [TenSV], [NganhHoc], [KhoaHoc], [SoDienThoai]) VALUES (N'1009', N'Ba Huy', N'GDTH', N'K61', 987654321)
GO
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'maichi', N'Mai Chi', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'quoclinh', N'Quốc Linh', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'tacgia8', N'Tac Gia 8', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'TG1', N'Tac Gia 15', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'TG4', N'Tac Gia 3', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'TG5', N'Tac Gia 5', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'TG6', N'Van Quang', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'TG7', N'Van A', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'TG8', N'Tac Gia 8', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'truongquy', N'Trương Quý', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'vanCũ', N'van Cũ', N'Không')
INSERT [dbo].[TacGia] ([MaTacGia], [TacGia], [GhiChu]) VALUES (N'vinhnguyen', N'Vinh Nguyen', N'Không')
GO
ALTER TABLE [dbo].[MuonTraSach]  WITH CHECK ADD  CONSTRAINT [FK_MuonTraSach_Sach] FOREIGN KEY([MaSach])
REFERENCES [dbo].[Sach] ([MaSach])
GO
ALTER TABLE [dbo].[MuonTraSach] CHECK CONSTRAINT [FK_MuonTraSach_Sach]
GO
ALTER TABLE [dbo].[MuonTraSach]  WITH CHECK ADD  CONSTRAINT [FK_MuonTraSach_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO
ALTER TABLE [dbo].[MuonTraSach] CHECK CONSTRAINT [FK_MuonTraSach_SinhVien]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_LoaiSach] FOREIGN KEY([MaLoai])
REFERENCES [dbo].[LoaiSach] ([MaLoai])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_LoaiSach]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_NhaXuatBan] FOREIGN KEY([MaXB])
REFERENCES [dbo].[NhaXuatBan] ([MaXB])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_NhaXuatBan]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_TacGia] FOREIGN KEY([MaTacGia])
REFERENCES [dbo].[TacGia] ([MaTacGia])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_TacGia]
GO
USE [master]
GO
ALTER DATABASE [QuanLyThuVienDB] SET  READ_WRITE 
GO
