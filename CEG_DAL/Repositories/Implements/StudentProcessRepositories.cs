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
    public class StudentProcessRepositories : RepositoryBase<StudentProcess>, IStudentProcessRepositories
    {
        private readonly MyDBContext _dbContext;
        public StudentProcessRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentProcess> GetByIdNoTracking(int id)
        {
            return await _dbContext.StudentProcesses.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(pro => pro.StudentProcessId == id);
        }

        public async Task<List<StudentProcess>> GetStudentProcessesList()
        {
            return await _dbContext.StudentProcesses.ToListAsync();
        }
    }
}
