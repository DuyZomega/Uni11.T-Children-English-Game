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
    public class GameLevelRepositories : RepositoryBase<GameLevel>, IGameLevelRepositories
    {
        private readonly MyDBContext _dbContext;
        public GameLevelRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GameLevel> GetByIdNoTracking(int id)
        {
            return await _dbContext.GameLevels.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(level => level.GameLevelId == id);
        }

        public async Task<List<GameLevel>> GetGameLevelsList()
        {
            return await _dbContext.GameLevels.ToListAsync();
        }
    }
}
