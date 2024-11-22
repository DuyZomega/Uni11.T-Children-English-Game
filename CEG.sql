USE [master]
GO
/****** Object:  Database [ChildrenEnglishGame]    Script Date: 08-Nov-24 18:58:16 ******/
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
EXEC sys.sp_db_vardecimal_storage_format N'ChildrenEnglishGame', N'ON'
GO
ALTER DATABASE [ChildrenEnglishGame] SET QUERY_STORE = ON
GO
ALTER DATABASE [ChildrenEnglishGame] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ChildrenEnglishGame]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 08-Nov-24 18:58:16 ******/
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
/****** Object:  Table [dbo].[Class]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[class_id] [int] IDENTITY(1,1) NOT NULL,
	[teacher_id] [int] NOT NULL,
	[course_id] [int] NOT NULL,
	[class_name] [nvarchar](max) NOT NULL,
	[minimum_students] [int] NULL,
	[maximum_students] [int] NULL,
	[number_of_students] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[enrollment_fee] [int] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Class_1] PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 08-Nov-24 18:58:16 ******/
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
/****** Object:  Table [dbo].[Enroll]    Script Date: 08-Nov-24 18:58:16 ******/
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
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Enroll] PRIMARY KEY CLUSTERED 
(
	[enroll_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 08-Nov-24 18:58:16 ******/
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
/****** Object:  Table [dbo].[GameConfig]    Script Date: 08-Nov-24 18:58:16 ******/
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
/****** Object:  Table [dbo].[GameLevel]    Script Date: 08-Nov-24 18:58:16 ******/
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
/****** Object:  Table [dbo].[Homework]    Script Date: 08-Nov-24 18:58:16 ******/
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
/****** Object:  Table [dbo].[HomeworkAnswer]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkAnswer](
	[homework_answer_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_question_id] [int] NOT NULL,
	[answer] [nvarchar](max) NULL,
	[type] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_HomeworkAnswer] PRIMARY KEY CLUSTERED 
(
	[homework_answer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkQuestion]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkQuestion](
	[homework_question_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NOT NULL,
	[question] [nvarchar](max) NULL,
 CONSTRAINT [PK_HomeworkData] PRIMARY KEY CLUSTERED 
(
	[homework_question_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkResult]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkResult](
	[homework_result_id] [int] IDENTITY(1,1) NOT NULL,
	[total_point] [int] NULL,
	[total_correct_answers] [int] NULL,
	[playtime] [time](7) NULL,
 CONSTRAINT [PK_HomeworkResult] PRIMARY KEY CLUSTERED 
(
	[homework_result_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parent]    Script Date: 08-Nov-24 18:58:16 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 08-Nov-24 18:58:16 ******/
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
/****** Object:  Table [dbo].[Schedule]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[schedule_id] [int] IDENTITY(1,1) NOT NULL,
	[session_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[schedule_date] [datetime] NULL,
	[start_time] [time](7) NULL,
	[end_time] [time](7) NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[session_id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
	[hours] [int] NULL,
	[session_number] [int] NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[student_id] [int] IDENTITY(1,1) NOT NULL,
	[parent_id] [int] NOT NULL,
	[account_id] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[cur_level] [int] NULL,
	[age] [int] NULL,
	[birthdate] [datetime] NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Student_1] PRIMARY KEY CLUSTERED 
(
	[student_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentAnswer]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAnswer](
	[student_answer_id] [int] IDENTITY(1,1) NOT NULL,
	[game_id] [int] NOT NULL,
	[student_homework_id] [int] NOT NULL,
	[answer] [nvarchar](max) NULL,
	[type] [nvarchar](max) NULL,
 CONSTRAINT [PK_StudentAnswer] PRIMARY KEY CLUSTERED 
(
	[student_answer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentHomework]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentHomework](
	[student_homework_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NOT NULL,
	[student_progress_id] [int] NOT NULL,
	[homework_result_id] [int] NOT NULL,
	[point] [int] NULL,
	[playtime] [time](7) NULL,
	[status] [nvarchar](50) NULL,
	[correct_answers] [int] NULL,
 CONSTRAINT [PK_StudentHomework] PRIMARY KEY CLUSTERED 
(
	[student_homework_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentProgress]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentProgress](
	[student_progress_id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NOT NULL,
	[total_point] [int] NULL,
	[playtime] [time](7) NULL,
 CONSTRAINT [PK_StudentProgress] PRIMARY KEY CLUSTERED 
(
	[student_progress_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 08-Nov-24 18:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[teacher_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](10) NOT NULL,
	[certificate] [nvarchar](max) NOT NULL,
	[address] [nvarchar](max) NOT NULL,
	[image] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[teacher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 08-Nov-24 18:58:16 ******/
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
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (1, 1, N'HoaDM1', N'pass123', N'Dao Minh Hoa', CAST(N'2024-09-10T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (2, 3, N'MinhDH2', N'pass123', N'Dang Hoang Minh', CAST(N'2024-09-12T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (3, 4, N'VinhBH3', N'pass123', N'Bui Huu Vinh', CAST(N'2024-09-12T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (4, 2, N'LinhNK4', N'pass123', N'Nguyen Khanh Linh', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (5, 4, N'KhoaPA5', N'pass123', N'Pham Anh Khoa', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (6, 3, N'MinhLT6', N'pass123', N'Lam The Minh', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (7, 4, N'DuyDNM7', N'pass123', N'Dang Nguyen Minh Duy', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (8, 2, N'NganVT8', N'pass123', N'Vo Thuy Ngan', CAST(N'2024-09-14T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (9, 2, N'UyenDNM9', N'pass123', N'Do Ngoc My Uyen', CAST(N'2024-09-16T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (10, 4, N'NganLT10', N'pass123', N'Le Thu Ngan', CAST(N'2024-09-16T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (11, 2, N'DucPM11', N'pass123', N'Pham Minh Duc', CAST(N'2024-09-17T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (12, 3, N'ThongHH12', N'pass123', N'Ho Hieu Thong', CAST(N'2024-09-17T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (13, 4, N'NamMN13', N'pass123', N'Mai Nhat Nam', CAST(N'2024-09-17T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (14, 2, N'TuMNT14', N'pass123', N'Mai Nguyen Tuan Tu', CAST(N'2024-09-19T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (15, 4, N'DuyBD15', N'pass123', N'Bui Duc Duy', CAST(N'2024-09-19T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (16, 3, N'MinhNN16', N'pass123', N'Nguyen Ngoc Minh', CAST(N'2024-09-19T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (17, 4, N'KhanhHN17', N'pass123', N'Ho Ngoc Khanh', CAST(N'2024-09-20T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (18, 2, N'MaiPTN18', N'pass123', N'Pham Thi Ngoc Mai', CAST(N'2024-09-21T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (19, 2, N'KimHN19', N'pass123', N'Ho Ngoc Kim', CAST(N'2024-09-23T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (20, 4, N'KhoaBA20', N'pass123', N'Bui Anh Khoa', CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (21, 2, N'SonNT21', N'pass123', N'Nguyen Truc Son', CAST(N'2024-09-26T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (22, 1, N'DuyHTK22', N'pass123', N'Ho Tran Khanh Duy', CAST(N'2024-09-27T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (23, 2, N'TuanHA23', N'pass123', N'Huynh Anh Tuan', CAST(N'2024-09-27T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (24, 2, N'TrinhLT24', N'pass123', N'Le Thi Trinh', CAST(N'2024-09-28T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (25, 2, N'BaoPN25', N'pass123', N'Pham Nguyen Bao', CAST(N'2024-09-28T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (26, 2, N'MinhDL26', N'pass123', N'Do Le Minh', CAST(N'2024-09-29T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (27, 2, N'LanTT27', N'pass123', N'Tran Thi Lan', CAST(N'2024-09-29T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (28, 2, N'CuongNV28', N'pass123', N'Nguyen Van Cuong', CAST(N'2024-09-30T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (29, 2, N'TrangTT29', N'pass123', N'Tran Thi Trang', CAST(N'2024-09-30T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (30, 2, N'KhanhND30', N'pass123', N'Nguyen Duc Khanh', CAST(N'2024-10-01T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (31, 2, N'ThuVN31', N'pass123', N'Vu Ngoc Thu', CAST(N'2024-10-01T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (32, 2, N'PhongTT32', N'pass123', N'Tran Tuan Phong', CAST(N'2024-10-02T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (33, 2, N'LinhNT33', N'pass123', N'Nguyen Thi Linh', CAST(N'2024-10-02T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (34, 2, N'TuanBT34', N'pass123', N'Bui Tuan Tu', CAST(N'2024-10-03T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (35, 2, N'ThuyLT35', N'pass123', N'Le Thi Thuy', CAST(N'2024-10-03T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (36, 2, N'KienPV36', N'pass123', N'Pham Van Kien', CAST(N'2024-10-04T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (37, 2, N'DuongTV37', N'pass123', N'Tran Van Duong', CAST(N'2024-10-04T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (38, 2, N'HieuNL38', N'pass123', N'Nguyen Le Hieu', CAST(N'2024-10-05T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (39, 2, N'HoaTL39', N'pass123', N'Tran Lan Hoa', CAST(N'2024-10-05T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (40, 2, N'ThanhPH40', N'pass123', N'Phan Huu Thanh', CAST(N'2024-10-06T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (41, 2, N'DiepNT41', N'pass123', N'Nguyen Thi Diep', CAST(N'2024-10-06T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (42, 2, N'TrungDN42', N'pass123', N'Dang Nguyen Trung', CAST(N'2024-10-07T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (43, 2, N'HangLH43', N'pass123', N'Le Hong Hang', CAST(N'2024-10-07T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (44, 2, N'ToanTB44', N'pass123', N'Tran Bao Toan', CAST(N'2024-10-08T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (45, 2, N'TrangLN45', N'pass123', N'Le Ngoc Trang', CAST(N'2024-10-08T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (46, 2, N'KhanhBT46', N'pass123', N'Bui Thi Khanh', CAST(N'2024-10-09T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (47, 2, N'ThuyNT47', N'pass123', N'Nguyen Thi Thuy', CAST(N'2024-10-09T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (48, 2, N'TuanHV48', N'pass123', N'Hoang Van Tuan', CAST(N'2024-10-10T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (49, 2, N'DuyenTD49', N'pass123', N'Tran Thi Duyen', CAST(N'2024-10-10T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (50, 2, N'HieuNV50', N'pass123', N'Nguyen Van Hieu', CAST(N'2024-10-11T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (51, 2, N'HoaLT51', N'pass123', N'Le Thi Hoa', CAST(N'2024-10-11T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (52, 2, N'ThanhTH52', N'pass123', N'Tran Huu Thanh', CAST(N'2024-10-12T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (53, 2, N'PhuongNN53', N'pass123', N'Nguyen Ngoc Phuong', CAST(N'2024-10-12T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (54, 2, N'CuongLB54', N'pass123', N'Le Bao Cuong', CAST(N'2024-10-13T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (55, 2, N'LinhNT55', N'pass123', N'Nguyen Thi Linh', CAST(N'2024-10-13T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (56, 2, N'HieuHN56', N'pass123', N'Hoang Ngoc Hieu', CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (57, 2, N'NgocLT57', N'pass123', N'Le Thanh Ngoc', CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (58, 2, N'HungVT58', N'pass123', N'Vo Thanh Hung', CAST(N'2024-10-15T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (59, 2, N'DucPV59', N'pass123', N'Pham Van Duc', CAST(N'2024-10-15T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (60, 2, N'KhoaLN60', N'pass123', N'Le Ngoc Khoa', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (61, 2, N'ThuyPT61', N'pass123', N'Pham Thanh Thuy', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (62, 4, N'HaiTA62', N'pass123', N'Tran Anh Hai', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (63, 4, N'AnhNL63', N'pass123', N'Nguyen Lan Anh', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Female', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (64, 4, N'ThienLH64', N'pass123', N'Le Ha Thien', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (65, 4, N'CuongVM65', N'pass123', N'Vu Manh Cuong', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (66, 4, N'KhaiTV66', N'pass123', N'Tran Van Khai', CAST(N'2024-10-18T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
INSERT [dbo].[Accounts] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [status]) VALUES (67, 4, N'ManhVV', N'pass123', N'Vu Van Manh', CAST(N'2024-10-18T00:00:00.000' AS DateTime), N'Male', N'Active')
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Class] ON 
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (1, 1, 1, N'Starter Class', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Ongoing')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (2, 1, 1, N'3 Weeks Course', 5, 20, 0, CAST(N'2024-10-01T00:00:00.000' AS DateTime), CAST(N'2024-10-22T00:00:00.000' AS DateTime), 1000000, N'Ended')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (3, 1, 1, N'Winter Class', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (4, 1, 1, N'Spring Class', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (5, 1, 1, N'New Year Class', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (6, 1, 1, N'New Semester Class', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Ongoing')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (7, 1, 1, N'Preparation Class', 5, 20, 0, CAST(N'2024-10-01T00:00:00.000' AS DateTime), CAST(N'2024-10-28T00:00:00.000' AS DateTime), 1000000, N'Ended')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (8, 1, 1, N'Advanced Class', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (9, 1, 1, N'Fall Class', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (10, 1, 1, N'Fresh Class', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
SET IDENTITY_INSERT [dbo].[Class] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (1, N'Vocabulary for Beginners', N'A1', N'Middle School Course', N'Available', 56, NULL, 11, N'Beginner', N'Vocabulary')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (2, N'Intermediate Grammar Course', N'B1', N'Middle School Course', N'Draft', 56, NULL, 12, N'Intermediate', N'Grammar')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (3, N'Intermediate Vocabulary Course', N'B1', N'Middle School Course', N'Available', 56, NULL, 12, N'Intermediate', N'Vocabulary')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (4, N'Grammar for Beginners', N'A2', N'Middle School Course', N'Available', 56, NULL, 11, N'Beginner', N'Grammar')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (5, N'Practice Pronunciation Course', N'A1', N'Middle School Course', N'Draft', 56, NULL, 12, N'Intermediate', N'Pronunciation')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (6, N'Pronunciation for Beginners', N'A2', N'Middle School Course', N'Available', 56, NULL, 11, N'Beginner', N'Pronunciation')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (7, N'English for High Intermediate', N'B2', N'Middle School Course', N'Draft', 56, NULL, 12, N'Intermediate', N'Pronunciation')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (8, N'Vocabulary for Beginners', N'A2', N'Middle School Course', N'Draft', 56, NULL, 11, N'Beginner', N'Vocabulary')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (9, N'English for High Intermediate', N'B2', N'Middle School Course', N'Draft', 56, NULL, 12, N'Intermediate', N'Pronunciation')
GO
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[Enroll] ON 
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (1, 1, 1, 1, CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (2, 2, 1, 2, CAST(N'2024-09-21T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (3, 3, 1, 3, CAST(N'2024-09-22T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (4, 4, 1, 4, CAST(N'2024-09-23T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (5, 5, 1, 5, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (6, 5, 1, 6, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (7, 6, 1, 7, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (8, 7, 1, 8, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (9, 8, 1, 9, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (10, 9, 1, 10, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (11, 10, 1, 11, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (12, 11, 1, 12, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (13, 12, 1, 13, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (14, 13, 1, 14, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (15, 14, 1, 15, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (16, 15, 1, 16, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
SET IDENTITY_INSERT [dbo].[Enroll] OFF
GO
SET IDENTITY_INSERT [dbo].[Homework] ON 
GO
INSERT [dbo].[Homework] ([homework_id], [session_id], [title], [description], [game_config_id], [start_date], [end_date], [hours], [type]) VALUES (1, 1, N'Basic Homework', N'Learn the Basic', NULL, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 1, N'Vocabulary')
GO
INSERT [dbo].[Homework] ([homework_id], [session_id], [title], [description], [game_config_id], [start_date], [end_date], [hours], [type]) VALUES (2, 1, N'Standard Homework', N'Get used to English', NULL, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 1, N'Vocabulary')
GO
SET IDENTITY_INSERT [dbo].[Homework] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeworkAnswer] ON 
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (1, 1, N'to', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (2, 1, N'top', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (3, 1, N'off', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (4, 1, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (5, 2, N'of', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (6, 2, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (7, 2, N'at', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (8, 2, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (9, 3, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (10, 3, N'of', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (11, 3, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (12, 3, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (13, 4, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (14, 4, N'in', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (15, 4, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (16, 4, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (17, 5, N'of', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (18, 5, N'off', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (19, 5, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (20, 5, N'the', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (21, 6, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (22, 6, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (23, 6, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (24, 6, N'about', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (25, 7, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (26, 7, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (27, 7, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (28, 7, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (29, 8, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (30, 8, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (31, 8, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (32, 8, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (33, 9, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (34, 9, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (35, 9, N'not', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (36, 9, N'the', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (37, 10, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (38, 10, N'off', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (39, 10, N'them', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (40, 10, N'of', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (41, 11, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (42, 11, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (43, 11, N'about', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (44, 11, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (45, 12, N'to', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (46, 12, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (47, 12, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (48, 12, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (49, 13, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (50, 13, N'with', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (51, 13, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (52, 13, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (53, 14, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (54, 14, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (55, 14, N'of', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (56, 14, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (57, 15, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (58, 15, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (59, 15, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (60, 15, N'about', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (61, 16, N'in', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (62, 16, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (63, 16, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (64, 16, N'of', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (65, 17, N'about', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (66, 17, N'on', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (67, 17, N'in', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (68, 17, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (69, 18, N'for', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (70, 18, N'about', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (71, 18, N'in', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (72, 18, N'at', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (73, 19, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (74, 19, N'with', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (75, 19, N'for', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (76, 19, N'in', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (77, 20, N'on', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (78, 20, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (79, 20, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (80, 20, N'with', N'Incorrect')
GO
SET IDENTITY_INSERT [dbo].[HomeworkAnswer] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeworkQuestion] ON 
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (1, 1, N'I want _ go home.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (2, 1, N'She is good _ dancing.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (3, 1, N'He is afraid _ the dark.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (4, 1, N'They are interested _ learning new things.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (5, 1, N'She is tired _ studying.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (6, 1, N'I am excited _ the trip.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (7, 1, N'He is famous _ his paintings.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (8, 1, N'We are late _ the meeting.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (9, 1, N'I am responsible _ organizing the event.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (10, 1, N'They are proud _ their accomplishments.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (11, 1, N'She is worried _ the exam.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (12, 1, N'He is married _ a doctor.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (13, 1, N'I am bored _ this movie.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (14, 1, N'She is jealous _ her friend’s success.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (15, 1, N'He is angry _ the delay.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (16, 1, N'They are ready _ the party.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (17, 1, N'I am addicted _ video games.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (18, 1, N'She is good _ solving problems.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (19, 1, N'We are familiar _ this software.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (20, 1, N'He is famous _ being an actor.')
GO
SET IDENTITY_INSERT [dbo].[HomeworkQuestion] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeworkResult] ON 
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (1, 1, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[HomeworkResult] OFF
GO
SET IDENTITY_INSERT [dbo].[Parent] ON 
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (1, 3, N'vinhbh@gmail.com', N'0938743302', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (2, 5, N'khoapa@gmail.com', N'0939956001', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (3, 7, N'duydnm@gmail.com', N'0999558026', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (4, 10, N'nganlt@gmail.com', N'0939438834', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (5, 13, N'nammn@gmail.com', N'0935420752', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (6, 15, N'duybd@gmail.com', N'0983822065', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (7, 17, N'khanhhn@gmail.com', N'0938220659', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (8, 20, N'khoaba@gmail.com', N'0973344678', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (9, 62, N'haita@gmail.com', N'0912838984', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (10, 63, N'anhnl@gmail.com', N'0987213462', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (11, 64, N'thienlh@gmail.com', N'0918829734', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (12, 65, N'cuongvm@gmail.com', N'0923028345', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (13, 66, N'khaitv@gmail.com', N'0923847435', N'District12')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (14, 67, N'manhvv@gmail.com', N'0912049208', N'District12')
GO
SET IDENTITY_INSERT [dbo].[Parent] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (2, N'Student')
GO
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (3, N'Teacher')
GO
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (4, N'Parent')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (1, 1, 1, CAST(N'2024-09-10T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (2, 2, 1, CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (3, 3, 1, CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Cancelled')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (4, 4, 1, CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (5, 5, 1, CAST(N'2024-11-03T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (6, 6, 1, CAST(N'2024-11-12T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (7, 7, 1, CAST(N'2024-12-13T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (8, 8, 1, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (9, 9, 1, CAST(N'2024-12-15T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (10, 10, 1, CAST(N'2024-12-16T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Session] ON 
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (1, 1, N'Basic', N'First Session', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (2, 1, N'Basic', N'Second Session', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (3, 1, N'Basic', N'Third Session', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (4, 1, N'Basic', N'Fourth Session', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (5, 1, N'Basic', N'Fifth Session', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (6, 1, N'Basic', N'Sixth Session', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (7, 1, N'Basic', N'Seventh Session', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (8, 1, N'Basic', N'Eighth Session', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (9, 1, N'Basic', N'Ninth Session', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (10, 1, N'Basic', N'Tenth Session', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (11, 2, N'Basic', N'First Session', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (12, 2, N'Basic', N'Second Session', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (13, 2, N'Basic', N'Third Session', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (14, 2, N'Basic', N'Fourth Session', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (15, 2, N'Basic', N'Fifth Session', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (16, 2, N'Basic', N'Sixth Session', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (17, 2, N'Basic', N'Seventh Session', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (18, 2, N'Basic', N'Eighth Session', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (19, 2, N'Basic', N'Ninth Session', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (20, 2, N'Basic', N'Tenth Session', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (21, 3, N'Basic', N'First Session', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (22, 3, N'Basic', N'Second Session', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (23, 3, N'Basic', N'Third Session', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (24, 3, N'Basic', N'Fourth Session', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (25, 3, N'Basic', N'Fifth Session', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (26, 3, N'Basic', N'Sixth Session', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (27, 3, N'Basic', N'Seventh Session', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (28, 3, N'Basic', N'Eighth Session', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (29, 3, N'Basic', N'Ninth Session', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (30, 3, N'Basic', N'Tenth Session', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (31, 4, N'Basic', N'First Session', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (32, 4, N'Basic', N'Second Session', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (33, 4, N'Basic', N'Third Session', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (34, 4, N'Basic', N'Fourth Session', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (35, 4, N'Basic', N'Fifth Session', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (36, 4, N'Basic', N'Sixth Session', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (37, 4, N'Basic', N'Seventh Session', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (38, 4, N'Basic', N'Eighth Session', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (39, 4, N'Basic', N'Ninth Session', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (40, 4, N'Basic', N'Tenth Session', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (41, 5, N'Basic', N'First Session', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (42, 5, N'Basic', N'Second Session', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (43, 5, N'Basic', N'Third Session', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (44, 5, N'Basic', N'Fourth Session', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (45, 5, N'Basic', N'Fifth Session', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (46, 5, N'Basic', N'Sixth Session', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (47, 5, N'Basic', N'Seventh Session', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (48, 5, N'Basic', N'Eighth Session', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (49, 5, N'Basic', N'Ninth Session', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (50, 5, N'Basic', N'Tenth Session', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (51, 6, N'Basic', N'First Session', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (52, 6, N'Basic', N'Second Session', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (53, 6, N'Basic', N'Third Session', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (54, 6, N'Basic', N'Fourth Session', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (55, 6, N'Basic', N'Fifth Session', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (56, 6, N'Basic', N'Sixth Session', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (57, 6, N'Basic', N'Seventh Session', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (58, 6, N'Basic', N'Eighth Session', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (59, 6, N'Basic', N'Ninth Session', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (60, 6, N'Basic', N'Tenth Session', 2, 10)
GO
SET IDENTITY_INSERT [dbo].[Session] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (1, 1, 4, N'Dang Thuy Vi', 0, 13, CAST(N'2011-03-22T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (2, 2, 8, N'Pham Anh Khoa', 0, 14, CAST(N'2010-05-03T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (3, 3, 9, N'Dang Nguyen Minh Duy', 0, 13, CAST(N'2011-01-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (4, 4, 11, N'Le Thu Ngan', 0, 15, CAST(N'2009-02-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (5, 5, 14, N'Mai Nhat Nam', 0, 13, CAST(N'2011-04-17T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (6, 6, 18, N'Bui Duc Duy', 0, 12, CAST(N'2011-11-19T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (7, 7, 19, N'Ho Ngoc Khanh', 0, 13, CAST(N'2011-06-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (8, 8, 21, N'Bui Anh Khoa', 0, 13, CAST(N'2011-01-30T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (9, 9, 23, N'Huynh Anh Tuan', 1, 14, CAST(N'2010-07-22T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (10, 10, 24, N'Le Thi Trinh', 0, 12, CAST(N'2012-10-11T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (11, 11, 25, N'Pham Nguyen Bao', 1, 11, CAST(N'2013-05-20T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (12, 12, 26, N'Do Le Minh', 0, 14, CAST(N'2010-04-08T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (13, 13, 27, N'Tran Thi Lan', 1, 13, CAST(N'2011-08-17T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (14, 14, 28, N'Nguyen Van Cuong', 2, 15, CAST(N'2009-09-23T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (15, 14, 29, N'Tran Thi Trang', 0, 12, CAST(N'2012-06-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (16, 9, 30, N'Nguyen Duc Khanh', 1, 14, CAST(N'2010-02-25T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (17, 12, 31, N'Vu Ngoc Thu', 1, 11, CAST(N'2013-12-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (18, 2, 32, N'Tran Tuan Phong', 0, 15, CAST(N'2009-01-30T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (19, 7, 33, N'Nguyen Thi Linh', 2, 13, CAST(N'2011-10-19T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (20, 8, 34, N'Bui Tuan Tu', 1, 12, CAST(N'2012-05-25T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (21, 9, 35, N'Le Thi Thuy', 0, 11, CAST(N'2013-03-08T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (22, 2, 36, N'Pham Van Kien', 1, 14, CAST(N'2010-11-30T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (23, 3, 37, N'Tran Van Duong', 2, 15, CAST(N'2009-08-22T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (24, 4, 38, N'Nguyen Le Hieu', 0, 13, CAST(N'2011-06-12T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (25, 3, 39, N'Tran Lan Hoa', 1, 12, CAST(N'2012-02-18T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (26, 11, 40, N'Phan Huu Thanh', 0, 15, CAST(N'2009-10-05T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (27, 10, 41, N'Nguyen Thi Diep', 1, 11, CAST(N'2013-01-16T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (28, 8, 42, N'Dang Nguyen Trung', 2, 14, CAST(N'2010-04-30T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (29, 5, 43, N'Le Hong Hang', 1, 12, CAST(N'2012-07-19T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (30, 4, 44, N'Tran Bao Toan', 0, 15, CAST(N'2009-12-25T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (31, 7, 45, N'Le Ngoc Trang', 1, 11, CAST(N'2013-11-11T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (32, 8, 46, N'Bui Thi Khanh', 1, 13, CAST(N'2011-04-03T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (33, 6, 47, N'Nguyen Thi Thuy', 2, 12, CAST(N'2012-09-29T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (34, 8, 48, N'Hoang Van Tuan', 1, 14, CAST(N'2010-05-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (35, 9, 49, N'Tran Thi Duyen', 0, 11, CAST(N'2013-03-28T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (36, 11, 50, N'Nguyen Van Hieu', 1, 15, CAST(N'2009-08-13T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (37, 6, 51, N'Le Thi Hoa', 0, 13, CAST(N'2011-01-04T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (38, 12, 52, N'Tran Huu Thanh', 1, 12, CAST(N'2012-06-09T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (39, 14, 53, N'Nguyen Ngoc Phuong', 2, 14, CAST(N'2010-10-21T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (40, 9, 54, N'Le Bao Cuong', 0, 11, CAST(N'2013-12-03T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (41, 13, 55, N'Nguyen Thi Linh', 1, 13, CAST(N'2011-11-18T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (42, 7, 56, N'Hoang Ngoc Hieu', 0, 12, CAST(N'2012-02-09T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (43, 5, 57, N'Le Thanh Ngoc', 1, 15, CAST(N'2009-07-20T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (44, 14, 58, N'Vo Thanh Hung', 2, 14, CAST(N'2010-09-05T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (45, 9, 59, N'Pham Van Duc', 1, 11, CAST(N'2013-05-27T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (46, 14, 60, N'Le Ngoc Khoa', 0, 12, CAST(N'2012-10-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (47, 13, 61, N'Pham Thanh Thuy', 0, 13, CAST(N'2011-03-15T00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[StudentHomework] ON 
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (1, 1, 1, 1, 100, NULL, N'StudentHomework', 5)
GO
SET IDENTITY_INSERT [dbo].[StudentHomework] OFF
GO
SET IDENTITY_INSERT [dbo].[StudentProgress] ON 
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [total_point], [playtime]) VALUES (1, 1, 100, NULL)
GO
SET IDENTITY_INSERT [dbo].[StudentProgress] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 
GO
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (1, 2, N'haint@gmail.com', N'0912868225', N'CertificatePlaceholder', N'District', N'ImagePlaceholder')
GO
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (2, 6, N'minhlt@gmail.com', N'0938833549', N'CertificatePlaceholder', N'District', N'ImagePlaceholder')
GO
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (3, 12, N'thonghh@gmail.com', N'0912868225', N'CertificatePlaceholder', N'District', N'ImagePlaceholder')
GO
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (4, 16, N'minhnn@gmail.com', N'0984949957', N'CertificatePlaceholder', N'District', N'ImagePlaceholder')
GO
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (1, 1, N'VNPAY1', 100000, CAST(N'2024-09-20T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-20T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (2, 2, N'VNPAY2', 100000, CAST(N'2024-09-21T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-21T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (3, 3, N'VNPAY3', 100000, CAST(N'2024-09-22T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-22T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (4, 4, N'VNPAY4', 100000, CAST(N'2024-09-23T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-23T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (5, 5, N'VNPAY5', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (6, 1, N'VNPAY6', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (7, 1, N'VNPAY7', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (8, 2, N'VNPAY8', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (9, 3, N'VNPAY9', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (10, 3, N'VNPAY10', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (11, 3, N'VNPAY11', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (12, 4, N'VNPAY12', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (13, 5, N'VNPAY13', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (14, 5, N'VNPAY14', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (15, 6, N'VNPAY15', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Transaction] ([transaction_id], [parent_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date]) VALUES (16, 6, N'VNPAY16', 100000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Transaction_status_placeholder', N'Fee', CAST(N'2024-09-24T00:00:00.000' AS DateTime))
GO
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
ALTER TABLE [dbo].[StudentAnswer]  WITH CHECK ADD  CONSTRAINT [FK_StudentAnswer_Game] FOREIGN KEY([game_id])
REFERENCES [dbo].[Game] ([game_id])
GO
ALTER TABLE [dbo].[StudentAnswer] CHECK CONSTRAINT [FK_StudentAnswer_Game]
GO
ALTER TABLE [dbo].[StudentAnswer]  WITH CHECK ADD  CONSTRAINT [FK_StudentAnswer_StudentHomework] FOREIGN KEY([student_homework_id])
REFERENCES [dbo].[StudentHomework] ([student_homework_id])
GO
ALTER TABLE [dbo].[StudentAnswer] CHECK CONSTRAINT [FK_StudentAnswer_StudentHomework]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_Homework] FOREIGN KEY([homework_id])
REFERENCES [dbo].[Homework] ([homework_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_Homework]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_HomeworkResult] FOREIGN KEY([homework_result_id])
REFERENCES [dbo].[HomeworkResult] ([homework_result_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_HomeworkResult]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_StudentProgress] FOREIGN KEY([student_progress_id])
REFERENCES [dbo].[StudentProgress] ([student_progress_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_StudentProgress]
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
