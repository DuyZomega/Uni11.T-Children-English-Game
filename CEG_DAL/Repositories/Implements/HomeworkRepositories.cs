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

        public async Task<Homework?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Homeworks
                .Include(h => h.HomeworkQuestions)
                .ThenInclude(s => s.HomeworkAnswers)
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(home => home.HomeworkId == id);
        }

        public async Task<List<Homework>> GetHomeworksList()
        {
            return await _dbContext.Homeworks.ToListAsync();
        }
        public async Task<int> GetIdByTitle(string name)
        {
            var result = await (from s in _dbContext.Homeworks where s.Title == name select s).FirstOrDefaultAsync();
            if (result != null) return result.HomeworkId;
            return 0;
        }
        public async Task<List<Homework>?> GetHomeworkListBySessionId(int sessionId)
        {
            return await _dbContext.Homeworks.AsNoTrackingWithIdentityResolution().Where(home => home.SessionId == sessionId).ToListAsync();
        }

        public async Task<Homework?> GetByTitle(string name)
        {
            return await _dbContext.Homeworks
                .Select(h => new Homework()
                {
                    HomeworkId = h.HomeworkId,
                    Title = h.Title,
                    Description = h.Description,
                    StartDate = h.StartDate,
                    EndDate = h.EndDate,
                    GameConfigId = h.GameConfigId,
                    Hours = h.Hours,
                    Type = h.Type,
                    Status = h.Status,
                    HomeworkQuestions = h.HomeworkQuestions,
                })
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(home => home.Title == name);
        }
    }
}
