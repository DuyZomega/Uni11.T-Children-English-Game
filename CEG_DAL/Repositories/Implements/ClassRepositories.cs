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

        public async Task<Class?> GetByIdNoTracking(int id, bool includeTeacher = false, bool includeCourse = false, bool includeSession = false, bool filterSession = false)
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
                    EnrollmentFee = c.EnrollmentFee,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Status = c.Status,
                    Teacher = includeTeacher ? new Teacher // Create a new Teacher object
                    {
                        TeacherId = c.Teacher.TeacherId,
                        AccountId = c.Teacher.AccountId,
                        Email = c.Teacher.Email,
                        Phone = c.Teacher.Phone,
                        Image = c.Teacher.Image,
                        Account = new Account
                        {
                            Fullname = c.Teacher.Account.Fullname,
                            Gender = c.Teacher.Account.Gender,
                        }
                        // Add other necessary properties here, but do NOT include Classes
                    } : null,
                    Course = includeCourse ? new Course // Create a new Course object
                    {
                        CourseId = c.Course.CourseId,
                        CourseName = c.Course.CourseName,
                        Sessions = includeSession
                        ? (filterSession
                            ? c.Course.Sessions.Where(ses => !ses.Schedules.Any(sch => sch.ClassId == c.ClassId)).Select(ses => new Session
                            {
                                SessionId = ses.SessionId,
                                SessionNumber = ses.SessionNumber,
                                Title = ses.Title,
                                Description = ses.Description,
                                Hours = ses.Hours,
                            }).ToList()
                            : c.Course.Sessions.Select(ses => new Session
                            {
                                SessionId = ses.SessionId,
                                SessionNumber = ses.SessionNumber,
                                Title = ses.Title,
                                Description = ses.Description,
                                Hours = ses.Hours,
                            }).ToList())
                        : null,
                        // Add other necessary properties here, but do NOT include Classes
                    } : null,
                    Schedules = c.Schedules.Select(sch => new Schedule()
                    {
                        ScheduleId = sch.ScheduleId,
                        ScheduleDate = sch.ScheduleDate,
                        StartTime = sch.StartTime,
                        EndTime = sch.EndTime,
                        Status = sch.Status,
                        Session = new Session()
                        {
                            SessionId = sch.SessionId,
                            SessionNumber = sch.Session.SessionNumber,
                            Title = sch.Session.Title,
                            Description = sch.Session.Description,
                            Hours = sch.Session.Hours
                        }
                    }).ToList(),
                    Enrolls = c.Enrolls.Select(s => new Enroll()
                    {
                        EnrollId = s.EnrollId,
                        Student = new Student()
                        {
                            Account = new Account()
                            {
                                Fullname = s.Student.Account.Fullname
                            }
                        },
                        Status = s.Status
                    }).ToList(),
                })
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(cla => cla.ClassId == id);
        }

        public async Task<List<Class>> GetClassList()
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
                    EnrollmentFee = c.EnrollmentFee,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Status = c.Status,
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
                    },
                    Schedules = c.Schedules,
                    Enrolls = c.Enrolls,
                })
                .ToListAsync();
        }

        public async Task<List<Class>> GetClassOptionListByStatusOpen()
        {
            return await _dbContext.Classes
                .AsNoTrackingWithIdentityResolution()
                .Where(c => c.Status.Equals("Open"))
                .Select(c => new Class
                {
                    ClassName = c.ClassName,
                    EnrollmentFee = c.EnrollmentFee
                })
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
                    EnrollmentFee = c.EnrollmentFee,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Status = c.Status,
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
                    },
                    Schedules = c.Schedules,
                    Enrolls = c.Enrolls,
                })
                .ToListAsync();
        }

        public async Task<List<Class>> GetClassListParent()
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
                    EnrollmentFee = c.EnrollmentFee,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Status = c.Status,
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
                    },
                    Schedules = c.Schedules,
                    Enrolls = c.Enrolls,
                })
                .ToListAsync();
        }

        public async Task<List<Class>> GetClassListByTeacherId(int teacherId)
        {
            return await _dbContext.Classes
                .AsNoTrackingWithIdentityResolution()
                .Where(c => c.TeacherId == teacherId && c.Status != "Draft")
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MinimumStudents = c.MinimumStudents,
                    MaximumStudents = c.MaximumStudents,
                    EnrollmentFee = c.EnrollmentFee,
                    TeacherId = c.TeacherId,
                    CourseId = c.CourseId,
                    Status = c.Status,
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
                    },
                    Schedules = c.Schedules,
                    Enrolls = c.Enrolls,
                })
                .ToListAsync();
        }

        public async Task<int> GetIdByClassId(int id)
        {
            var result = await (from c in _dbContext.Classes where c.ClassId == id select c).FirstOrDefaultAsync();
            if (result != null) return result.ClassId;
            return 0;
        }

        public async Task<Class?> GetByClassName(string className)
        {
            return await _dbContext.Classes.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(c => c.ClassName == className);
        }
    }
}
