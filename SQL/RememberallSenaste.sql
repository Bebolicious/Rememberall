USE [master]
GO
/****** Object:  Database [Rememberall]    Script Date: 2019-01-11 15:29:41 ******/
CREATE DATABASE [Rememberall]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Rememberall', FILENAME = N'C:\Users\Administrator\Rememberall.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Rememberall_log', FILENAME = N'C:\Users\Administrator\Rememberall_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [Rememberall] SET AUTO_CLOSE ON 
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
ALTER DATABASE [Rememberall] SET  ENABLE_BROKER 
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
/****** Object:  Table [dbo].[Activities]    Script Date: 2019-01-11 15:29:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Activityname] [nvarchar](50) NULL,
 CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Alarms]    Script Date: 2019-01-11 15:29:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alarms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Alarmname] [nvarchar](50) NULL,
	[Alarmtime] [time](7) NULL,
	[DateId] [datetime] NULL,
	[ActivityId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Alarms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManyActivities]    Script Date: 2019-01-11 15:29:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManyActivities](
	[ActivityId] [int] NOT NULL,
	[ActivityDate] [datetime] NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_ManyActivities] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2019-01-11 15:29:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
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
SET IDENTITY_INSERT [dbo].[Activities] ON 

INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (1, N'Oscar12345666')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (2, N'hoppsan')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (3, N'Hejsanasd')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (4, N'hejjsd')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (5, N'asds')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (6, N'asds')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (7, N'123')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (8, N'123123')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (9, N'12312312')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (10, N'Letsgo')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (11, N'Hej123')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (12, N'Hej')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (13, N'Oscar')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (14, N'Test123')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (15, N'Test123')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (20, N'Checkpoint')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (21, N'A')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (22, N'Redovisning av projekt')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (23, N'Helg')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (26, N'Promenix')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (27, N'Lunch')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (28, N'RAAAAUUUUUSSS')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (29, N'C')
INSERT [dbo].[Activities] ([Id], [Activityname]) VALUES (35, N'Redovisning Checkpoint')
SET IDENTITY_INSERT [dbo].[Activities] OFF
SET IDENTITY_INSERT [dbo].[Alarms] ON 

INSERT [dbo].[Alarms] ([Id], [Alarmname], [Alarmtime], [DateId], [ActivityId], [UserId]) VALUES (1, N'Alarm', CAST(N'20:00:00' AS Time), CAST(N'2019-10-02T16:00:00.000' AS DateTime), NULL, 8)
INSERT [dbo].[Alarms] ([Id], [Alarmname], [Alarmtime], [DateId], [ActivityId], [UserId]) VALUES (4, N'Hejsan123', CAST(N'23:30:00' AS Time), CAST(N'2019-10-23T00:00:00.000' AS DateTime), NULL, 8)
INSERT [dbo].[Alarms] ([Id], [Alarmname], [Alarmtime], [DateId], [ActivityId], [UserId]) VALUES (5, N'Förebereda inför redovisning', CAST(N'14:15:00' AS Time), CAST(N'2019-01-11T15:15:00.000' AS DateTime), 35, 17)
SET IDENTITY_INSERT [dbo].[Alarms] OFF
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (1, CAST(N'2020-02-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (4, CAST(N'2019-01-20T22:00:00.000' AS DateTime), 1)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (5, CAST(N'2019-01-20T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (7, CAST(N'2019-01-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (8, CAST(N'2019-01-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (9, CAST(N'2019-01-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (10, CAST(N'2019-02-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (11, CAST(N'2019-02-10T00:00:00.000' AS DateTime), 5)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (12, CAST(N'2019-02-10T00:00:00.000' AS DateTime), 5)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (13, CAST(N'2019-12-24T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (20, CAST(N'2019-01-11T08:30:00.000' AS DateTime), 8)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (22, CAST(N'2019-01-11T15:00:00.000' AS DateTime), 8)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (23, CAST(N'2019-01-11T16:30:00.000' AS DateTime), 8)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (26, CAST(N'2019-01-18T16:00:00.000' AS DateTime), 8)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (27, CAST(N'2019-01-11T12:00:00.000' AS DateTime), 8)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (28, CAST(N'2019-01-11T14:35:00.000' AS DateTime), 8)
INSERT [dbo].[ManyActivities] ([ActivityId], [ActivityDate], [UserId]) VALUES (35, CAST(N'2019-01-11T15:15:00.000' AS DateTime), 17)
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (1, N'Oscar', N'Hejsan', N'Oscar', NULL)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (5, N'Oscar1234', N'c412b37f8c0484e6db8bce177ae88c5443b26e92', NULL, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (8, N'1', N'356a192b7913b04c54574d18c28d46e6395428ab', N'Oscar', N'Carlsson')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (9, N'Ogge', N'356a192b7913b04c54574d18c28d46e6395428ab', NULL, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (10, N'123', N'356a192b7913b04c54574d18c28d46e6395428ab', NULL, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (11, N'1234', N'356a192b7913b04c54574d18c28d46e6395428ab', NULL, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (12, N'12345', N'356a192b7913b04c54574d18c28d46e6395428ab', NULL, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (13, N'12356', N'356a192b7913b04c54574d18c28d46e6395428ab', NULL, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (14, N'Oskar', N'1617c49e07a9853d106add8391367f34fe201fac', NULL, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (16, N'', N'a4a40333e270e6ba8726b958f47293c81ab91bde', NULL, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Firstname], [Lastname]) VALUES (17, N'Oscar123456', N'356a192b7913b04c54574d18c28d46e6395428ab', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E4F3D5AE7F]    Script Date: 2019-01-11 15:29:41 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Alarms]  WITH CHECK ADD  CONSTRAINT [FK_Alarms_Activities] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Alarms] CHECK CONSTRAINT [FK_Alarms_Activities]
GO
ALTER TABLE [dbo].[Alarms]  WITH CHECK ADD  CONSTRAINT [FK_Alarms_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Alarms] CHECK CONSTRAINT [FK_Alarms_Users]
GO
ALTER TABLE [dbo].[ManyActivities]  WITH CHECK ADD  CONSTRAINT [FK_ManyActivities_Activities] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ManyActivities] CHECK CONSTRAINT [FK_ManyActivities_Activities]
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
