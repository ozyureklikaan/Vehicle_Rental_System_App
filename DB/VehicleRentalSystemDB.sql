USE [master]
GO
/****** Object:  Database [vehicleRentalSystem]    Script Date: 23.05.2019 14:23:03 ******/
CREATE DATABASE [vehicleRentalSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'vehicleRentalSystem', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\vehicleRentalSystem.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'vehicleRentalSystem_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\vehicleRentalSystem_log.ldf' , SIZE = 2304KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [vehicleRentalSystem] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [vehicleRentalSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [vehicleRentalSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [vehicleRentalSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [vehicleRentalSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [vehicleRentalSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [vehicleRentalSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [vehicleRentalSystem] SET  MULTI_USER 
GO
ALTER DATABASE [vehicleRentalSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [vehicleRentalSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [vehicleRentalSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [vehicleRentalSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [vehicleRentalSystem] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'vehicleRentalSystem', N'ON'
GO
ALTER DATABASE [vehicleRentalSystem] SET QUERY_STORE = OFF
GO
USE [vehicleRentalSystem]
GO
/****** Object:  Table [dbo].[tCompany]    Script Date: 23.05.2019 14:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tCompany](
	[companyID] [int] IDENTITY(1,1) NOT NULL,
	[companyName] [nvarchar](50) NULL,
	[password] [nchar](10) NULL,
	[city] [nvarchar](50) NULL,
	[address] [nvarchar](200) NULL,
	[vehicleNumber] [int] NULL,
	[companyScore] [int] NULL,
 CONSTRAINT [PK_tCompany] PRIMARY KEY CLUSTERED 
(
	[companyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tPersons]    Script Date: 23.05.2019 14:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tPersons](
	[personID] [int] IDENTITY(1,1) NOT NULL,
	[personName] [nvarchar](50) NULL,
	[personLastName] [nvarchar](50) NULL,
	[age] [int] NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
 CONSTRAINT [PK_tPersons] PRIMARY KEY CLUSTERED 
(
	[personID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRentalTransactions]    Script Date: 23.05.2019 14:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRentalTransactions](
	[rentalID] [int] IDENTITY(1,1) NOT NULL,
	[personID] [int] NULL,
	[vehicleID] [int] NULL,
	[rentingDate] [datetime] NULL,
 CONSTRAINT [PK_tRental] PRIMARY KEY CLUSTERED 
(
	[rentalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tVehicle]    Script Date: 23.05.2019 14:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tVehicle](
	[vehicleID] [int] IDENTITY(1,1) NOT NULL,
	[vehicleBrand] [nvarchar](50) NULL,
	[vehicleModel] [nvarchar](50) NULL,
	[inputDate] [datetime] NULL,
	[outputDate] [datetime] NULL,
	[requiredAgeOfDrivingLicense] [int] NULL,
	[minimumAgeLimit] [int] NULL,
	[dailyKilometerLimit] [int] NULL,
	[instantKilometerOfTheVehicle] [int] NULL,
	[airbag] [bit] NULL,
	[baggageVolume] [int] NULL,
	[numberOfSeats] [int] NULL,
	[dailyRentPrice] [money] NULL,
	[leasingStatus] [bit] NULL,
	[companyID] [int] NULL,
 CONSTRAINT [PK_tVehicle] PRIMARY KEY CLUSTERED 
(
	[vehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tRentalTransactions]  WITH CHECK ADD  CONSTRAINT [FK_tRentalTransactions_tPersons] FOREIGN KEY([personID])
REFERENCES [dbo].[tPersons] ([personID])
GO
ALTER TABLE [dbo].[tRentalTransactions] CHECK CONSTRAINT [FK_tRentalTransactions_tPersons]
GO
ALTER TABLE [dbo].[tRentalTransactions]  WITH CHECK ADD  CONSTRAINT [FK_tRentalTransactions_tVehicle] FOREIGN KEY([vehicleID])
REFERENCES [dbo].[tVehicle] ([vehicleID])
GO
ALTER TABLE [dbo].[tRentalTransactions] CHECK CONSTRAINT [FK_tRentalTransactions_tVehicle]
GO
ALTER TABLE [dbo].[tVehicle]  WITH CHECK ADD  CONSTRAINT [FK_tVehicle_tCompany] FOREIGN KEY([companyID])
REFERENCES [dbo].[tCompany] ([companyID])
GO
ALTER TABLE [dbo].[tVehicle] CHECK CONSTRAINT [FK_tVehicle_tCompany]
GO
USE [master]
GO
ALTER DATABASE [vehicleRentalSystem] SET  READ_WRITE 
GO
