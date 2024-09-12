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
    public class RegisteredCourseRepositories : RepositoryBase<RegisteredCourse>, IRegisteredCourseRepositories
    {
        private readonly MyDBContext _dbContext;
        public RegisteredCourseRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RegisteredCourse> GetByIdNoTracking(int id)
        {
            return await _dbContext.RegisteredCourses.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(regi => regi.RegisteredCourseId == id);
        }

        public async Task<List<RegisteredCourse>> GetRegisteredCoursesList()
        {
            return await _dbContext.RegisteredCourses.ToListAsync();
        }
    }
}
