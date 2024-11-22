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
    public class StudentRepositories : RepositoryBase<Student>, IStudentRepositories
    {
        private readonly MyDBContext _dbContext;
        public StudentRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Student>> GetStudentList()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<List<string>> GetStudentNameList()
        {
            return await _dbContext.Students
                .AsNoTrackingWithIdentityResolution()
                .Select(s => s.Account.Fullname)
                .ToListAsync();
        }

        public async Task<List<string>> GetStudentNameListByParent(int id)
        {
            return await _dbContext.Students
                .AsNoTrackingWithIdentityResolution()
                .Where(s => s.ParentId == id)
                .Select(s => s.Account.Fullname)
                .ToListAsync();
        }

        public async Task<List<string>> GetStudentNameListByParentName(string parentName)
        {
            return await _dbContext.Students
                .AsNoTrackingWithIdentityResolution()
                .Where(s => s.Parent.Account.Fullname.Trim().Equals(parentName))
                .Select(s => s.Account.Fullname)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Students.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(stu => stu.StudentId == id);
        }
        public async Task<Student?> GetByFullname(string fullname)
        {
            return await _dbContext.Students
                .Include(s => s.Account)
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(s => s.Account.Fullname == fullname);
        }
        public async Task<Student?> GetByAccountIdNoTracking(int id)
        {
            return await _dbContext.Students
                .Select(stu => new Student
                {
                    StudentId = stu.StudentId,
                    Age = stu.Age,
                    Birthdate = stu.Birthdate,
                    CurLevel = stu.CurLevel,
                    Description = stu.Description,
                    Image = stu.Image,
                    ParentId = stu.ParentId,
                    AccountId = stu.AccountId,
                    Account = new Account
                    {
                        AccountId = stu.AccountId,
                        Fullname = stu.Account.Fullname,
                        Gender = stu.Account.Gender,
                        RoleId = stu.Account.RoleId,
                        Username = stu.Account.Username,
                        Password = stu.Account.Password,
                        CreatedDate = stu.Account.CreatedDate,
                        Status = stu.Account.Status,
                        Role = new Role
                        {
                            RoleId = stu.Account.Role.RoleId,
                            RoleName = stu.Account.Role.RoleName
                        },
                    },
                    Enrolls = stu.Enrolls.Select(enr => new Enroll
                    {
                        EnrollId = enr.EnrollId,
                        ClassId = enr.ClassId,
                        Class = new Class
                        {
                            ClassId = enr.ClassId,
                            StartDate = enr.Class.StartDate,
                            EndDate = enr.Class.EndDate,
                            ClassName = enr.Class.ClassName,
                            CourseId = enr.Class.CourseId,
                            Course = new Course
                            {
                                CourseId = enr.Class.CourseId,
                                CourseName = enr.Class.Course.CourseName,
                                Status = enr.Class.Course.Status,
                            },
                            Status = enr.Class.Status
                        },
                        Status = enr.Status,
                        EnrolledDate = enr.EnrolledDate,
                        RegistrationDate = enr.RegistrationDate,
                        TransactionId = enr.TransactionId,
                        /*Transaction = new Transaction
                        {

                        },*/
                    }).ToList(),
                    Parent = new Parent
                    {
                        ParentId = stu.ParentId,
                        Account = new Account
                        {
                            AccountId = stu.Parent.AccountId,
                            Fullname = stu.Parent.Account.Fullname,
                            Gender = stu.Parent.Account.Gender,
                            Status = stu.Parent.Account.Status,
                            Role = new Role
                            {
                                RoleId = stu.Parent.Account.Role.RoleId,
                                RoleName = stu.Parent.Account.Role.RoleName
                            }
                        },
                    },
                    StudentProgresses = stu.StudentProgresses.Select(pro => new StudentProgress
                    {
                        StudentProgressId = pro.StudentProgressId,
                        Playtime = pro.Playtime,
                        TotalPoint = pro.TotalPoint,
                        StudentId = pro.StudentId,
                    }).ToList(),
                })
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(t => t.Account.AccountId == id);
        }
        public async Task<List<Student>> GetStudentByParentId(int parentId)
        {
            return await _dbContext.Students.Where(stu => stu.ParentId == parentId)
                .Include(stu => stu.Account).ToListAsync();
        }
        public async Task<List<Student>> GetStudentByClassId(int classId)
        {
            return await _dbContext.Enrolls
                .Where(stu => stu.ClassId == classId)
                .Select(e => e.Student)
                .ToListAsync();
        }

        public async Task<int?> GetIdByAccountIdNoTracking(int id)
        {
            return await _dbContext.Students
                .Where(stu => stu.AccountId == id)
                .Select(e => e.StudentId)
                .FirstOrDefaultAsync();
        }
    }
}
