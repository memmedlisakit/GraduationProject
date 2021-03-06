USE [master]
GO
/****** Object:  Database [IMS_DB]    Script Date: 19-Mar-18 1:11:22 PM ******/
CREATE DATABASE [IMS_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IMS_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\IMS_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IMS_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\IMS_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [IMS_DB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IMS_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IMS_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IMS_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IMS_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IMS_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IMS_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [IMS_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IMS_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IMS_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IMS_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IMS_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IMS_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IMS_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IMS_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IMS_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IMS_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IMS_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IMS_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IMS_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IMS_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IMS_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IMS_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IMS_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IMS_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [IMS_DB] SET  MULTI_USER 
GO
ALTER DATABASE [IMS_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IMS_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IMS_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IMS_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IMS_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IMS_DB] SET QUERY_STORE = OFF
GO
USE [IMS_DB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [IMS_DB]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 19-Mar-18 1:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[brand_id] [int] IDENTITY(1,1) NOT NULL,
	[brand_name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[brand_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 19-Mar-18 1:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[cart_id] [int] IDENTITY(1,1) NOT NULL,
	[cart_user_id] [int] NOT NULL,
	[cart_product_id] [int] NOT NULL,
	[cart_product_quantity] [int] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[cart_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 19-Mar-18 1:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[order_user_id] [int] NOT NULL,
	[order_product_id] [int] NOT NULL,
	[order_product_quantity] [int] NOT NULL,
	[order_total_price] [float] NOT NULL,
	[order_date] [date] NOT NULL,
	[order_status] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission_Id_And_Role_Id]    Script Date: 19-Mar-18 1:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission_Id_And_Role_Id](
	[p_id] [int] IDENTITY(1,1) NOT NULL,
	[p_permission_id] [int] NOT NULL,
	[p_role_id] [int] NOT NULL,
 CONSTRAINT [PK_Permission_Id_And_Role_Id] PRIMARY KEY CLUSTERED 
(
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 19-Mar-18 1:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[per_id] [int] IDENTITY(1,1) NOT NULL,
	[per_link] [nvarchar](500) NOT NULL,
	[per_controller] [nvarchar](150) NOT NULL,
	[per_action] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[per_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 19-Mar-18 1:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[pro_id] [int] IDENTITY(1,1) NOT NULL,
	[pro_name] [nvarchar](500) NOT NULL,
	[pro_quantity] [int] NOT NULL,
	[pro_price] [decimal](10, 2) NOT NULL,
	[pro_img] [nvarchar](500) NULL,
	[pro_color] [nvarchar](50) NULL,
	[pro_status] [bit] NOT NULL,
	[pro_brand_id] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[pro_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 19-Mar-18 1:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Id_And_Role_Id]    Script Date: 19-Mar-18 1:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Id_And_Role_Id](
	[p_id] [int] IDENTITY(1,1) NOT NULL,
	[p_user_id] [int] NOT NULL,
	[p_role_id] [int] NOT NULL,
 CONSTRAINT [PK_User_Id_And_Role_Id] PRIMARY KEY CLUSTERED 
(
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19-Mar-18 1:11:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_username] [nvarchar](150) NOT NULL,
	[user_email] [nvarchar](150) NOT NULL,
	[user_password] [nvarchar](250) NOT NULL,
	[user_img] [nvarchar](500) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([brand_id], [brand_name]) VALUES (1, N'Epson ')
INSERT [dbo].[Brands] ([brand_id], [brand_name]) VALUES (2, N'HP ')
INSERT [dbo].[Brands] ([brand_id], [brand_name]) VALUES (4, N'Brother ')
INSERT [dbo].[Brands] ([brand_id], [brand_name]) VALUES (17, N'Canon')
SET IDENTITY_INSERT [dbo].[Brands] OFF
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([cart_id], [cart_user_id], [cart_product_id], [cart_product_quantity]) VALUES (54, 26, 25, 1)
INSERT [dbo].[Cart] ([cart_id], [cart_user_id], [cart_product_id], [cart_product_quantity]) VALUES (55, 26, 22, 2)
INSERT [dbo].[Cart] ([cart_id], [cart_user_id], [cart_product_id], [cart_product_quantity]) VALUES (59, 23, 23, 1)
SET IDENTITY_INSERT [dbo].[Cart] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([order_id], [order_user_id], [order_product_id], [order_product_quantity], [order_total_price], [order_date], [order_status]) VALUES (41, 26, 25, 1, 8.49, CAST(N'2018-03-18' AS Date), 0)
INSERT [dbo].[Orders] ([order_id], [order_user_id], [order_product_id], [order_product_quantity], [order_total_price], [order_date], [order_status]) VALUES (42, 26, 22, 2, 113.9, CAST(N'2018-03-18' AS Date), 0)
INSERT [dbo].[Orders] ([order_id], [order_user_id], [order_product_id], [order_product_quantity], [order_total_price], [order_date], [order_status]) VALUES (43, 22, 22, 1, 56.95, CAST(N'2018-03-18' AS Date), 0)
INSERT [dbo].[Orders] ([order_id], [order_user_id], [order_product_id], [order_product_quantity], [order_total_price], [order_date], [order_status]) VALUES (44, 22, 26, 6, 233.7, CAST(N'2018-03-18' AS Date), 1)
INSERT [dbo].[Orders] ([order_id], [order_user_id], [order_product_id], [order_product_quantity], [order_total_price], [order_date], [order_status]) VALUES (45, 23, 25, 2, 16.98, CAST(N'2018-03-18' AS Date), 0)
INSERT [dbo].[Orders] ([order_id], [order_user_id], [order_product_id], [order_product_quantity], [order_total_price], [order_date], [order_status]) VALUES (46, 22, 27, 3, 47.49, CAST(N'2018-03-19' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Permission_Id_And_Role_Id] ON 

INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (102, 1, 25)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (103, 6, 25)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (104, 11, 25)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (105, 15, 25)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (110, 15, 31)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (111, 17, 31)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (284, 1, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (285, 2, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (286, 3, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (287, 4, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (288, 6, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (289, 7, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (290, 8, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (291, 9, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (292, 11, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (293, 12, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (294, 13, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (295, 15, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (296, 17, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (297, 18, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (298, 20, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (299, 21, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (300, 22, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (301, 23, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (302, 24, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (303, 26, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (304, 27, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (305, 28, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (306, 29, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (307, 30, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (308, 31, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (309, 32, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (310, 33, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (311, 35, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (312, 36, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (313, 37, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (314, 38, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (315, 39, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (316, 40, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (317, 41, 24)
INSERT [dbo].[Permission_Id_And_Role_Id] ([p_id], [p_permission_id], [p_role_id]) VALUES (318, 42, 24)
SET IDENTITY_INSERT [dbo].[Permission_Id_And_Role_Id] OFF
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (1, N'Rolu melumatini gormek ', N'Role', N'Index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (2, N'Rolu melumatini elave etmek', N'Role', N'Insert')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (3, N'Rolu melumatini silmek ', N'Role', N'Delete')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (4, N'Rolu melumatini yenilemek', N'Role', N'Update')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (6, N'Istifadechi melumatini gormek', N'User', N'Index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (7, N'Istifadechi melumati elave etmek', N'User', N'Insert')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (8, N'Istifadechi melumatini silmek', N'User', N'Delete')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (9, N'Istifadechi melumatini yenilemek', N'User', N'Update')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (11, N'Role ve Permission melumatini gormek', N'RoleAndPermission', N'Index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (12, N'Role ve Permission melumatini elave etmek', N'RoleAndPermission', N'Insert')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (13, N'Role ve Permission melumatini silmek', N'RoleAndPermission', N'Delete')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (15, N'Dashboard sehifesini gormek', N'Dashboard', N'Index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (17, N'Permission melumatini gormek', N'Permission', N'Index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (18, N'Permission melumatini yenilemek', N'Permission', N'Update')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (20, N'Role ve Permission melumatini yenilemek', N'RoleAndPermission', N'Update')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (21, N'Role ve User melumatini gormek', N'RoleAndUser', N'Index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (22, N'Role ve User melumatini Silmek', N'RoleAndUser', N'Delete')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (23, N'Role ve User melumatini elave etmek', N'RoleAndUser', N'Insert')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (24, N'Role ve User melumatini yenilemek', N'RoleAndUser', N'Update')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (26, N'Brand melumatlarini gormek', N'Brand', N'Index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (27, N'Brand melumatini elave etmek', N'Brand', N'Insert')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (28, N'Brand melumatini  silmek', N'Brand', N'Delete')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (29, N'Brand melumatini yenilemek', N'Brand', N'Update')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (30, N'Product melumatlarini gormek', N'Product', N'Index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (31, N'Product melumatini elave etmek', N'Product', N'Insert')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (32, N'Product melumatini silmek', N'Product', N'Delete')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (33, N'Product melumatini yenilemek', N'Product', N'Update')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (35, N'Mushteri product sehifesi gormek', N'Product', N'Customer_index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (36, N'Product carta elave etmek', N'Product', N'Add_to_cart')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (37, N'Cart sehifesini gormek', N'Product', N'Cart')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (38, N'Cart dan product silmek', N'Product', N'Delete_cart')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (39, N'Cart dan Product sifarish etmek', N'Product', N'Order')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (40, N'Order melumatini gormek', N'Order', N'Index')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (41, N'Order melumatini yenilemek', N'Order', N'Order_Update')
INSERT [dbo].[Permissions] ([per_id], [per_link], [per_controller], [per_action]) VALUES (42, N'Order melumatini silmek', N'Order', N'Delete_Order')
SET IDENTITY_INSERT [dbo].[Permissions] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (14, N'intelliFAX-2840 Black High Yield Toner Cartridge', 8, CAST(66.95 AS Decimal(10, 2)), N'03162018214018PM400G135.jpg', N'rgb(244, 232, 65)', 0, 4)
INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (20, N'Canon PGI-250XL & CLI-251XL Compatible High-Yield 5-Piece Combo Pack', 25, CAST(17.95 AS Decimal(10, 2)), N'03162018232633PM640canon-pgi250-cli-251-5-pack.jpg', N'rgb(65, 244, 223)', 0, 17)
INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (21, N'Canon PGI-250XL Compatible Pigment Black High-Yield Ink Cartridge', 13, CAST(4.95 AS Decimal(10, 2)), N'03162018232435PM061pgi-250xlblk_1.jpg', N'rgb(244, 66, 66)', 1, 17)
INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (22, N'Canon PGI-270XL & CLI-271XL Compatible High-Yield 10-Piece Combo Pack', 25, CAST(56.95 AS Decimal(10, 2)), N'03162018233636PM973pgi270xl-cli270xl-10pack_final.jpg', N'rgb(244, 65, 211)', 1, 17)
INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (23, N'intelliFAX-2840 Black High Yield Toner Cartridge ', 53, CAST(10.95 AS Decimal(10, 2)), N'03162018232241PM815N5480.wt.jpg', N'rgb(244, 232, 65)', 1, 4)
INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (24, N'intelliFAX-2840 Black High Yield Toner Cartridge ', 13, CAST(66.95 AS Decimal(10, 2)), N'03162018233642PM928toner.jpg', N'rgb(65, 244, 223)', 0, 4)
INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (25, N'Epson 252XL (T252XL120) Remanufactured Black High-Yield Ink Cartridge', 41, CAST(8.49 AS Decimal(10, 2)), N'03162018230148PM471epson-252xl-black.jpg', N'rgb(65, 142, 244)', 1, 1)
INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (26, N'Epson 127 9-Pack Extra High Yield Remanufactured Ink Cartridges', 8, CAST(38.95 AS Decimal(10, 2)), N'03162018230949PM236epson_127_9_pack_ij.jpg', N'rgb(244, 66, 66)', 1, 1)
INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (27, N'HP 336 Black Original Ink Cartridge 5ml (C9362EE)', 6, CAST(15.83 AS Decimal(10, 2)), N'03162018234651PM366HP_336_OEM_3__20446.1486547225.jpg', N'rgb(115, 244, 65)', 1, 2)
INSERT [dbo].[Products] ([pro_id], [pro_name], [pro_quantity], [pro_price], [pro_img], [pro_color], [pro_status], [pro_brand_id]) VALUES (28, N'Epson 29XL Black High Capacity Replacement Ink Cartridge ', 18, CAST(10.00 AS Decimal(10, 2)), N'03162018232254PM025Black_Ink__86534.1519205409.jpg', N'rgb(65, 142, 244)', 0, 2)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (24, N'Admin')
INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (25, N'Subadmin')
INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (31, N'Editor')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[User_Id_And_Role_Id] ON 

INSERT [dbo].[User_Id_And_Role_Id] ([p_id], [p_user_id], [p_role_id]) VALUES (46, 23, 25)
INSERT [dbo].[User_Id_And_Role_Id] ([p_id], [p_user_id], [p_role_id]) VALUES (49, 22, 24)
INSERT [dbo].[User_Id_And_Role_Id] ([p_id], [p_user_id], [p_role_id]) VALUES (51, 26, 31)
SET IDENTITY_INSERT [dbo].[User_Id_And_Role_Id] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([user_id], [user_username], [user_email], [user_password], [user_img]) VALUES (22, N'sakit', N'sakit@gmail.com', N'3244185981728979115075721453575112', N'18032018122112983PMIMG_2544.JPG')
INSERT [dbo].[Users] ([user_id], [user_username], [user_email], [user_password], [user_img]) VALUES (23, N'terlan', N'terlan@gmail.com', N'3244185981728979115075721453575112', N'')
INSERT [dbo].[Users] ([user_id], [user_username], [user_email], [user_password], [user_img]) VALUES (26, N'orxan', N'orxan@gmail.com', N'3244185981728979115075721453575112', N'18032018131309222PM10645029_361998617282413_6991835763115688935_n.jpg')
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_order_status]  DEFAULT ((0)) FOR [order_status]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Products] FOREIGN KEY([cart_product_id])
REFERENCES [dbo].[Products] ([pro_id])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Products]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Users] FOREIGN KEY([cart_user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Users]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Products] FOREIGN KEY([order_product_id])
REFERENCES [dbo].[Products] ([pro_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([order_user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[Permission_Id_And_Role_Id]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Id_And_Role_Id_Permissions] FOREIGN KEY([p_permission_id])
REFERENCES [dbo].[Permissions] ([per_id])
GO
ALTER TABLE [dbo].[Permission_Id_And_Role_Id] CHECK CONSTRAINT [FK_Permission_Id_And_Role_Id_Permissions]
GO
ALTER TABLE [dbo].[Permission_Id_And_Role_Id]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Id_And_Role_Id_Roles] FOREIGN KEY([p_role_id])
REFERENCES [dbo].[Roles] ([role_id])
GO
ALTER TABLE [dbo].[Permission_Id_And_Role_Id] CHECK CONSTRAINT [FK_Permission_Id_And_Role_Id_Roles]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Brands] FOREIGN KEY([pro_brand_id])
REFERENCES [dbo].[Brands] ([brand_id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands]
GO
ALTER TABLE [dbo].[User_Id_And_Role_Id]  WITH CHECK ADD  CONSTRAINT [FK_User_Id_And_Role_Id_Roles] FOREIGN KEY([p_role_id])
REFERENCES [dbo].[Roles] ([role_id])
GO
ALTER TABLE [dbo].[User_Id_And_Role_Id] CHECK CONSTRAINT [FK_User_Id_And_Role_Id_Roles]
GO
ALTER TABLE [dbo].[User_Id_And_Role_Id]  WITH CHECK ADD  CONSTRAINT [FK_User_Id_And_Role_Id_Users] FOREIGN KEY([p_user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[User_Id_And_Role_Id] CHECK CONSTRAINT [FK_User_Id_And_Role_Id_Users]
GO
USE [master]
GO
ALTER DATABASE [IMS_DB] SET  READ_WRITE 
GO
