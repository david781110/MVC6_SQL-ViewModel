USE [master]
GO
/****** Object:  Database [MVC_UserDB2]    Script Date: 2023/6/26 下午 05:31:38 ******/
CREATE DATABASE [MVC_UserDB2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MVC_UserDB2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MVC_UserDB2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MVC_UserDB2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MVC_UserDB2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MVC_UserDB2] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MVC_UserDB2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MVC_UserDB2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET ARITHABORT OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MVC_UserDB2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MVC_UserDB2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MVC_UserDB2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MVC_UserDB2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET RECOVERY FULL 
GO
ALTER DATABASE [MVC_UserDB2] SET  MULTI_USER 
GO
ALTER DATABASE [MVC_UserDB2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MVC_UserDB2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MVC_UserDB2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MVC_UserDB2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MVC_UserDB2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MVC_UserDB2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MVC_UserDB2', N'ON'
GO
ALTER DATABASE [MVC_UserDB2] SET QUERY_STORE = OFF
GO
USE [MVC_UserDB2]
GO
/****** Object:  Table [dbo].[DepartmentTable2]    Script Date: 2023/6/26 下午 05:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentTable2](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](50) NULL,
	[DepartmentY] [nvarchar](50) NULL,
 CONSTRAINT [PK_DepartmentTable2] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTable2]    Script Date: 2023/6/26 下午 05:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTable2](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserSex] [nchar](10) NULL,
	[UserBirthDay] [datetime2](7) NULL,
	[UserMobilePhone] [nvarchar](50) NULL,
	[UserApproved] [bit] NULL,
	[DepartmentId] [int] NULL,
 CONSTRAINT [PK_UserTable2] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DepartmentTable2] ON 

INSERT [dbo].[DepartmentTable2] ([DepartmentId], [DepartmentName], [DepartmentY]) VALUES (1, N'資訊管理系', N'四年制')
INSERT [dbo].[DepartmentTable2] ([DepartmentId], [DepartmentName], [DepartmentY]) VALUES (2, N'財務金融系', N'四年制')
INSERT [dbo].[DepartmentTable2] ([DepartmentId], [DepartmentName], [DepartmentY]) VALUES (3, N'行銷管理系', N'四年制')
INSERT [dbo].[DepartmentTable2] ([DepartmentId], [DepartmentName], [DepartmentY]) VALUES (4, N'商管所碩士', N'二年制')
INSERT [dbo].[DepartmentTable2] ([DepartmentId], [DepartmentName], [DepartmentY]) VALUES (5, N'資工所博士班', N'七年制')
SET IDENTITY_INSERT [dbo].[DepartmentTable2] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTable2] ON 

INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (1, N'MIS', N'M         ', CAST(N'2023-01-23T00:00:00.0000000' AS DateTime2), N'0980818017', 1, 1)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (2, N'Mwqe', N'M         ', CAST(N'2021-05-06T00:00:00.0000000' AS DateTime2), N'0988222111', 1, 2)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (3, N'david', N'M         ', CAST(N'1933-01-01T00:00:00.0000000' AS DateTime2), N'0922888111', 1, 3)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (4, N'ijodiqo', N'F         ', CAST(N'1932-01-22T00:00:00.0000000' AS DateTime2), N'0933583111', 0, 4)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (5, N'ojoioi', N'F         ', CAST(N'1956-11-14T00:00:00.0000000' AS DateTime2), N'0984828191', 0, 5)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (6, N'ojoioi231', N'M         ', CAST(N'1956-11-14T00:00:00.0000000' AS DateTime2), N'0984828191', 0, 4)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (7, N'davidsad', N'M         ', CAST(N'1933-01-01T00:00:00.0000000' AS DateTime2), N'0933583111', 1, 2)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (8, N'MIS123', N'F         ', CAST(N'1956-11-14T00:00:00.0000000' AS DateTime2), N'0984828191', 1, 1)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (9, N'Mwqe', N'M         ', CAST(N'1933-01-01T00:00:00.0000000' AS DateTime2), N'0984828191', 0, 1)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (10, N'ojoioi', N'F         ', CAST(N'2021-05-06T00:00:00.0000000' AS DateTime2), N'0984828191', 1, 1)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (11, N'ojoioi', N'F         ', CAST(N'2021-05-06T00:00:00.0000000' AS DateTime2), N'0984828191', 1, 2)
INSERT [dbo].[UserTable2] ([UserId], [UserName], [UserSex], [UserBirthDay], [UserMobilePhone], [UserApproved], [DepartmentId]) VALUES (12, N'ojoioi', N'F         ', CAST(N'2021-05-06T00:00:00.0000000' AS DateTime2), N'0984828191', 1, 3)
SET IDENTITY_INSERT [dbo].[UserTable2] OFF
GO
ALTER TABLE [dbo].[UserTable2]  WITH CHECK ADD  CONSTRAINT [FK_UserTable2_DepartmentTable2] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[DepartmentTable2] ([DepartmentId])
GO
ALTER TABLE [dbo].[UserTable2] CHECK CONSTRAINT [FK_UserTable2_DepartmentTable2]
GO
USE [master]
GO
ALTER DATABASE [MVC_UserDB2] SET  READ_WRITE 
GO
