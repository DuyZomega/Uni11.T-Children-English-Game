USE [master]
GO
/****** Object:  Database [ChildrenEnglishGame]    Script Date: 26/8/2024 1:38:11 PM ******/
CREATE DATABASE [ChildrenEnglishGame]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ChildrenEnglishGame', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ChildrenEnglishGame.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ChildrenEnglishGame_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ChildrenEnglishGame_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ChildrenEnglishGame] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ChildrenEnglishGame].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ChildrenEnglishGame] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ARITHABORT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ChildrenEnglishGame] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ChildrenEnglishGame] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ChildrenEnglishGame] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ChildrenEnglishGame] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET RECOVERY FULL 
GO
ALTER DATABASE [ChildrenEnglishGame] SET  MULTI_USER 
GO
ALTER DATABASE [ChildrenEnglishGame] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ChildrenEnglishGame] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ChildrenEnglishGame] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ChildrenEnglishGame] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ChildrenEnglishGame', N'ON'
GO
ALTER DATABASE [ChildrenEnglishGame] SET QUERY_STORE = ON
GO
ALTER DATABASE [ChildrenEnglishGame] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ChildrenEnglishGame]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[admin_id] [int] NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[class_id] [int] NOT NULL,
	[teacher_id] [int] NOT NULL,
	[class_name] [nvarchar](10) NOT NULL,
	[number_of_students] [int] NULL,
 CONSTRAINT [PK_Class_1] PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[course_id] [int] NOT NULL,
	[teacher_id] [int] NOT NULL,
	[course_name] [nvarchar](50) NOT NULL,
	[course_type] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Course_1] PRIMARY KEY CLUSTERED 
