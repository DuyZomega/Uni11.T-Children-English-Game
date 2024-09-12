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
    public class HomeworkResultRepositories : RepositoryBase<HomeworkResult>, IHomeworkResultRepositories
    {
        private readonly MyDBContext _dbContext;
        public HomeworkResultRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<HomeworkResult> GetByIdNoTracking(int id)
        {
            return _dbContext.HomeworkResults.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(homere => homere.HomeworkResultId == id);
        }

        public async Task<List<HomeworkResult>> GetHomeworkResultsList()
        {
            return await _dbContext.HomeworkResults.ToListAsync();
        }
    }
}
