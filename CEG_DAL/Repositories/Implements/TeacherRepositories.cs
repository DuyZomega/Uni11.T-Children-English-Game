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
    public class TeacherRepositories : RepositoryBase<Teacher>, ITeacherRepositories
    {
        private readonly MyDBContext _dbContext;
        public TeacherRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Teacher>> GetTeacherList()
        {
            return await _dbContext.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Teachers.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(t => t.TeacherId == id);
        }

        public async Task<Teacher?> GetByEmail(string email)
        {
            return await _dbContext.Teachers.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(t => t.Email == email);
        }

        public async Task<int> GetIdByUsername(string username)
        {
            var result = await (from t in _dbContext.Teachers where t.Account.Username == username select t).FirstOrDefaultAsync();
            if (result != null) return result.TeacherId;
            return 0;
        }
    }
}
