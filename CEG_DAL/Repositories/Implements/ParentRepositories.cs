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
    public class ParentRepositories : RepositoryBase<Parent>,IParentRepositories
    {
        private readonly MyDBContext _dbContext;
        public ParentRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Parent> GetByIdNoTracking(int id)
        {
            return await _dbContext.Parents.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(par => par.ParentsId == id);
        }

        public async Task<List<Parent>> GetParentList()
        {
            return await _dbContext.Parents.ToListAsync();
        }
    }
}
