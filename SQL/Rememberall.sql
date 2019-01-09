USE [master]
GO
/****** Object:  Database [Rememberall]    Script Date: 2019-01-09 11:25:58 ******/
CREATE DATABASE [Rememberall]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Rememberall', FILENAME = N'C:\Users\Admin\Rememberall.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Rememberall_log', FILENAME = N'C:\Users\Admin\Rememberall_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Rememberall] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Rememberall].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Rememberall] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Rememberall] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Rememberall] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Rememberall] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Rememberall] SET ARITHABORT OFF 
GO
ALTER DATABASE [Rememberall] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Rememberall] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Rememberall] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Rememberall] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Rememberall] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Rememberall] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Rememberall] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Rememberall] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Rememberall] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Rememberall] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Rememberall] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Rememberall] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Rememberall] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Rememberall] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Rememberall] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Rememberall] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Rememberall] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Rememberall] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Rememberall] SET  MULTI_USER 
GO
ALTER DATABASE [Rememberall] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Rememberall] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Rememberall] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Rememberall] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Rememberall] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Rememberall] SET QUERY_STORE = OFF
GO
USE [Rememberall]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Rememberall]
GO
/****** Object:  Table [dbo].[Activities]    Script Date: 2019-01-09 11:25:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activities](
	[Id] [int] NOT NULL,
	[Activityname] [nvarchar](50) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Alarms]    Script Date: 2019-01-09 11:25:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alarms](
	[Id] [int] NOT NULL,
	[Alarmname] [nvarchar](50) NULL,
	[Alarmtime] [time](7) NULL,
	[DateId] [int] NULL,
	[ActivityId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Alarms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Date]    Script Date: 2019-01-09 11:25:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Date](
	[Id] [int] NOT NULL,
	[WeekId] [int] NULL,
	[Fulldate] [date] NULL,
 CONSTRAINT [PK_Date] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManyActivities]    Script Date: 2019-01-09 11:25:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManyActivities](
	[ActivityId] [int] NOT NULL,
	[DateId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_ManyActivities] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC,
	[DateId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2019-01-09 11:25:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Activities]  WITH CHECK ADD  CONSTRAINT [FK_Activities_Alarms] FOREIGN KEY([Id])
REFERENCES [dbo].[Alarms] ([Id])
GO
ALTER TABLE [dbo].[Activities] CHECK CONSTRAINT [FK_Activities_Alarms]
GO
ALTER TABLE [dbo].[Alarms]  WITH CHECK ADD  CONSTRAINT [FK_Alarms_Date] FOREIGN KEY([DateId])
REFERENCES [dbo].[Date] ([Id])
GO
ALTER TABLE [dbo].[Alarms] CHECK CONSTRAINT [FK_Alarms_Date]
GO
ALTER TABLE [dbo].[Alarms]  WITH CHECK ADD  CONSTRAINT [FK_Alarms_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Alarms] CHECK CONSTRAINT [FK_Alarms_Users]
GO
ALTER TABLE [dbo].[ManyActivities]  WITH CHECK ADD  CONSTRAINT [FK_ManyActivities_Activities] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activities] ([Id])
GO
ALTER TABLE [dbo].[ManyActivities] CHECK CONSTRAINT [FK_ManyActivities_Activities]
GO
ALTER TABLE [dbo].[ManyActivities]  WITH CHECK ADD  CONSTRAINT [FK_ManyActivities_Date] FOREIGN KEY([DateId])
REFERENCES [dbo].[Date] ([Id])
GO
ALTER TABLE [dbo].[ManyActivities] CHECK CONSTRAINT [FK_ManyActivities_Date]
GO
ALTER TABLE [dbo].[ManyActivities]  WITH CHECK ADD  CONSTRAINT [FK_ManyActivities_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ManyActivities] CHECK CONSTRAINT [FK_ManyActivities_Users]
GO
USE [master]
GO
ALTER DATABASE [Rememberall] SET  READ_WRITE 
GO
