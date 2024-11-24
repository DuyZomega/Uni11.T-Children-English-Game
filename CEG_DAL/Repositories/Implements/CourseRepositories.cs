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
    public class CourseRepositories : RepositoryBase<Course>, ICourseRepositories
    {
        private readonly MyDBContext _dbContext;
        public CourseRepositories(MyDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Course?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Courses
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(cou => cou.CourseId == id);
        }

        public async Task<Course?> GetByIdNoTracking(int id, bool includeSessions = false, bool includeClasses = false, bool includeHomeworks = false)
        {
            var query = _dbContext.Courses
                        .Select(c => new Course
                        {
                            CourseId = c.CourseId,
                            CourseName = c.CourseName,
                            CourseType = c.CourseType,
                            Description = c.Description,
                            Difficulty = c.Difficulty,
                            Category = c.Category,
                            Image = c.Image,
                            RequiredAge = c.RequiredAge,
                            TotalHours = c.TotalHours,
                            Status = c.Status,
                            // Include Sessions only if requested
                            Sessions = includeSessions ? c.Sessions.Select(s => new Session
                            {
                                SessionId = s.SessionId,
                                Title = s.Title,
                                Description = s.Description,
                                Hours = s.Hours,
                                SessionNumber = s.SessionNumber,
                                //Status = s.Status,
                                // Include Homeworks only if requested
                                Homeworks = includeHomeworks ? s.Homeworks.ToList() : null
                            }).ToList() : null,
                            // Include Classes only if requested
                            Classes = includeClasses ? c.Classes.ToList() : null
                        }).AsNoTrackingWithIdentityResolution();

            return await query.SingleOrDefaultAsync(cou => cou.CourseId == id);
        }

        public async Task<List<Course>> GetList()
        {
            return await _dbContext.Courses
                .Select(c => new Course()
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseType = c.CourseType,
                    Description = c.Description,
                    Difficulty = c.Difficulty,
                    Category = c.Category,
                    Image = c.Image,
                    RequiredAge = c.RequiredAge,
                    TotalHours = c.TotalHours,
                    Status = c.Status,
                    Sessions = c.Sessions,
                    Classes = c.Classes
                })
                .ToListAsync();
        }

        public async Task<List<Course>?> GetListByStatus(string status)
        {
            return await _dbContext.Courses
                .Where(c => c.Status == status)
                .Select(c => new Course()
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseType = c.CourseType,
                    Description = c.Description,
                    Difficulty = c.Difficulty,
                    Category = c.Category,
                    Image = c.Image,
                    RequiredAge = c.RequiredAge,
                    TotalHours = c.TotalHours,
                    Status = c.Status,
                    Sessions = c.Sessions
                })
                .ToListAsync();
        }

        public async Task<List<string>?> GetNameList()
        {
            return await _dbContext.Courses
                .Select(c => c.CourseName)
                .ToListAsync();
        }
        public async Task<List<string>?> GetNameListByStatus(string status)
        {
            return await _dbContext.Courses
                .Where(c => c.Status == status)
                .Select(c => c.CourseName)
                .ToListAsync();
        }

        public async Task<Course?> GetByName(string name)
        {
            return await _dbContext.Courses.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseName == name);
        }

        public async Task<int> GetIdByName(string name)
        {
            var result = await (from c in _dbContext.Courses where c.CourseName == name select c).FirstOrDefaultAsync();
            if (result != null) return result.CourseId;
            return 0;
        }

        public async Task<string?> GetStatusByHomeworkIdNoTracking(int homeworkId)
        {
            return await _dbContext.Homeworks
                .Where(h => h.HomeworkId == homeworkId)
                .Select(h => h.Session.Course.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetStatusByCourseIdNoTracking(int courseId)
        {
            return await _dbContext.Courses
                .Where(h => h.CourseId == courseId)
                .Select(h => h.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetStatusBySessionIdNoTracking(int sessionId)
        {
            return await _dbContext.Sessions
                .Where(h => h.SessionId == sessionId)
                .Select(h => h.Course.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetStatusByQuestionIdNoTracking(int questionId)
        {
            return await _dbContext.HomeworkQuestions
                .Where(h => h.HomeworkQuestionId == questionId)
                .Select(hq => hq.Homework != null
                      ? hq.Homework.Session.Course.Status
                      : "NotFound")
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalAmount()
        {
            return await _dbContext.Courses.CountAsync();
        }

        public void UpdateTotalHoursByIdThroughSessionsSum(int id)
        {
            var selectedCourse = _dbContext.Courses.Where(cour => cour.CourseId.Equals(id)).Select(c => new Course()
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseType = c.CourseType,
                Description = c.Description,
                Difficulty = c.Difficulty,
                Category = c.Category,
                Image = c.Image,
                RequiredAge = c.RequiredAge,
                TotalHours = c.TotalHours,
                Status = c.Status,
                Sessions = c.Sessions
            }).SingleOrDefault();
            if(selectedCourse == null) { return; }
            selectedCourse.TotalHours = selectedCourse.Sessions.Sum(ses => ses.Hours);
            _dbContext.Courses.Update(selectedCourse);
            _dbContext.SaveChanges();
        }
    }
}
