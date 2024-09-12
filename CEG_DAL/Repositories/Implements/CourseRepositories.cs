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

        public async Task<int> GenerateNewCourseId()
        {
            var lastCou = await _dbContext.Courses.OrderByDescending(cou => cou.CourseId).FirstOrDefaultAsync();
            int newId = 1;
            if (lastCou != null)
            {
                newId = lastCou.CourseId + 1;
            }
            return newId;
        }

        public async Task<Course> GetByIdNoTracking(int id)
        {
            return await _dbContext.Courses.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cou => cou.CourseId == id);
        }

        public async Task<List<Course>> GetCoursList()
        {
            return await _dbContext.Courses.ToListAsync();
        }
    }
}
