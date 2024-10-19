USE [master]
GO
DROP DATABASE IF EXISTS [ChildrenEnglishGame]
GO
/****** Object:  Database [ChildrenEnglishGame]    Script Date: 10/17/2024 9:25:50 PM ******/
USE [master]
GO
CREATE DATABASE [ChildrenEnglishGame]

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
/****** Object:  Table [dbo].[Accounts]    Script Date: 10/17/2024 9:25:50 PM ******/
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
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 10/17/2024 9:25:50 PM ******/
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
	[number_of_students] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Class_1] PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 10/17/2024 9:25:50 PM ******/
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
	[total_hours] [int] NULL,
	[image] [nvarchar](max) NULL,
	[required_age] [int] NULL,
	[difficulty] [nvarchar](20) NULL,
	[category] [nvarchar](20) NULL,
 CONSTRAINT [PK_Course_1] PRIMARY KEY CLUSTERED 
(
	[course_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enroll]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enroll](
	[enroll_id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[transaction_id] [int] NOT NULL,
	[registration_date] [datetime] NOT NULL,
	[enrolled_date] [datetime] NOT NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Enroll] PRIMARY KEY CLUSTERED 
(
	[enroll_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 10/17/2024 9:25:50 PM ******/
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
/****** Object:  Table [dbo].[GameConfig]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameConfig](
	[game_config_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[point] [int] NULL,
	[correct_answer] [nvarchar](max) NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_GameConfig] PRIMARY KEY CLUSTERED 
(
	[game_config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLevel]    Script Date: 10/17/2024 9:25:50 PM ******/
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
/****** Object:  Table [dbo].[Homework]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Homework](
	[homework_id] [int] IDENTITY(1,1) NOT NULL,
	[session_id] [int] NOT NULL,
	[title] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
	[game_config_id] [int] NULL,
	[status] [nvarchar](50) NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[hours] [int] NULL,
	[type] [nvarchar](50) NULL,
 CONSTRAINT [PK_Homework] PRIMARY KEY CLUSTERED 
(
	[homework_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkAnswer]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkAnswer](
	[homework_answer_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_question_id] [int] NULL,
	[answer] [nvarchar](max) NULL,
	[type] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_HomeworkAnswer] PRIMARY KEY CLUSTERED 
(
	[homework_answer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkQuestion]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkQuestion](
	[homework_question_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NULL,
	[question] [nvarchar](max) NULL,
 CONSTRAINT [PK_HomeworkData] PRIMARY KEY CLUSTERED 
(
	[homework_question_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkResult]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkResult](
	[homework_result_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NULL,
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
/****** Object:  Table [dbo].[Parent]    Script Date: 10/17/2024 9:25:50 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 10/17/2024 9:25:50 PM ******/
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
/****** Object:  Table [dbo].[Schedule]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[schedule_id] [int] NOT NULL,
	[session_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[day_of_week] [nvarchar](50) NOT NULL,
	[start_time] [time](7) NOT NULL,
	[end_time] [time](7) NOT NULL,
	[status] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[session_id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
	[status] [nvarchar](50) NULL,
	[hours] [int] NULL,
	[number] [int] NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[student_id] [int] IDENTITY(1,1) NOT NULL,
	[parent_id] [int] NOT NULL,
	[account_id] [int] NOT NULL,
	[total_point] [int] NOT NULL,
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
/****** Object:  Table [dbo].[StudentHomework]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentHomework](
	[student_homework_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NULL,
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
/****** Object:  Table [dbo].[StudentProgress]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentProgress](
	[student_progress_id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NOT NULL,
	[session_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[total_point] [int] NULL,
	[playtimes] [int] NULL,
 CONSTRAINT [PK_StudentProgress] PRIMARY KEY CLUSTERED 
(
	[student_progress_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[teacher_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](50) NOT NULL,
	[certificate] [nvarchar](max) NULL,
	[address] [nvarchar](max) NOT NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[teacher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 10/17/2024 9:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[transaction_id] [int] IDENTITY(1,1) NOT NULL,
	[parent_id] [int] NOT NULL,
	[vnpay_id] [nvarchar](max) NOT NULL,
	[transaction_amount] [int] NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[transaction_status] [nvarchar](max) NOT NULL,
	[transaction_type] [nvarchar](50) NOT NULL,
	[confirm_date] [datetime] NOT NULL,
 CONSTRAINT [PK_Transaction_1] PRIMARY KEY CLUSTERED 
(
	[transaction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (1, 1, N'admin', N'pass123', N'Sys Admin', CAST(N'2024-09-10T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (2, 3, N'HaiNT2', N'pass123', N'Nguyen Thanh Hai', CAST(N'2024-09-12T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (3, 4, N'ViDT3', N'pass123', N'Dang Thuy Vi', CAST(N'2024-09-12T00:00:00.000' AS DateTime), N'Female', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (4, 2, N'HienCQ4', N'pass123', N'Co Quoc Hien', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (5, 4, N'KhoaPA5', N'pass123', N'Pham Anh Khoa', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (6, 3, N'MinhLT6', N'pass123', N'Lam The Minh', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (7, 4, N'DuyDNM7', N'pass123', N'Dang Nguyen Minh Duy', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (8, 2, N'NganVT8', N'pass123', N'Vo Thuy Ngan', CAST(N'2024-09-14T00:00:00.000' AS DateTime), N'Female', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (9, 2, N'UyenDNM9', N'pass123', N'Do Ngoc My Uyen', CAST(N'2024-09-16T00:00:00.000' AS DateTime), N'Female', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (10, 4, N'NganLT10', N'pass123', N'Le Thu Ngan', CAST(N'2024-09-16T00:00:00.000' AS DateTime), N'Female', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (11, 2, N'DucPM11', N'pass123', N'Pham Minh Duc', CAST(N'2024-09-17T00:00:00.000' AS DateTime), N'Female', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (12, 3, N'ThongHH12', N'pass123', N'Ho Hieu Thong', CAST(N'2024-09-17T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (13, 4, N'NamMN13', N'pass123', N'Mai Nhat Nam', CAST(N'2024-09-17T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (14, 2, N'TuMNT14', N'pass123', N'Mai Nguyen Tuan Tu', CAST(N'2024-09-19T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (15, 4, N'DuyBD15', N'pass123', N'Bui Duc Duy', CAST(N'2024-09-19T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (16, 3, N'MinhNN16', N'pass123', N'Nguyen Ngoc Minh', CAST(N'2024-09-19T00:00:00.000' AS DateTime), N'Female', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (17, 4, N'KhanhHN17', N'pass123', N'Ho Ngoc Khanh', CAST(N'2024-09-20T00:00:00.000' AS DateTime), N'Female', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (18, 2, N'MaiPTN18', N'pass123', N'Pham Thi Ngoc Mai', CAST(N'2024-09-21T00:00:00.000' AS DateTime), N'Female', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (19, 2, N'KimHN19', N'pass123', N'Ho Ngoc Kim', CAST(N'2024-09-23T00:00:00.000' AS DateTime), N'Female', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (20, 4, N'KhoaBA20', N'pass123', N'Bui Anh Khoa', CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Male', N'Active')
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (21, 2, N'SonNT21', N'pass123', N'Ngugyen Truc Son', CAST(N'2024-09-26T00:00:00.000' AS DateTime), N'Female', N'Active')
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Class] ON 

INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [status]) VALUES (1, 1, 1, N'6th Grade', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), N'Available')
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [status]) VALUES (2, 1, 1, N'6th Grade', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), N'Available')
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [status]) VALUES (3, 1, 1, N'6th Grade', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), N'Available')
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [status]) VALUES (4, 1, 1, N'6th Grade', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), N'Available')
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [status]) VALUES (5, 1, 1, N'6th Grade', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), N'Available')
SET IDENTITY_INSERT [dbo].[Class] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (1, N'Middle School', N'unknown', N'Middleschool Course', N'Available', 56, NULL, 12, N'Beginner', N'Middle School')
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (2, N'High School', N'unknown', N'Highschool Course', N'Draft', 56, NULL, 15, N'Intermediate', N'High School')
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[Enroll] ON 

INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (1, 1, 1, 1, CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (2, 2, 1, 2, CAST(N'2024-09-21T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (3, 3, 1, 3, CAST(N'2024-09-22T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (4, 4, 1, 4, CAST(N'2024-09-23T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (5, 5, 1, 5, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
SET IDENTITY_INSERT [dbo].[Enroll] OFF
GO
SET IDENTITY_INSERT [dbo].[Homework] ON 

INSERT [dbo].[Homework] ([homework_id], [session_id], [title], [description], [game_config_id], [status], [start_date], [end_date], [hours], [type]) VALUES (1, 1, N'Basic Homework', N'Learn the Basic', NULL, N'Available', CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 1, N'Vocabulary')
INSERT [dbo].[Homework] ([homework_id], [session_id], [title], [description], [game_config_id], [status], [start_date], [end_date], [hours], [type]) VALUES (2, 1, N'Standard Homework', N'Get used to English', NULL, N'Available', CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 1, N'Vocabulary')
SET IDENTITY_INSERT [dbo].[Homework] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeworkAnswer] ON 

INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (1, 1, N'to', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (2, 1, N'top', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (3, 1, N'off', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (4, 1, N'with', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (5, 2, N'of', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (6, 2, N'with', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (7, 2, N'at', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (8, 2, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (9, 3, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (10, 3, N'of', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (11, 3, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (12, 3, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (13, 4, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (14, 4, N'in', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (15, 4, N'with', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (16, 4, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (17, 5, N'of', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (18, 5, N'off', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (19, 5, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (20, 5, N'the', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (21, 6, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (22, 6, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (23, 6, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (24, 6, N'about', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (25, 7, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (26, 7, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (27, 7, N'for', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (28, 7, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (29, 8, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (30, 8, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (31, 8, N'for', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (32, 8, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (33, 9, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (34, 9, N'for', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (35, 9, N'not', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (36, 9, N'the', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (37, 10, N'with', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (38, 10, N'off', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (39, 10, N'them', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (40, 10, N'of', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (41, 11, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (42, 11, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (43, 11, N'about', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (44, 11, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (45, 12, N'to', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (46, 12, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (47, 12, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (48, 12, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (49, 13, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (50, 13, N'with', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (51, 13, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (52, 13, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (53, 14, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (54, 14, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (55, 14, N'of', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (56, 14, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (57, 15, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (58, 15, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (59, 15, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (60, 15, N'about', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (61, 16, N'in', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (62, 16, N'for', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (63, 16, N'with', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (64, 16, N'of', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (65, 17, N'about', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (66, 17, N'on', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (67, 17, N'in', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (68, 17, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (69, 18, N'for', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (70, 18, N'about', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (71, 18, N'in', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (72, 18, N'at', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (73, 19, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (74, 19, N'with', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (75, 19, N'for', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (76, 19, N'in', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (77, 20, N'on', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (78, 20, N'to', N'Incorrect')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (79, 20, N'for', N'Correct')
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (80, 20, N'with', N'Incorrect')
SET IDENTITY_INSERT [dbo].[HomeworkAnswer] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeworkQuestion] ON 

INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (1, 1, N'I want _ go home.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (2, 1, N'She is good _ dancing.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (3, 1, N'He is afraid _ the dark.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (4, 1, N'They are interested _ learning new things.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (5, 1, N'She is tired _ studying.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (6, 1, N'I am excited _ the trip.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (7, 1, N'He is famous _ his paintings.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (8, 1, N'We are late _ the meeting.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (9, 1, N'I am responsible _ organizing the event.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (10, 1, N'They are proud _ their accomplishments.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (11, 1, N'She is worried _ the exam.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (12, 1, N'He is married _ a doctor.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (13, 1, N'I am bored _ this movie.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (14, 1, N'She is jealous _ her friend’s success.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (15, 1, N'He is angry _ the delay.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (16, 1, N'They are ready _ the party.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (17, 1, N'I am addicted _ video games.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (18, 1, N'She is good _ solving problems.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (19, 1, N'We are familiar _ this software.')
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (20, 1, N'He is famous _ being an actor.')
SET IDENTITY_INSERT [dbo].[HomeworkQuestion] OFF
GO
SET IDENTITY_INSERT [dbo].[Parent] ON 

INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (1, 3, N'vidt@gmail.com', N'0938743302', N'District12')
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (2, 5, N'khoapa@gmail.com', N'0939956001', N'District12')
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (3, 7, N'duydnm@gmail.com', N'0999558026', N'District12')
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (4, 10, N'nganlt@gmail.com', N'0939438834', N'District12')
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (5, 13, N'nammn@gmail.com', N'0935420752', N'District12')
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (6, 15, N'duybd@gmail.com', N'0983822065', N'District12')
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (7, 17, N'khanhhn@gmail.com', N'0938220659', N'District12')
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (8, 20, N'khoaba@gmail.com', N'0973344678', N'District12')
SET IDENTITY_INSERT [dbo].[Parent] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (2, N'Student')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (3, N'Teacher')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (4, N'Parent')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Session] ON 

INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [status], [hours], [number]) VALUES (1, 1, N'Basic', N'kkkkk', N'Available', 2, 20)
SET IDENTITY_INSERT [dbo].[Session] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [total_point], [description], [playtime], [cur_level], [points], [age], [birthdate], [image]) VALUES (1, 1, 4, 0, N'Dang Thuy Vi', 0, 0, 0, 13, CAST(N'2011-03-22T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [total_point], [description], [playtime], [cur_level], [points], [age], [birthdate], [image]) VALUES (2, 2, 8, 0, N'Pham Anh Khoa', 0, 0, 0, 14, CAST(N'2010-05-03T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [total_point], [description], [playtime], [cur_level], [points], [age], [birthdate], [image]) VALUES (3, 3, 9, 0, N'Dang Nguyen Minh Duy', 0, 0, 0, 13, CAST(N'2011-01-14T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [total_point], [description], [playtime], [cur_level], [points], [age], [birthdate], [image]) VALUES (4, 4, 11, 0, N'Le Thu Ngan', 0, 0, 0, 15, CAST(N'2009-02-14T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [total_point], [description], [playtime], [cur_level], [points], [age], [birthdate], [image]) VALUES (5, 5, 14, 0, N'Mai Nhat Nam', 0, 0, 0, 13, CAST(N'2011-04-17T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [total_point], [description], [playtime], [cur_level], [points], [age], [birthdate], [image]) VALUES (6, 6, 18, 0, N'Bui Duc Duy', 0, 0, 0, 12, CAST(N'2011-11-19T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [total_point], [description], [playtime], [cur_level], [points], [age], [birthdate], [image]) VALUES (7, 7, 19, 0, N'Ho Ngoc Khanh', 0, 0, 0, 13, CAST(N'2011-06-24T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [total_point], [description], [playtime], [cur_level], [points], [age], [birthdate], [image]) VALUES (8, 8, 21, 0, N'Bui Anh Khoa', 0, 0, 0, 13, CAST(N'2011-01-30T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[StudentHomework] ON 

INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [total_point], [status], [hours]) VALUES (1, 1, 1, 100, N'Active', 12)
SET IDENTITY_INSERT [dbo].[StudentHomework] OFF
GO
SET IDENTITY_INSERT [dbo].[StudentProgress] ON 

INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [session_id], [class_id], [total_point], [playtimes]) VALUES (1, 1, 1, 2, 100, 1)
SET IDENTITY_INSERT [dbo].[StudentProgress] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (1, 2, N'haint@gmail.com', N'0912868225', NULL, N'District', NULL)
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (2, 6, N'minhlt@gmail.com', N'0938833549', NULL, N'District', NULL)
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (3, 12, N'thonghh@gmail.com', N'0912868225', NULL, N'District', NULL)
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (4, 16, N'minhnn@gmail.com', N'0984949957', NULL, N'District', NULL)
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 

INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (1, 1, N'VNPAY1', 100000, CAST(N'2024-09-20T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (2, 2, N'VNPAY2', 100000, CAST(N'2024-09-21T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-21T00:00:00.000' AS DateTime))
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (3, 3, N'VNPAY3', 100000, CAST(N'2024-09-22T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-22T00:00:00.000' AS DateTime))
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (4, 4, N'VNPAY4', 100000, CAST(N'2024-09-23T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-23T00:00:00.000' AS DateTime))
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (5, 5, N'VNPAY5', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Role]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Course1] FOREIGN KEY([course_id])
REFERENCES [dbo].[Course] ([course_id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Course1]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Teacher] FOREIGN KEY([teacher_id])
REFERENCES [dbo].[Teacher] ([teacher_id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Teacher]
GO
ALTER TABLE [dbo].[Enroll]  WITH CHECK ADD  CONSTRAINT [FK_Enroll_Class] FOREIGN KEY([class_id])
REFERENCES [dbo].[Class] ([class_id])
GO
ALTER TABLE [dbo].[Enroll] CHECK CONSTRAINT [FK_Enroll_Class]
GO
ALTER TABLE [dbo].[Enroll]  WITH CHECK ADD  CONSTRAINT [FK_Enroll_Student] FOREIGN KEY([student_id])
REFERENCES [dbo].[Student] ([student_id])
GO
ALTER TABLE [dbo].[Enroll] CHECK CONSTRAINT [FK_Enroll_Student]
GO
ALTER TABLE [dbo].[Enroll]  WITH CHECK ADD  CONSTRAINT [FK_Enroll_Transaction] FOREIGN KEY([transaction_id])
REFERENCES [dbo].[Transaction] ([transaction_id])
GO
ALTER TABLE [dbo].[Enroll] CHECK CONSTRAINT [FK_Enroll_Transaction]
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
ALTER TABLE [dbo].[HomeworkAnswer]  WITH CHECK ADD  CONSTRAINT [FK_HomeworkAnswer_HomeworkQuestion] FOREIGN KEY([homework_question_id])
REFERENCES [dbo].[HomeworkQuestion] ([homework_question_id])
GO
ALTER TABLE [dbo].[HomeworkAnswer] CHECK CONSTRAINT [FK_HomeworkAnswer_HomeworkQuestion]
GO
ALTER TABLE [dbo].[HomeworkQuestion]  WITH CHECK ADD  CONSTRAINT [FK_HomeworkQuestion_Homework] FOREIGN KEY([homework_id])
REFERENCES [dbo].[Homework] ([homework_id])
GO
ALTER TABLE [dbo].[HomeworkQuestion] CHECK CONSTRAINT [FK_HomeworkQuestion_Homework]
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
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Class] FOREIGN KEY([class_id])
REFERENCES [dbo].[Class] ([class_id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Class]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Session] FOREIGN KEY([session_id])
REFERENCES [dbo].[Session] ([session_id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Session]
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
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Parents] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Parent] ([parent_id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Parents]
GO
USE [master]
GO
ALTER DATABASE [ChildrenEnglishGame] SET  READ_WRITE 
GO
