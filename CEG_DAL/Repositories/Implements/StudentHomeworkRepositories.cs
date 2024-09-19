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
    public class StudentHomeworkRepositories : RepositoryBase<StudentHomework>,IStudentHomeworkRepositories
    {
        private readonly MyDBContext _dbContext;
        public StudentHomeworkRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentHomework> GetByIdNoTracking(int id)
        {
            return await _dbContext.StudentHomeworks.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(home => home.StudentHomeworkId == id);
        }

        public async Task<List<StudentHomework>> GetStudentHomeworksList()
        {
            return await _dbContext.StudentHomeworks.ToListAsync();
        }
    }
}
