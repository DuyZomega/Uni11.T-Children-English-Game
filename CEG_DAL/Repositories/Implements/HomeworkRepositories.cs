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
    public class HomeworkRepositories : RepositoryBase<Homework>, IHomeworkRepositories
    {
        private readonly MyDBContext _dbContext;
        public HomeworkRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Homework> GetByIdNoTracking(int id)
        {
            return await _dbContext.Homeworks.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(home => home.HomeworkId == id);
        }

        public async Task<List<Homework>> GetHomeworksList()
        {
            return await _dbContext.Homeworks.ToListAsync();
        }
    }
}
