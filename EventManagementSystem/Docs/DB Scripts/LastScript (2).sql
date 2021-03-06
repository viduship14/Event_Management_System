USE [master]
GO
/****** Object:  Database [EventManagemtSystem]    Script Date: 2/2/2018 6:14:16 PM ******/
CREATE DATABASE [EventManagemtSystem]

USE [EventManagemtSystem]
Go



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
/****** Object:  Table [dbo].[tblEvent]    Script Date: 2/2/2018 6:14:17 PM ******/
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
/****** Object:  Table [dbo].[tblImage]    Script Date: 2/2/2018 6:14:17 PM ******/
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
