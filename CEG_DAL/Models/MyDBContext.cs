using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CEG_DAL.Models;

public partial class MyDBContext : DbContext
{
    public MyDBContext()
    {
    }

    public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enroll> Enrolls { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameConfig> GameConfigs { get; set; }

    public virtual DbSet<GameLevel> GameLevels { get; set; }

    public virtual DbSet<Homework> Homeworks { get; set; }

    public virtual DbSet<HomeworkAnswer> HomeworkAnswers { get; set; }

    public virtual DbSet<HomeworkQuestion> HomeworkQuestions { get; set; }

    public virtual DbSet<HomeworkResult> HomeworkResults { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentHomework> StudentHomeworks { get; set; }

    public virtual DbSet<StudentProgress> StudentProgresses { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Fullname).HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_Role");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK_Class_1");

            entity.ToTable("Class");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(10)
                .HasColumnName("class_name");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.MaximumStudents).HasColumnName("maximum_students");
            entity.Property(e => e.MinimumStudents).HasColumnName("minimum_students");
            entity.Property(e => e.NumberOfStudents).HasColumnName("number_of_students");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_Course1");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_Teacher");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_Course_1");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Category)
                .HasMaxLength(20)
                .HasColumnName("category");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("course_name");
            entity.Property(e => e.CourseType)
                .HasMaxLength(50)
                .HasColumnName("course_type");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Difficulty)
                .HasMaxLength(20)
                .HasColumnName("difficulty");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.RequiredAge).HasColumnName("required_age");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TotalHours).HasColumnName("total_hours");
        });

        modelBuilder.Entity<Enroll>(entity =>
        {
            entity.ToTable("Enroll");

            entity.Property(e => e.EnrollId).HasColumnName("enroll_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.EnrolledDate)
                .HasColumnType("datetime")
                .HasColumnName("enrolled_date");
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("datetime")
                .HasColumnName("registration_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

            entity.HasOne(d => d.Class).WithMany(p => p.Enrolls)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enroll_Class");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrolls)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enroll_Student");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Enrolls)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enroll_Transaction");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("Game");

            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.DownloadLink).HasColumnName("download_link");
            entity.Property(e => e.GameConfigId).HasColumnName("game_config_id");
            entity.Property(e => e.Point).HasColumnName("point");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.GameConfig).WithMany(p => p.Games)
                .HasForeignKey(d => d.GameConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Game_GameConfig");
        });

        modelBuilder.Entity<GameConfig>(entity =>
        {
            entity.ToTable("GameConfig");

            entity.Property(e => e.GameConfigId).HasColumnName("game_config_id");
            entity.Property(e => e.CorrectAnswer).HasColumnName("correct_answer");
            entity.Property(e => e.Point).HasColumnName("point");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<GameLevel>(entity =>
        {
            entity.ToTable("GameLevel");

            entity.Property(e => e.GameLevelId).HasColumnName("game_level_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Game).WithMany(p => p.GameLevels)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameLevel_Game");
        });

        modelBuilder.Entity<Homework>(entity =>
        {
            entity.ToTable("Homework");

            entity.Property(e => e.HomeworkId).HasColumnName("homework_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.GameConfigId).HasColumnName("game_config_id");
            entity.Property(e => e.Hours).HasColumnName("hours");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.GameConfig).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.GameConfigId)
                .HasConstraintName("FK_Homework_GameConfig");

            entity.HasOne(d => d.Session).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Homework_Session");
        });

        modelBuilder.Entity<HomeworkAnswer>(entity =>
        {
            entity.ToTable("HomeworkAnswer");

            entity.Property(e => e.HomeworkAnswerId).HasColumnName("homework_answer_id");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.HomeworkQuestionId).HasColumnName("homework_question_id");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.HomeworkQuestion).WithMany(p => p.HomeworkAnswers)
                .HasForeignKey(d => d.HomeworkQuestionId)
                .HasConstraintName("FK_HomeworkAnswer_HomeworkQuestion");
        });

        modelBuilder.Entity<HomeworkQuestion>(entity =>
        {
            entity.HasKey(e => e.HomeworkQuestionId).HasName("PK_HomeworkData");

            entity.ToTable("HomeworkQuestion");

            entity.Property(e => e.HomeworkQuestionId).HasColumnName("homework_question_id");
            entity.Property(e => e.HomeworkId).HasColumnName("homework_id");
            entity.Property(e => e.Question).HasColumnName("question");

            entity.HasOne(d => d.Homework).WithMany(p => p.HomeworkQuestions)
                .HasForeignKey(d => d.HomeworkId)
                .HasConstraintName("FK_HomeworkQuestion_Homework");
        });

        modelBuilder.Entity<HomeworkResult>(entity =>
        {
            entity.ToTable("HomeworkResult");

            entity.Property(e => e.HomeworkResultId).HasColumnName("homework_result_id");
            entity.Property(e => e.HomeworkId).HasColumnName("homework_id");
            entity.Property(e => e.Playtime).HasColumnName("playtime");
            entity.Property(e => e.StudentProgressId).HasColumnName("student_progress_id");
            entity.Property(e => e.TotalPoint).HasColumnName("total_point");
            entity.Property(e => e.WordAmount).HasColumnName("word_amount");

            entity.HasOne(d => d.Homework).WithMany(p => p.HomeworkResults)
                .HasForeignKey(d => d.HomeworkId)
                .HasConstraintName("FK_HomeworkResult_Homework");

            entity.HasOne(d => d.StudentProgress).WithMany(p => p.HomeworkResults)
                .HasForeignKey(d => d.StudentProgressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HomeworkResult_StudentProgress");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.ToTable("Parent");

            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");

            entity.HasOne(d => d.Account).WithMany(p => p.Parents)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Parents_Accounts");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Hours).HasColumnName("hours");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Course).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_Course");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK_Student_1");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Birthdate)
                .HasColumnType("datetime")
                .HasColumnName("birthdate");
            entity.Property(e => e.CurLevel).HasColumnName("cur_level");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Highscore).HasColumnName("highscore");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Playtime).HasColumnName("playtime");
            entity.Property(e => e.Points).HasColumnName("points");

            entity.HasOne(d => d.Account).WithMany(p => p.Students)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Accounts");

            entity.HasOne(d => d.Parent).WithMany(p => p.Students)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Parents");
        });

        modelBuilder.Entity<StudentHomework>(entity =>
        {
            entity.ToTable("StudentHomework");

            entity.Property(e => e.StudentHomeworkId).HasColumnName("student_homework_id");
            entity.Property(e => e.HomeworkId).HasColumnName("homework_id");
            entity.Property(e => e.Hours).HasColumnName("hours");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.StudentProgressId).HasColumnName("student_progress_id");
            entity.Property(e => e.TotalPoint).HasColumnName("total_point");

            entity.HasOne(d => d.Homework).WithMany(p => p.StudentHomeworks)
                .HasForeignKey(d => d.HomeworkId)
                .HasConstraintName("FK_StudentHomework_Homework");

            entity.HasOne(d => d.StudentProgress).WithMany(p => p.StudentHomeworks)
                .HasForeignKey(d => d.StudentProgressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentHomework_StudentProgress");
        });

        modelBuilder.Entity<StudentProgress>(entity =>
        {
            entity.ToTable("StudentProgress");

            entity.Property(e => e.StudentProgressId).HasColumnName("student_progress_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Playtimes).HasColumnName("playtimes");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TotalPoint).HasColumnName("total_point");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentProgresses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentProgress_Class");

            entity.HasOne(d => d.Session).WithMany(p => p.StudentProgresses)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentProgress_Session");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentProgresses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentProgress_Student");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("Teacher");

            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Certificate).HasColumnName("certificate");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");

            entity.HasOne(d => d.Account).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teacher_Accounts");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_Transaction_1");

            entity.ToTable("Transaction");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.ConfirmDate)
                .HasColumnType("datetime")
                .HasColumnName("confirm_date");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.TransactionAmount).HasColumnName("transaction_amount");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");
            entity.Property(e => e.TransactionStatus).HasColumnName("transaction_status");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .HasColumnName("transaction_type");
            entity.Property(e => e.VnpayId).HasColumnName("vnpay_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_Parents");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
