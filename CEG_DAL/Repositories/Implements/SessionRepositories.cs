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

        public async Task<Session> GetByIdNoTracking(int id)
        {
            return await _dbContext.Sessions.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(sess => sess.SessionId == id);
        }

        public async Task<List<Session>> GetSessionsList()
        {
            return await _dbContext.Sessions.ToListAsync();
        }
    }
}
