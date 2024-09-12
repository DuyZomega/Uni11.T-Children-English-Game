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
    public class GameConfigRepositories : RepositoryBase<GameConfig>, IGameConfigRepositories
    {
        private readonly MyDBContext _dbContext;
        public GameConfigRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GameConfig> GetByIdNoTracking(int id)
        {
            return await _dbContext.GameConfigs.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(config => config.GameConfigId == id);
        }

        public async Task<List<GameConfig>> GetGameConfigsList()
        {
            return await _dbContext.GameConfigs.ToListAsync();
        }
    }
}
