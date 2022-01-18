USE [master]
GO
/****** Object:  Database [ExpressDB]    Script Date: 2021/08/05 13:32:26 ******/
CREATE DATABASE [ExpressDB]
GO
ALTER DATABASE [ExpressDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExpressDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExpressDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ExpressDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ExpressDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ExpressDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ExpressDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ExpressDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ExpressDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ExpressDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ExpressDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ExpressDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ExpressDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ExpressDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ExpressDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ExpressDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ExpressDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ExpressDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ExpressDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ExpressDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ExpressDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ExpressDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ExpressDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ExpressDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ExpressDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ExpressDB] SET  MULTI_USER 
GO
ALTER DATABASE [ExpressDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ExpressDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ExpressDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ExpressDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ExpressDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ExpressDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ExpressDB] SET QUERY_STORE = OFF
GO
USE [ExpressDB]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 2021/08/05 13:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Country] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](1024) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2021/08/05 13:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Username] [nvarchar](255) NULL,
	[PasswordHash] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 2021/08/05 13:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BranchId] [int] NOT NULL,
	[Make] [nvarchar](255) NOT NULL,
	[Model] [nvarchar](255) NOT NULL,
	[Year] [int] NOT NULL,
	[Color] [nvarchar](255) NOT NULL,
	[Registration] [nvarchar](255) NOT NULL,
	[VINumber] [nvarchar](max) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WayBill]    Script Date: 2021/08/05 13:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WayBill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssignedToVehicleId] [int] NULL,
	[TotalWeight] [decimal](10, 2) NOT NULL,
	[ParcelCount] [int] NOT NULL,
	[Reference] [nvarchar](255) NOT NULL,
	[ContentDescription] [nvarchar](4000) NULL,
	[VehicleStartsFrom] [nvarchar](4000) NOT NULL,
	[Destination] [nvarchar](4000) NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Date] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Branch] ON 

INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (2, N'Windhoek', N'Namibia', N'1 Kudu Gas Street Northern Industrial AreaWindhoek Namibia 10001Namibia')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (3, N'Upington', N'South Africa', N'6 Vooruit Street
Upington Northern Cape 8801
South Africa')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (4, N'Gaborone', N'Botswana', N'Plot 72 
Gaborone International Commerce Park Gaborone Plot 72
Botswana')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (5, N'Bloemfontein', N'South Africa', N'Unit 4 Goldex ParkVista Bloemfontein 9301South Africa')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (6, N'Johannesburg', N'South Africa', N'No 1 Gordon Avenue Meadowview Business Estate East
Johannesburg Gauteng 2090
South Africa')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (7, N'Polokwane', N'South Africa', N'9 Marmer Street Corporate Park 1 Magnavia
Polokwane Limpopo 0700
Polokwane')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (8, N'Cape Town', N'South Africa', N'4 Triton Rd
Kuilsriver Industrial Kuilsriver 7580
South Africa')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (9, N'George', N'South Africa', N'Cnr Pearl Str & Ruby Crescent Tamsui Industria
George Western Cape 6529
South Africa')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (19, N'Durban', N'South Africa', N'25 Henry Pennington Road (Richmond Road)
Westmead Durban 3608
South Africa')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (20, N'Maputu', N'Mozambique', N'
Jumbo Complex A16 Rua 13.008
Matola Maputo
Mozambique')
INSERT [dbo].[Branch] ([Id], [Name], [Country], [Address]) VALUES (21, N'Matsapha', N'Swaziland', N'Matsapha Industrial Site Off King Mswati III Avenue
Matsapha')
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [PasswordHash]) VALUES (1, N'Ashly', N'Raju', N'admin@express.co.za', N'ZJReYmYYxPGc_y0zQK7PhR7WYkRUTK_nX40sebqbprs=')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicle] ON 

INSERT [dbo].[Vehicle] ([Id], [BranchId], [Make], [Model], [Year], [Color], [Registration], [VINumber], [ModifiedDate], [CreatedDate]) VALUES (1, 8, N'Isuzu', N'FX-Series FXZ 28-360', 2021, N'White', N'NGK3445', N'SHS799123HJKSH', CAST(N'2021-08-05T12:50:42.180' AS DateTime), CAST(N'2021-08-05T12:41:05.847' AS DateTime))
INSERT [dbo].[Vehicle] ([Id], [BranchId], [Make], [Model], [Year], [Color], [Registration], [VINumber], [ModifiedDate], [CreatedDate]) VALUES (2, 19, N'Mercedes-Benz', N'Pure 2652LS/33', 2021, N'White', N'NEUJDR33', N'D23HD6FJHF78946', NULL, CAST(N'2021-08-05T12:42:42.773' AS DateTime))
INSERT [dbo].[Vehicle] ([Id], [BranchId], [Make], [Model], [Year], [Color], [Registration], [VINumber], [ModifiedDate], [CreatedDate]) VALUES (3, 6, N'Mercedes-Benz', N'Actros 4145K/51 FBU5', 2021, N'White', N'HKC234D', N'VYSIIS672HHDKA7', NULL, CAST(N'2021-08-05T12:43:50.113' AS DateTime))
INSERT [dbo].[Vehicle] ([Id], [BranchId], [Make], [Model], [Year], [Color], [Registration], [VINumber], [ModifiedDate], [CreatedDate]) VALUES (4, 20, N'MAN', N'TGS26.440 26.440', 2021, N'White', N'HGD77239', N'HHKJSHD232368', NULL, CAST(N'2021-08-05T12:45:12.103' AS DateTime))
SET IDENTITY_INSERT [dbo].[Vehicle] OFF
GO
SET IDENTITY_INSERT [dbo].[WayBill] ON 

INSERT [dbo].[WayBill] ([Id], [AssignedToVehicleId], [TotalWeight], [ParcelCount], [Reference], [ContentDescription], [VehicleStartsFrom], [Destination], [CreatedById], [CreatedDate], [Date]) VALUES (1, 1, CAST(120.00 AS Decimal(10, 2)), 12, N'IB87113LR', N'Sanitary Items. Items are fragile, handle with care.', N'Mozambique', N'Durban', 1, CAST(N'2021-08-05T12:53:10.790' AS DateTime), CAST(N'2021-08-10' AS Date))
INSERT [dbo].[WayBill] ([Id], [AssignedToVehicleId], [TotalWeight], [ParcelCount], [Reference], [ContentDescription], [VehicleStartsFrom], [Destination], [CreatedById], [CreatedDate], [Date]) VALUES (2, NULL, CAST(500.00 AS Decimal(10, 2)), 120, N'BY76525MC', N'Rice and Bean Products. Shrink-wrapped pallets.
 ', N'Durban', N'Cape Town', 1, CAST(N'2021-08-05T13:02:33.207' AS DateTime), CAST(N'2021-08-20' AS Date))
SET IDENTITY_INSERT [dbo].[WayBill] OFF
GO
ALTER TABLE [dbo].[Vehicle] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[WayBill]  WITH CHECK ADD FOREIGN KEY([AssignedToVehicleId])
REFERENCES [dbo].[Vehicle] ([Id])
GO
ALTER TABLE [dbo].[WayBill]  WITH CHECK ADD FOREIGN KEY([CreatedById])
REFERENCES [dbo].[Users] ([Id])
GO
USE [master]
GO
ALTER DATABASE [ExpressDB] SET  READ_WRITE 
GO
