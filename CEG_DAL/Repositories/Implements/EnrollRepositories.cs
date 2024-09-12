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
    public class EnrollRepositories : RepositoryBase<Enroll>, IEnrollRepositories
    {
        private readonly MyDBContext _dbContext;
        public EnrollRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Enroll> GetByIdNoTracking(int id)
        {
            return await _dbContext.Enrolls.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(en => en.EnrollId == id);
        }

        public async Task<List<Enroll>> GetEnrollsList()
        {
            return await _dbContext.Enrolls.ToListAsync();
        }
    }
}
