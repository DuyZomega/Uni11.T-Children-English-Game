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
    public class ClassRepositories : RepositoryBase<Class>, IClassRepositories
    {
        private readonly MyDBContext _dbContext;
        public ClassRepositories(MyDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Class> GetByIdNoTracking(int id)
        {
            return await _dbContext.Classes.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(cla => cla.ClassId == id);
        }

        public async Task<List<Class>> GetClassList()
        {
            return await _dbContext.Classes.ToListAsync();
        }
    }
}
