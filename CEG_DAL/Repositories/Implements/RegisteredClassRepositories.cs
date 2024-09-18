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
    public class RegisteredClassRepositories : RepositoryBase<RegisteredClass>, IRegisteredClassRepositories
    {
        private readonly MyDBContext _dbContext;
        public RegisteredClassRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RegisteredClass?> GetByIdNoTracking(int id)
        {
            return await _dbContext.RegisteredClasses.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(regi => regi.RegisteredClassId == id);
        }

        public async Task<List<RegisteredClass>> GetRegisteredCoursesList()
        {
            return await _dbContext.RegisteredClasses.ToListAsync();
        }
    }
}
