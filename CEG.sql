USE [master]
GO
DROP DATABASE IF EXISTS [ChildrenEnglishGame]
GO
/****** Object:  Database [ChildrenEnglishGame]    Script Date: 9/18/2024 12:13:58 PM ******/
CREATE DATABASE [ChildrenEnglishGame]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ChildrenEnglishGame', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ChildrenEnglishGame.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ChildrenEnglishGame_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ChildrenEnglishGame_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ChildrenEnglishGame] SET COMPATIBILITY_LEVEL = 150
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
ALTER DATABASE [ChildrenEnglishGame] SET QUERY_STORE = ON
GO
ALTER DATABASE [ChildrenEnglishGame] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ChildrenEnglishGame]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[fullname] [nvarchar](max) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[gender] [nvarchar](10) NOT NULL,
	[status] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[class_id] [int] IDENTITY(1,1) NOT NULL,
	[teacher_id] [int] NOT NULL,
	[course_id] [int] NOT NULL,
	[class_name] [nvarchar](10) NOT NULL,
	[minimum_students] [int] NULL,
	[maximum_students] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[total_hours] [int] NULL,
	[image] [nvarchar](max) NULL,
	[required_age] [int] NULL,
	[difficulty] [nvarchar](20) NULL,
	[category] [nvarchar](20) NULL,
 CONSTRAINT [PK_Class_1] PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[course_id] [int] IDENTITY(1,1) NOT NULL,
	[course_name] [nvarchar](50) NOT NULL,
	[course_type] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Course_1] PRIMARY KEY CLUSTERED 
