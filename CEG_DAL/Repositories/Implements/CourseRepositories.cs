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

        public async Task<Course?> GetByIdNoTrackingInclude(int id)
        {
            return await _dbContext.Courses
                .Include(c => c.Sessions)
                .ThenInclude(s => s.Homeworks)
                .Include(c => c.Classes)
                .AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseId == id);
        }

        public async Task<Course?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Courses
                .AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseId == id);
        }

        public async Task<Course?> GetByIdNoTracking(int id, bool includeSessions = false, bool includeHomeworks = false)
        {
            if (includeSessions && !includeHomeworks)
            {
                return await _dbContext.Courses
                    .Include(c => c.Sessions)
                    .AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseId == id);
            }
            else if(includeHomeworks || (includeSessions && includeHomeworks))
            {
                return await _dbContext.Courses
                    .Include(c => c.Sessions)
                    .ThenInclude(s => s.Homeworks)
                    .AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseId == id);
            }
            return await _dbContext.Courses
                .AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseId == id);
        }

        public async Task<List<Course>> GetCourseList()
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

        public async Task<List<string>?> GetCourseNameList()
        {
            return await _dbContext.Courses
                .Select(c => c.CourseName)
                .ToListAsync();
        }
        public async Task<List<string>?> GetCourseNameByStatusList(string status)
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
    }
}
