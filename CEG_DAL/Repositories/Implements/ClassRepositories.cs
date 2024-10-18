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
    public class ClassRepositories : RepositoryBase<Class>, IClassRepositories
    {
        private readonly MyDBContext _dbContext;
        public ClassRepositories(MyDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Class?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Classes.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cla => cla.ClassId == id);
        }

        public async Task<List<Class>> GetClassList()
        {
            return await _dbContext.Classes
                .Include(c => c.Teacher)
                .Include(c => c.Course)
                .ToListAsync();
        }
        public async Task<List<Class>> GetClassListAdmin()
        {
            return await _dbContext.Classes
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MinimumStudents = c.MinimumStudents,
                    MaximumStudents = c.MaximumStudents,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Teacher = new Teacher // Create a new Teacher object
                    {
                        TeacherId = c.Teacher.TeacherId,
                        Email = c.Teacher.Email,
                        Phone = c.Teacher.Phone,
                        Image = c.Teacher.Image,
                        Account = new Account
                        {
                            Fullname = c.Teacher.Account.Fullname,
                            Gender = c.Teacher.Account.Gender,
                        }
                        // Add other necessary properties here, but do NOT include Classes
                    },
                    Course = new Course // Create a new Course object
                    {
                        CourseId = c.Course.CourseId,
                        CourseName = c.Course.CourseName
                        // Add other necessary properties here, but do NOT include Classes
                    }
                })
                .ToListAsync();
        }
        public async Task<List<Class>> GetClassListByTeacherId(int teacherId)
        {
            return await _dbContext.Classes.AsNoTrackingWithIdentityResolution().Where(c => c.TeacherId == teacherId)
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Account)
                .Include(c => c.Course).
                Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MinimumStudents = c.MinimumStudents,
                    MaximumStudents = c.MaximumStudents,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Teacher = new Teacher // Create a new Teacher object
                    {
                        TeacherId = c.Teacher.TeacherId,
                        Email = c.Teacher.Email,
                        Phone = c.Teacher.Phone,
                        Image = c.Teacher.Image,
                        Account = new Account
                        {
                            Fullname = c.Teacher.Account.Fullname,
                            Gender = c.Teacher.Account.Gender,
                        }
                        // Add other necessary properties here, but do NOT include Classes
                    },
                    Course = new Course // Create a new Course object
                    {
                        CourseId = c.Course.CourseId,
                        CourseName = c.Course.CourseName
                        // Add other necessary properties here, but do NOT include Classes
                    }
                }).ToListAsync();
        }

        public async Task<int> GetIdByClassId(int id)
        {
            var result = await (from c in _dbContext.Classes where c.ClassId == id select c).FirstOrDefaultAsync();
            if (result != null) return result.ClassId;
            return 0;
        }
    }
}
