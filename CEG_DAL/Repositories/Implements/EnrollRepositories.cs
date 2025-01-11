using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using CEG_DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Implements
{
    public class EnrollRepositories : RepositoryBase<Enroll>, IEnrollRepositories
    {
        private readonly MyDBContext _dbContext;
        public EnrollRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Enroll?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Enrolls
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(en => en.EnrollId == id);
        }

        public async Task<List<Enroll>> GetList()
        {
            return await _dbContext.Enrolls
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<List<Enroll>> GetEnrollByParentId(int? parentId)
        {
            return await _dbContext.Enrolls
                .AsNoTrackingWithIdentityResolution()
                .Where(e => e.Student.ParentId == parentId)
                .Select(e => new Enroll
                {
                    EnrollId = e.EnrollId,
                    RegistrationDate = e.RegistrationDate,
                    EnrolledDate = e.EnrolledDate,
                    Status = e.Status,
                    Student = new Student
                    {
                        StudentId = e.Student.StudentId,
                        ParentId = e.Student.ParentId,
                        Account = new Account
                        {
                            Fullname = e.Student.Account.Fullname,
                            Gender = e.Student.Account.Gender,
                        }
                    },
                    Class = new Class
                    {
                        ClassId = e.Class.ClassId,
                        ClassName = e.Class.ClassName,
                        StartDate = e.Class.StartDate,
                        EndDate = e.Class.EndDate,
                        MinimumStudents = e.Class.MinimumStudents,
                        MaximumStudents = e.Class.MaximumStudents,
                        NumberOfStudents = e.Class.NumberOfStudents,
                        TeacherId = e.Class.TeacherId,
                        CourseId = e.Class.CourseId,
                        Status = e.Class.Status,
                        Teacher = new Teacher // Create a new Teacher object
                        {
                            TeacherId = e.Class.Teacher.TeacherId,
                            Account = new Account
                            {
                                Fullname = e.Class.Teacher.Account.Fullname
                            }
                            // Add other necessary properties here, but do NOT include Classes
                        },
                        Course = new Course // Create a new Course object
                        {
                            CourseId = e.Class.Course.CourseId,
                            CourseName = e.Class.Course.CourseName
                            // Add other necessary properties here, but do NOT include Classes
                        },
                    },
                })
                .ToListAsync();
        }

        public async Task<List<Enroll>> GetEnrollByStudentId(int? studentId)
        {
            return await _dbContext.Enrolls
                .AsNoTrackingWithIdentityResolution()
                .Where(e => e.StudentId == studentId)
                .Select(e => new Enroll
                {
                    EnrollId = e.EnrollId,
                    RegistrationDate = e.RegistrationDate,
                    EnrolledDate = e.EnrolledDate,
                    Status = e.Status,
                    Student = new Student
                    {
                        StudentId = e.Student.StudentId,
                        Account = new Account
                        {
                            Fullname = e.Student.Account.Fullname,
                            Gender = e.Student.Account.Gender,
                        }
                    },
                    Class = new Class
                    {
                        ClassId = e.Class.ClassId,
                        ClassName = e.Class.ClassName,
                        StartDate = e.Class.StartDate,
                        EndDate = e.Class.EndDate,
                        MinimumStudents = e.Class.MinimumStudents,
                        MaximumStudents = e.Class.MaximumStudents,
                        NumberOfStudents = e.Class.NumberOfStudents,
                        TeacherId = e.Class.TeacherId,
                        CourseId = e.Class.CourseId,
                        Status = e.Class.Status,
                        Teacher = new Teacher
                        {
                            TeacherId = e.Class.Teacher.TeacherId,
                            Account = new Account
                            {
                                Fullname = e.Class.Teacher.Account.Fullname,
                                Gender = e.Class.Teacher.Account.Gender,
                            }
                        },
                        Course = new Course
                        {
                            CourseId = e.Class.Course.CourseId,
                            CourseName = e.Class.Course.CourseName
                        }
                    }
                })
                .ToListAsync();
        }

        public async Task<Enroll?> GetByStudentFullnameAndClassName(string stuFullName, string claName)
        {
            return await _dbContext.Enrolls
                .AsNoTrackingWithIdentityResolution()
                .Where(enr => enr.Class.ClassName == claName && enr.Student.Account.Fullname == stuFullName).
                SingleOrDefaultAsync();
        }

        public async Task<List<Enroll>> GetByClassId(int? claId)
        {
            return await _dbContext.Enrolls
                .AsNoTrackingWithIdentityResolution()
                .Where(e => e.ClassId == claId)
                .Select(e => new Enroll
                {
                    EnrollId = e.EnrollId,
                    RegistrationDate = e.RegistrationDate,
                    EnrolledDate = e.EnrolledDate,
                    Status = e.Status,
                    Student = new Student
                    {
                        StudentId = e.Student.StudentId,
                        Account = new Account
                        {
                            Fullname = e.Student.Account.Fullname,
                            Gender = e.Student.Account.Gender,
                        }
                    },
                    Class = new Class
                    {
                        ClassId = e.Class.ClassId,
                        ClassName = e.Class.ClassName,
                        StartDate = e.Class.StartDate,
                        EndDate = e.Class.EndDate,
                        MinimumStudents = e.Class.MinimumStudents,
                        MaximumStudents = e.Class.MaximumStudents,
                        NumberOfStudents = e.Class.NumberOfStudents,
                        TeacherId = e.Class.TeacherId,
                        CourseId = e.Class.CourseId,
                        Status = e.Class.Status,
                        Teacher = new Teacher
                        {
                            TeacherId = e.Class.Teacher.TeacherId,
                            Account = new Account
                            {
                                Fullname = e.Class.Teacher.Account.Fullname,
                                Gender = e.Class.Teacher.Account.Gender,
                            }
                        },
                        Course = new Course
                        {
                            CourseId = e.Class.Course.CourseId,
                            CourseName = e.Class.Course.CourseName
                        }
                    }
                })
                .ToListAsync();
        }

        public async Task<int> GetCountByClassId(int? claId)
        {
            return await _dbContext.Enrolls
                .AsNoTrackingWithIdentityResolution()
                .Where(e => e.ClassId == claId).CountAsync();
        }
    }
}