(
	[course_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enroll]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enroll](
	[enroll_id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[enrolled_date] [datetime] NOT NULL,
 CONSTRAINT [PK_Enroll] PRIMARY KEY CLUSTERED 
(
	[enroll_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[game_id] [int] IDENTITY(1,1) NOT NULL,
	[game_config_id] [int] NOT NULL,
	[download_link] [nvarchar](max) NULL,
	[title] [nvarchar](50) NOT NULL,
	[point] [int] NULL,
	[status] [nvarchar](50) NULL,
	[type] [nvarchar](50) NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[game_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameConfig]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameConfig](
	[game_config_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[point] [int] NULL,
	[correct_answer] [nvarchar](50) NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_GameConfig] PRIMARY KEY CLUSTERED 
(
	[game_config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLevel]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLevel](
	[game_level_id] [int] IDENTITY(1,1) NOT NULL,
	[game_id] [int] NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_GameLevel] PRIMARY KEY CLUSTERED 
(
	[game_level_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Homework]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Homework](
	[homework_id] [int] IDENTITY(1,1) NOT NULL,
	[session_id] [int] NOT NULL,
	[game_config_id] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[total_point] [int] NULL,
	[word_amount] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Homework] PRIMARY KEY CLUSTERED 
(
	[homework_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkResult]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkResult](
	[homework_result_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NOT NULL,
	[student_progress_id] [int] NOT NULL,
	[total_point] [int] NULL,
	[word_amount] [int] NULL,
	[playtime] [time](7) NULL,
 CONSTRAINT [PK_HomeworkResult] PRIMARY KEY CLUSTERED 
(
	[homework_result_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parent]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parent](
	[parent_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](10) NULL,
	[address] [nvarchar](max) NULL,
 CONSTRAINT [PK_Parent] PRIMARY KEY CLUSTERED 
(
	[parent_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[parent_id] [int] NOT NULL,
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
/****** Object:  Table [dbo].[RegisteredClass]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisteredClass](
	[registered_class_id] [int] IDENTITY(1,1) NOT NULL,
	[class_id] [int] NOT NULL,
	[payment_id] [int] NOT NULL,
	[payment_status] [bit] NULL,
	[registered_date] [datetime] NULL,
	[confirm_date] [datetime] NULL,
 CONSTRAINT [PK_RegisteredCourse_1] PRIMARY KEY CLUSTERED 
(
	[registered_class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[session_id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NOT NULL,
	[title] [nvarchar](10) NOT NULL,
	[description] [nvarchar](max) NULL,
	[status] [nvarchar](50) NULL,
	[hours] [int] NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[student_id] [int] IDENTITY(1,1) NOT NULL,
	[parent_id] [int] NOT NULL,
	[account_id] [int] NOT NULL,
	[highscore] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[playtime] [int] NULL,
	[cur_level] [int] NULL,
	[points] [int] NULL,
	[age] [int] NULL,
	[birthdate] [datetime] NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Student_1] PRIMARY KEY CLUSTERED 
(
	[student_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentHomework]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentHomework](
	[student_homework_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NOT NULL,
	[student_progress_id] [int] NOT NULL,
	[total_point] [int] NOT NULL,
	[status] [nvarchar](50) NULL,
	[hours] [int] NULL,
 CONSTRAINT [PK_StudentHomework] PRIMARY KEY CLUSTERED 
(
	[student_homework_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentProgress]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentProgress](
	[student_progress_id] [int] NOT NULL,
	[student_id] [int] NOT NULL,
	[session_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[total_point] [int] NULL,
	[playtimes] [time](7) NULL,
 CONSTRAINT [PK_StudentProgress] PRIMARY KEY CLUSTERED 
(
	[student_progress_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 9/18/2024 12:13:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[teacher_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](50) NOT NULL,
	[address] [nvarchar](max) NOT NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[teacher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (1, 1, N'admin', N'admin', N'Sys Admin', CAST(N'2024-10-09T00:00:00.000' AS DateTime), N'Male', N'Active')
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'admin')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (2, N'student')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (3, N'teacher')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (4, N'parent')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Role]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Course] FOREIGN KEY([course_id])
REFERENCES [dbo].[Course] ([course_id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Course]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Teacher] FOREIGN KEY([teacher_id])
REFERENCES [dbo].[Teacher] ([teacher_id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Teacher]
GO
ALTER TABLE [dbo].[Enroll]  WITH CHECK ADD  CONSTRAINT [FK_Enroll_Student] FOREIGN KEY([student_id])
REFERENCES [dbo].[Student] ([student_id])
GO
ALTER TABLE [dbo].[Enroll] CHECK CONSTRAINT [FK_Enroll_Student]
GO
ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FK_Game_GameConfig] FOREIGN KEY([game_config_id])
REFERENCES [dbo].[GameConfig] ([game_config_id])
GO
ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FK_Game_GameConfig]
GO
ALTER TABLE [dbo].[GameLevel]  WITH CHECK ADD  CONSTRAINT [FK_GameLevel_Game] FOREIGN KEY([game_id])
REFERENCES [dbo].[Game] ([game_id])
GO
ALTER TABLE [dbo].[GameLevel] CHECK CONSTRAINT [FK_GameLevel_Game]
GO
ALTER TABLE [dbo].[Homework]  WITH CHECK ADD  CONSTRAINT [FK_Homework_GameConfig] FOREIGN KEY([game_config_id])
REFERENCES [dbo].[GameConfig] ([game_config_id])
GO
ALTER TABLE [dbo].[Homework] CHECK CONSTRAINT [FK_Homework_GameConfig]
GO
ALTER TABLE [dbo].[Homework]  WITH CHECK ADD  CONSTRAINT [FK_Homework_Session] FOREIGN KEY([session_id])
REFERENCES [dbo].[Session] ([session_id])
GO
ALTER TABLE [dbo].[Homework] CHECK CONSTRAINT [FK_Homework_Session]
GO
ALTER TABLE [dbo].[HomeworkResult]  WITH CHECK ADD  CONSTRAINT [FK_HomeworkResult_Homework] FOREIGN KEY([homework_id])
REFERENCES [dbo].[Homework] ([homework_id])
GO
ALTER TABLE [dbo].[HomeworkResult] CHECK CONSTRAINT [FK_HomeworkResult_Homework]
GO
ALTER TABLE [dbo].[HomeworkResult]  WITH CHECK ADD  CONSTRAINT [FK_HomeworkResult_StudentProgress] FOREIGN KEY([student_progress_id])
REFERENCES [dbo].[StudentProgress] ([student_progress_id])
GO
ALTER TABLE [dbo].[HomeworkResult] CHECK CONSTRAINT [FK_HomeworkResult_StudentProgress]
GO
ALTER TABLE [dbo].[Parent]  WITH CHECK ADD  CONSTRAINT [FK_Parents_Accounts] FOREIGN KEY([account_id])
REFERENCES [dbo].[Accounts] ([account_id])
GO
ALTER TABLE [dbo].[Parent] CHECK CONSTRAINT [FK_Parents_Accounts]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Parents] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Parent] ([parent_id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Parents]
GO
ALTER TABLE [dbo].[RegisteredClass]  WITH CHECK ADD  CONSTRAINT [FK_RegisteredCourse_Course] FOREIGN KEY([class_id])
REFERENCES [dbo].[Course] ([course_id])
GO
ALTER TABLE [dbo].[RegisteredClass] CHECK CONSTRAINT [FK_RegisteredCourse_Course]
GO
ALTER TABLE [dbo].[RegisteredClass]  WITH CHECK ADD  CONSTRAINT [FK_RegisteredCourse_Payment] FOREIGN KEY([payment_id])
REFERENCES [dbo].[Payment] ([payment_id])
GO
ALTER TABLE [dbo].[RegisteredClass] CHECK CONSTRAINT [FK_RegisteredCourse_Payment]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Course] FOREIGN KEY([course_id])
REFERENCES [dbo].[Course] ([course_id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Course]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Accounts] FOREIGN KEY([account_id])
REFERENCES [dbo].[Accounts] ([account_id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Accounts]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Parents] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Parent] ([parent_id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Parents]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_Homework] FOREIGN KEY([homework_id])
REFERENCES [dbo].[Homework] ([homework_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_Homework]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_StudentProgress] FOREIGN KEY([student_progress_id])
REFERENCES [dbo].[StudentProgress] ([student_progress_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_StudentProgress]
GO
ALTER TABLE [dbo].[StudentProgress]  WITH CHECK ADD  CONSTRAINT [FK_StudentProgress_Class] FOREIGN KEY([class_id])
REFERENCES [dbo].[Class] ([class_id])
GO
ALTER TABLE [dbo].[StudentProgress] CHECK CONSTRAINT [FK_StudentProgress_Class]
GO
ALTER TABLE [dbo].[StudentProgress]  WITH CHECK ADD  CONSTRAINT [FK_StudentProgress_Session] FOREIGN KEY([session_id])
REFERENCES [dbo].[Session] ([session_id])
GO
ALTER TABLE [dbo].[StudentProgress] CHECK CONSTRAINT [FK_StudentProgress_Session]
GO
ALTER TABLE [dbo].[StudentProgress]  WITH CHECK ADD  CONSTRAINT [FK_StudentProgress_Student] FOREIGN KEY([student_id])
REFERENCES [dbo].[Student] ([student_id])
GO
ALTER TABLE [dbo].[StudentProgress] CHECK CONSTRAINT [FK_StudentProgress_Student]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_Accounts] FOREIGN KEY([account_id])
REFERENCES [dbo].[Accounts] ([account_id])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_Accounts]
GO
USE [master]
GO
ALTER DATABASE [ChildrenEnglishGame] SET  READ_WRITE 
GO
