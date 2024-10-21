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
    public class ScheduleRepositories : RepositoryBase<Schedule>, IScheduleRepositories
    {
        private readonly MyDBContext _dbContext;

        public ScheduleRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateBySessionId(int sessionId)
        {
            
        }

        public async Task<Schedule?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == id);
        }

        public async Task<List<Schedule>?> GetListByClassId(int classId)
        {
            return await _dbContext.Schedules
                .Where(s => s.ClassId == classId)
                .ToListAsync();
        }
    }
}
