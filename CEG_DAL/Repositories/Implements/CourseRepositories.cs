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
                .Include(c => c.Sessions)
                .ThenInclude(s => s.Homeworks)
                .Include(c => c.Classes)
                .AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseId == id);
        }

        public async Task<Course?> GetByIdNoTracking(int id, bool includeSessions, bool includeHomeworks)
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
                .Include(c => c.Sessions)
                .Include(c => c.Classes)
                .ToListAsync();
        }

        public async Task<List<string>> GetCourseNameList()
        {
            return await _dbContext.Courses
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
    }
}
