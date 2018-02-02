USE [master]
GO
/****** Object:  Database [EventManagemtSystem]    Script Date: 2/2/2018 11:51:47 AM ******/
CREATE DATABASE [EventManagemtSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EventManagemtSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\EventManagemtSystem.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EventManagemtSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\EventManagemtSystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EventManagemtSystem] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EventManagemtSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EventManagemtSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [EventManagemtSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EventManagemtSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EventManagemtSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EventManagemtSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EventManagemtSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EventManagemtSystem] SET  MULTI_USER 
GO
ALTER DATABASE [EventManagemtSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EventManagemtSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EventManagemtSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EventManagemtSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [EventManagemtSystem]
GO
/****** Object:  Table [dbo].[tblAdmin]    Script Date: 2/2/2018 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAdmin](
	[AdminID] [int] NOT NULL,
	[AdminName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Question] [nvarchar](100) NOT NULL,
	[Answer] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tblAdmin] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEvent]    Script Date: 2/2/2018 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEvent](
	[EventID] [int] IDENTITY(1,1) NOT NULL,
	[EventName] [nvarchar](50) NOT NULL,
	[EventStart] [datetime] NULL,
	[EventEnd] [datetime] NULL,
	[Specification] [nvarchar](500) NULL,
	[Venue] [nvarchar](100) NULL,
	[Flag] [int] NOT NULL,
 CONSTRAINT [PK_tblEvent] PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblImage]    Script Date: 2/2/2018 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblImage](
	[EventID] [int] NOT NULL,
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[ImagePath] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_tblImage] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[tblImage]  WITH CHECK ADD  CONSTRAINT [FK_tblImage_tblEvent] FOREIGN KEY([EventID])
REFERENCES [dbo].[tblEvent] ([EventID])
GO
ALTER TABLE [dbo].[tblImage] CHECK CONSTRAINT [FK_tblImage_tblEvent]
GO
USE [master]
GO
ALTER DATABASE [EventManagemtSystem] SET  READ_WRITE 
GO
