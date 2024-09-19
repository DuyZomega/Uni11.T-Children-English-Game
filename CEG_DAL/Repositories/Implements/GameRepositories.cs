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
    public class GameRepositories : RepositoryBase<Game>, IGameRepositories
    {
        private readonly MyDBContext _dbContext;
        public GameRepositories(MyDBContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Game> GetByIdNoTracking(int id)
        {
            return await _dbContext.Games.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(game => game.GameId == id);
        }

        public async Task<List<Game>> GetGamesList()
        {
            return await _dbContext.Games.ToListAsync();
        }
    }
}
