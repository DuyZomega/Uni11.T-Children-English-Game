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
    public class SessionRepositories : RepositoryBase<Session>,ISessionRepositories
    {
        private readonly MyDBContext _dbContext;
        public SessionRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Session?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Sessions.Include(s => s.Homeworks).AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(sess => sess.SessionId == id);
        }

        public async Task<List<Session>> GetSessionList()
        {
            return await _dbContext.Sessions.ToListAsync();
        }
        
        public async Task<Session?> GetByTitle(string name)
        {
            return await _dbContext.Sessions.Include(s => s.Homeworks).AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(sess => sess.Title == name);
        }

        public async Task<int> GetIdByTitle(string name)
        {
            var result = await (from s in _dbContext.Sessions where s.Title == name select s).FirstOrDefaultAsync();
            if (result != null) return result.SessionId;
            return 0;
        }

        public async Task<List<Session>> GetSessionListByCourseId(int courseId)
        {
            return await _dbContext.Sessions.AsNoTrackingWithIdentityResolution().Where(sess => sess.CourseId == courseId).ToListAsync();
        }
    }
}
