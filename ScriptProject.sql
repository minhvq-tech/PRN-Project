USE [master]
GO
/****** Object:  Database [dbBlogs]    Script Date: 11/6/2021 10:13:40 PM ******/
CREATE DATABASE [dbBlogs]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbBlogs', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\dbBlogs.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'dbBlogs_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\dbBlogs_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dbBlogs] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbBlogs].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbBlogs] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbBlogs] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbBlogs] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbBlogs] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbBlogs] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbBlogs] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbBlogs] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbBlogs] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbBlogs] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbBlogs] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbBlogs] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbBlogs] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbBlogs] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbBlogs] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbBlogs] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbBlogs] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbBlogs] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbBlogs] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbBlogs] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbBlogs] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbBlogs] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbBlogs] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbBlogs] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbBlogs] SET  MULTI_USER 
GO
ALTER DATABASE [dbBlogs] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbBlogs] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbBlogs] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbBlogs] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [dbBlogs] SET DELAYED_DURABILITY = DISABLED 
GO
USE [dbBlogs]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 11/6/2021 10:13:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](150) NULL,
	[Password] [nvarchar](50) NULL,
	[Phone] [varchar](11) NULL,
	[Salt] [varchar](10) NULL,
	[Active] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[RoleID] [int] NULL,
	[LastLogin] [datetime] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/6/2021 10:13:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[Alias] [nvarchar](255) NULL,
	[MetaDesc] [nvarchar](255) NULL,
	[MeteKey] [nvarchar](255) NULL,
	[Thumb] [nvarchar](255) NULL,
	[Published] [bit] NOT NULL,
	[Ordering] [int] NULL,
	[Parents] [int] NULL,
	[Levels] [int] NULL,
	[Icon] [nvarchar](255) NULL,
	[Cover] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 11/6/2021 10:13:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[AccountID] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Posts]    Script Date: 11/6/2021 10:13:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Contents] [nvarchar](max) NULL,
	[Thumb] [nvarchar](255) NULL,
	[Published] [bit] NOT NULL,
	[Alias] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[AccountID] [int] NULL,
	[Author] [nvarchar](255) NULL,
	[Tags] [nvarchar](max) NULL,
	[SContents] [nvarchar](255) NULL,
	[CatID] [int] NULL,
	[isHot] [bit] NULL,
	[isNewfeed] [bit] NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/6/2021 10:13:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[RoleDescription] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Roles]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Accounts] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Accounts]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Accounts] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Accounts]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Categories] FOREIGN KEY([CatID])
REFERENCES [dbo].[Categories] ([CatID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Categories]
GO
USE [master]
GO
ALTER DATABASE [dbBlogs] SET  READ_WRITE 
GO
