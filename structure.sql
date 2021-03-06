USE [master]
GO
/****** Object:  Database [QuerySystem]    Script Date: 2022/4/28 下午 02:55:03 ******/
CREATE DATABASE [QuerySystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuerySystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuerySystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuerySystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuerySystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QuerySystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuerySystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuerySystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuerySystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuerySystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuerySystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuerySystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuerySystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuerySystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuerySystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuerySystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuerySystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuerySystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuerySystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuerySystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuerySystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuerySystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuerySystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuerySystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuerySystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuerySystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuerySystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuerySystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuerySystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuerySystem] SET RECOVERY FULL 
GO
ALTER DATABASE [QuerySystem] SET  MULTI_USER 
GO
ALTER DATABASE [QuerySystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuerySystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuerySystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuerySystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuerySystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuerySystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuerySystem', N'ON'
GO
ALTER DATABASE [QuerySystem] SET QUERY_STORE = OFF
GO
USE [QuerySystem]
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 2022/4/28 下午 02:55:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[ID] [uniqueidentifier] NOT NULL,
	[QuestionnaireID] [uniqueidentifier] NOT NULL,
	[PersonID] [uniqueidentifier] NOT NULL,
	[QuestionNo] [int] NOT NULL,
	[Answer] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 2022/4/28 下午 02:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[Mobile] [varchar](15) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Age] [varchar](3) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[QuestionnaireID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questionnaires]    Script Date: 2022/4/28 下午 02:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaires](
	[ID] [uniqueidentifier] NOT NULL,
	[QueryName] [nvarchar](100) NOT NULL,
	[QueryContent] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
	[StartTime] [date] NULL,
	[EndTime] [date] NULL,
	[IsExample] [bit] NOT NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Questionnaires] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 2022/4/28 下午 02:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[ID] [uniqueidentifier] NOT NULL,
	[QuestionnaireID] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[QuestionNo] [int] NOT NULL,
	[QuestionVal] [nvarchar](500) NOT NULL,
	[Selection] [nvarchar](500) NULL,
	[Necessary] [bit] NOT NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answers] ADD  CONSTRAINT [DF_Table_1_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [DF_Persons_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [DF_Persons_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Questionnaires] ADD  CONSTRAINT [DF_Questionnaires_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Questionnaires] ADD  CONSTRAINT [DF_Questionnaires_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Questions] ADD  CONSTRAINT [DF_Questions_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([ID])
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Table_1_Persons]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Questionnaires] FOREIGN KEY([QuestionnaireID])
REFERENCES [dbo].[Questionnaires] ([ID])
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Table_1_Questionnaires]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Questionnaires] FOREIGN KEY([QuestionnaireID])
REFERENCES [dbo].[Questionnaires] ([ID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Questionnaires]
GO
USE [master]
GO
ALTER DATABASE [QuerySystem] SET  READ_WRITE 
GO