(
	[course_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[game_id] [int] NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[point] [int] NULL,
	[status] [nvarchar](50) NULL,
	[type] [nvarchar](50) NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[game_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Homework]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Homework](
	[homework_id] [int] NOT NULL,
	[course_id] [int] NOT NULL,
	[game_id] [int] NOT NULL,
	[title] [nvarchar](10) NOT NULL,
	[total_point] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Homework] PRIMARY KEY CLUSTERED 
(
	[homework_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parents]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parents](
	[parents_id] [int] NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[fullname] [nvarchar](100) NOT NULL,
	[phone] [nvarchar](10) NULL,
	[address] [nvarchar](max) NULL,
	[gender] [nvarchar](10) NULL,
	[join_date] [datetime] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Parents] PRIMARY KEY CLUSTERED 
(
	[parents_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[payment_id] [int] NOT NULL,
	[parents_id] [int] NOT NULL,
	[payment_date] [datetime] NOT NULL,
	[payment_status] [bit] NOT NULL,
	[payment_type] [nvarchar](50) NOT NULL,
	[confirm_date] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment_1] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegisteredCourse]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisteredCourse](
	[registered_course_id] [int] NOT NULL,
	[course_id] [int] NOT NULL,
	[payment_id] [int] NOT NULL,
	[payment_status] [bit] NULL,
	[registered_date] [datetime] NULL,
	[confirm_date] [datetime] NULL,
 CONSTRAINT [PK_RegisteredCourse_1] PRIMARY KEY CLUSTERED 
(
	[registered_course_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[student_id] [int] NOT NULL,
	[parents_id] [int] NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[highscore] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[class_id] [int] NOT NULL,
	[playtime] [int] NULL,
	[cur_level] [int] NULL,
	[points] [int] NULL,
	[age] [int] NULL,
	[statuc] [nvarchar](50) NULL,
 CONSTRAINT [PK_Student_1] PRIMARY KEY CLUSTERED 
(
	[student_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentHomework]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentHomework](
	[student_homework_id] [int] NOT NULL,
	[homework_id] [int] NOT NULL,
	[student_process_id] [int] NOT NULL,
	[total_point] [int] NOT NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_StudentHomework] PRIMARY KEY CLUSTERED 
(
	[student_homework_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentProcess]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentProcess](
	[student_process_id] [int] NOT NULL,
	[student_id] [int] NOT NULL,
	[registered_course_id] [int] NOT NULL,
	[total_point] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Table_2] PRIMARY KEY CLUSTERED 
(
	[student_process_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 26/8/2024 1:38:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[teacher_id] [int] NOT NULL,
	[teacher_name] [nvarchar](max) NOT NULL,
	[course_id] [nchar](10) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](50) NOT NULL,
	[address] [nvarchar](max) NOT NULL,
	[gender] [nvarchar](10) NOT NULL,
	[join_date] [datetime] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[teacher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Teacher] FOREIGN KEY([teacher_id])
REFERENCES [dbo].[Teacher] ([teacher_id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Teacher]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Teacher] FOREIGN KEY([teacher_id])
REFERENCES [dbo].[Teacher] ([teacher_id])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Teacher]
GO
ALTER TABLE [dbo].[Homework]  WITH CHECK ADD  CONSTRAINT [FK_Homework_Course] FOREIGN KEY([course_id])
REFERENCES [dbo].[Course] ([course_id])
GO
ALTER TABLE [dbo].[Homework] CHECK CONSTRAINT [FK_Homework_Course]
GO
ALTER TABLE [dbo].[Homework]  WITH CHECK ADD  CONSTRAINT [FK_Homework_Game] FOREIGN KEY([game_id])
REFERENCES [dbo].[Game] ([game_id])
GO
ALTER TABLE [dbo].[Homework] CHECK CONSTRAINT [FK_Homework_Game]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Parents] FOREIGN KEY([parents_id])
REFERENCES [dbo].[Parents] ([parents_id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Parents]
GO
ALTER TABLE [dbo].[RegisteredCourse]  WITH CHECK ADD  CONSTRAINT [FK_RegisteredCourse_Course] FOREIGN KEY([course_id])
REFERENCES [dbo].[Course] ([course_id])
GO
ALTER TABLE [dbo].[RegisteredCourse] CHECK CONSTRAINT [FK_RegisteredCourse_Course]
GO
ALTER TABLE [dbo].[RegisteredCourse]  WITH CHECK ADD  CONSTRAINT [FK_RegisteredCourse_Payment] FOREIGN KEY([payment_id])
REFERENCES [dbo].[Payment] ([payment_id])
GO
ALTER TABLE [dbo].[RegisteredCourse] CHECK CONSTRAINT [FK_RegisteredCourse_Payment]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Class] FOREIGN KEY([class_id])
REFERENCES [dbo].[Class] ([class_id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Class]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Parents] FOREIGN KEY([parents_id])
REFERENCES [dbo].[Parents] ([parents_id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Parents]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_Homework] FOREIGN KEY([homework_id])
REFERENCES [dbo].[Homework] ([homework_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_Homework]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_StudentProcess] FOREIGN KEY([student_process_id])
REFERENCES [dbo].[StudentProcess] ([student_process_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_StudentProcess]
GO
ALTER TABLE [dbo].[StudentProcess]  WITH CHECK ADD  CONSTRAINT [FK_StudentProcess_RegisteredCourse] FOREIGN KEY([registered_course_id])
REFERENCES [dbo].[RegisteredCourse] ([registered_course_id])
GO
ALTER TABLE [dbo].[StudentProcess] CHECK CONSTRAINT [FK_StudentProcess_RegisteredCourse]
GO
ALTER TABLE [dbo].[StudentProcess]  WITH CHECK ADD  CONSTRAINT [FK_StudentProcess_Student] FOREIGN KEY([student_id])
REFERENCES [dbo].[Student] ([student_id])
GO
ALTER TABLE [dbo].[StudentProcess] CHECK CONSTRAINT [FK_StudentProcess_Student]
GO
USE [master]
GO
ALTER DATABASE [ChildrenEnglishGame] SET  READ_WRITE 
GO
