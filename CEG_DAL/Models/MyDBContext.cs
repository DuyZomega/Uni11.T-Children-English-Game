﻿using System;
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

    public virtual DbSet<HomeworkResult> HomeworkResults { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<RegisteredCourse> RegisteredCourses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentHomework> StudentHomeworks { get; set; }

    public virtual DbSet<StudentProcess> StudentProcesses { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Uid=sa;Pwd=1;Database=ChildrenEnglishGame;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("account_id");
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
                .HasMaxLength(10)
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

            entity.Property(e => e.ClassId)
                .ValueGeneratedNever()
                .HasColumnName("class_id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(10)
                .HasColumnName("class_name");
            entity.Property(e => e.NumberOfStudents).HasColumnName("number_of_students");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_Teacher");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_Course_1");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("course_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("course_name");
            entity.Property(e => e.CourseType)
                .HasMaxLength(50)
                .HasColumnName("course_type");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.NumberOfStudent).HasColumnName("number_of_student");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Class).WithMany(p => p.Courses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Class");

            entity.HasOne(d => d.ClassNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Teacher");
        });

        modelBuilder.Entity<Enroll>(entity =>
        {
            entity.ToTable("Enroll");

            entity.Property(e => e.EnrollId)
                .ValueGeneratedNever()
                .HasColumnName("enroll_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.EnrolledDate)
                .HasColumnType("datetime")
                .HasColumnName("enrolled_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrolls)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enroll_Student");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("Game");

            entity.Property(e => e.GameId)
                .ValueGeneratedNever()
                .HasColumnName("game_id");
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
                .HasConstraintName("FK_Game_GameConfig");
        });

        modelBuilder.Entity<GameConfig>(entity =>
        {
            entity.ToTable("GameConfig");

            entity.Property(e => e.GameConfigId)
                .ValueGeneratedNever()
                .HasColumnName("game_config_id");
            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(50)
                .HasColumnName("correct_answer");
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

            entity.Property(e => e.GameLevelId)
                .ValueGeneratedNever()
                .HasColumnName("game_level_id");
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

            entity.Property(e => e.HomeworkId)
                .ValueGeneratedNever()
                .HasColumnName("homework_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.GameConfigId).HasColumnName("game_config_id");
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
            entity.Property(e => e.TotalPoint).HasColumnName("total_point");
            entity.Property(e => e.WordAmount).HasColumnName("word_amount");

            entity.HasOne(d => d.GameConfig).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.GameConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Homework_GameConfig");

            entity.HasOne(d => d.Session).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Homework_Course");
        });

        modelBuilder.Entity<HomeworkResult>(entity =>
        {
            entity.ToTable("HomeworkResult");

            entity.Property(e => e.HomeworkResultId)
                .ValueGeneratedNever()
                .HasColumnName("homework_result_id");
            entity.Property(e => e.HomeworkId).HasColumnName("homework_id");
            entity.Property(e => e.Playtime).HasColumnName("playtime");
            entity.Property(e => e.StudentProcessId).HasColumnName("student_process_id");
            entity.Property(e => e.TotalPoint).HasColumnName("total_point");
            entity.Property(e => e.WordAmount).HasColumnName("word_amount");

            entity.HasOne(d => d.Homework).WithMany(p => p.HomeworkResults)
                .HasForeignKey(d => d.HomeworkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HomeworkResult_Homework");

            entity.HasOne(d => d.StudentProcess).WithMany(p => p.HomeworkResults)
                .HasForeignKey(d => d.StudentProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HomeworkResult_StudentProcess");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.ParentsId);

            entity.Property(e => e.ParentsId)
                .ValueGeneratedNever()
                .HasColumnName("parents_id");
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

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK_Payment_1");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("payment_id");
            entity.Property(e => e.ConfirmDate)
                .HasColumnType("datetime")
                .HasColumnName("confirm_date");
            entity.Property(e => e.ParentsId).HasColumnName("parents_id");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .HasColumnName("payment_type");

            entity.HasOne(d => d.Parents).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ParentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Parents");
        });

        modelBuilder.Entity<RegisteredCourse>(entity =>
        {
            entity.HasKey(e => e.RegisteredCourseId).HasName("PK_RegisteredCourse_1");

            entity.ToTable("RegisteredCourse");

            entity.Property(e => e.RegisteredCourseId)
                .ValueGeneratedNever()
                .HasColumnName("registered_course_id");
            entity.Property(e => e.ConfirmDate)
                .HasColumnType("datetime")
                .HasColumnName("confirm_date");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.RegisteredDate)
                .HasColumnType("datetime")
                .HasColumnName("registered_date");

            entity.HasOne(d => d.Course).WithMany(p => p.RegisteredCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisteredCourse_Course");

            entity.HasOne(d => d.Payment).WithMany(p => p.RegisteredCourses)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisteredCourse_Payment");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session");

            entity.Property(e => e.SessionId)
                .ValueGeneratedNever()
                .HasColumnName("session_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(10)
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

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("student_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Birhthdate)
                .HasColumnType("datetime")
                .HasColumnName("birhthdate");
            entity.Property(e => e.CurLevel).HasColumnName("cur_level");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Highscore).HasColumnName("highscore");
            entity.Property(e => e.ParentsId).HasColumnName("parents_id");
            entity.Property(e => e.Playtime).HasColumnName("playtime");
            entity.Property(e => e.Points).HasColumnName("points");

            entity.HasOne(d => d.Account).WithMany(p => p.Students)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Accounts");

            entity.HasOne(d => d.Parents).WithMany(p => p.Students)
                .HasForeignKey(d => d.ParentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Parents");
        });

        modelBuilder.Entity<StudentHomework>(entity =>
        {
            entity.ToTable("StudentHomework");

            entity.Property(e => e.StudentHomeworkId)
                .ValueGeneratedNever()
                .HasColumnName("student_homework_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.HomeworkId).HasColumnName("homework_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.StudentProcessId).HasColumnName("student_process_id");
            entity.Property(e => e.TotalPoint).HasColumnName("total_point");

            entity.HasOne(d => d.Homework).WithMany(p => p.StudentHomeworks)
                .HasForeignKey(d => d.HomeworkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentHomework_Homework");

            entity.HasOne(d => d.StudentProcess).WithMany(p => p.StudentHomeworks)
                .HasForeignKey(d => d.StudentProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentHomework_StudentProcess");
        });

        modelBuilder.Entity<StudentProcess>(entity =>
        {
            entity.HasKey(e => e.StudentProcessId).HasName("PK_Table_2");

            entity.ToTable("StudentProcess");

            entity.Property(e => e.StudentProcessId)
                .ValueGeneratedNever()
                .HasColumnName("student_process_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Playtime).HasColumnName("playtime");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TotalPoint).HasColumnName("total_point");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentProcesses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentProcess_Class");

            entity.HasOne(d => d.Session).WithMany(p => p.StudentProcesses)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentProcess_RegisteredCourse");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentProcesses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentProcess_Student");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("Teacher");

            entity.Property(e => e.TeacherId)
                .ValueGeneratedNever()
                .HasColumnName("teacher_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CourseId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("course_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");

            entity.HasOne(d => d.Account).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teacher_Accounts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
