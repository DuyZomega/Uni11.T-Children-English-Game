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
    public class StudentProgressRepositories : RepositoryBase<StudentProgress>, IStudentProgressRepositories
    {
        private readonly MyDBContext _dbContext;
        public StudentProgressRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentProgress?> GetByIdNoTracking(int id)
        {
            return await _dbContext.StudentProgresses.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(pro => pro.StudentProgressId == id);
        }

        public async Task<List<StudentProgress>> GetStudentProgressList()
        {
            return await _dbContext.StudentProgresses.ToListAsync();
        }
    }
}
