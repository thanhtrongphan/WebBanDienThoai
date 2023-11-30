create database WEBDIENTHOAI
GO
USE WEBDIENTHOAI
GO

--AccountType loại tài khoản
--Account tài khoản
--BRAND hiệu của sản phẩm
--Operation hệ điều hành
--PRODUCT sản phẩm
--BILL hoá đơn	
--BILLINFO chi tiết hoá đơn
CREATE TABLE AccountType
(
	ID int identity(1,1) primary key not null,
	Name nvarchar(50) not null
)

CREATE TABLE Account
(
	ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	NAME NVARCHAR(50) NULL,
	EMAIL NVARCHAR(50) NOT NULL,
	PASSWORD NVARCHAR(50) NOT NULL,
	NUMBERPHONE NVARCHAR(50) NULL,
	TYPEID INT NOT NULL, -- 1 là admin, 2 là khánh hàng
	ADDRESS NVARCHAR(50) NULL,
	PICTURE NVARCHAR(50) NULL,
	FOREIGN KEY (TYPEID) REFERENCES AccountType(ID)
)

CREATE TABLE BRAND
(
	ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	NAME NVARCHAR(50) NOT NULL
)

CREATE TABLE Operation
(
	ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	NAME NVARCHAR(50) NOT NULL
)

CREATE TABLE PRODUCT
(
	ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	 Name nvarchar (50) NOT NULL,
	 Price INT  NULL,
	 Stock   int  NULL,
	 Descriptions   NVARCHAR(50)  NULL,
	 SIM   int  NULL,
	 MemoryStorage   int  NULL,
	 Ram   int  NULL,
	 Picture   nvarchar (50) NULL,
	 BrandID   int  NULL,
	 OSID   int  NULL,
	 FOREIGN KEY (BrandID) REFERENCES BRAND(ID),
	 FOREIGN KEY (OSID) REFERENCES Operation(ID),
)

CREATE TABLE BILL
(
		ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		DateBuy datetime NULL,
		Status int NULL, -- 1 là đã thanh toán, 0 là chưa thanh toán
		AccountID int NULL,
		Address nvarchar(100) NULL,
		TotalPrice INT NULL,
		FOREIGN KEY (AccountID) REFERENCES ACCOUNT(ID)
)

CREATE TABLE BILLINFO
(
	PRODUCTID INT NOT NULL,
	BIllID INT NOT NULL,
	STOCK INT NULL,
	PRICE INT NULL,
	PRIMARY KEY (BIllID,PRODUCTID),
	FOREIGN KEY (PRODUCTID) REFERENCES PRODUCT(ID),
	FOREIGN KEY (BIllID) REFERENCES BILL(ID)
)

--INSERT DATA
USE [WEBDIENTHOAI]
GO
SET IDENTITY_INSERT [dbo].[AccountType] ON 

INSERT [dbo].[AccountType] ([ID], [Name]) VALUES (1, N'ADMIN')
INSERT [dbo].[AccountType] ([ID], [Name]) VALUES (2, N'CUSTOMER')
SET IDENTITY_INSERT [dbo].[AccountType] OFF
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([ID], [NAME], [EMAIL], [PASSWORD], [NUMBERPHONE], [TYPEID], [ADDRESS], [PICTURE]) VALUES (1, N'PHAN THANH TRONGJ', N'ADMIN@GMAIL.COM', N'123', N'123', 1, N'123', N'1')
INSERT [dbo].[Account] ([ID], [NAME], [EMAIL], [PASSWORD], [NUMBERPHONE], [TYPEID], [ADDRESS], [PICTURE]) VALUES (2, N'CUSTOMER', N'CUSTOMER@GMAIL.COM', N'123', N'123', 2, N'123', N'1')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[BRAND] ON 

INSERT [dbo].[BRAND] ([ID], [NAME]) VALUES (1, N'APPLE')
INSERT [dbo].[BRAND] ([ID], [NAME]) VALUES (2, N'SAMSUNG')
INSERT [dbo].[BRAND] ([ID], [NAME]) VALUES (3, N'OPPO')
INSERT [dbo].[BRAND] ([ID], [NAME]) VALUES (4, N'XIAOMI')
INSERT [dbo].[BRAND] ([ID], [NAME]) VALUES (5, N'NOKIA')
SET IDENTITY_INSERT [dbo].[BRAND] OFF
GO
SET IDENTITY_INSERT [dbo].[Operation] ON 

INSERT [dbo].[Operation] ([ID], [NAME]) VALUES (1, N'IOS')
INSERT [dbo].[Operation] ([ID], [NAME]) VALUES (2, N'ANDROID')
SET IDENTITY_INSERT [dbo].[Operation] OFF
GO
SET IDENTITY_INSERT [dbo].[PRODUCT] ON 

INSERT [dbo].[PRODUCT] ([ID], [Name], [Price], [Stock], [Descriptions], [SIM], [MemoryStorage], [Ram], [Picture], [BrandID], [OSID]) VALUES (1, N'IPHONE 15 PROMAX', 33890000, 10, N'IPHONE 15', 1, 256, 8, N'1', 1, 1)
INSERT [dbo].[PRODUCT] ([ID], [Name], [Price], [Stock], [Descriptions], [SIM], [MemoryStorage], [Ram], [Picture], [BrandID], [OSID]) VALUES (2, N'Samsung Galaxy A24', 5690000, 10, N'SAMSUNG', 2, 128, 6, N'1', 2, 2)
INSERT [dbo].[PRODUCT] ([ID], [Name], [Price], [Stock], [Descriptions], [SIM], [MemoryStorage], [Ram], [Picture], [BrandID], [OSID]) VALUES (3, N'OPPO Find N3 Flip', 22990000, 10, N'OPPO', 2, 256, 12, N'1', 3, 2)
INSERT [dbo].[PRODUCT] ([ID], [Name], [Price], [Stock], [Descriptions], [SIM], [MemoryStorage], [Ram], [Picture], [BrandID], [OSID]) VALUES (4, N'Xiaomi Redmi 12', 3690000, 10, N'XIAOMI', 2, 128, 4, N'1', 4, 2)
INSERT [dbo].[PRODUCT] ([ID], [Name], [Price], [Stock], [Descriptions], [SIM], [MemoryStorage], [Ram], [Picture], [BrandID], [OSID]) VALUES (5, N'Nokia 8210 4G', 1590000, 10, N'NOKIA', 2, 32, 1, N'1', 5, 2)
SET IDENTITY_INSERT [dbo].[PRODUCT] OFF
GO
