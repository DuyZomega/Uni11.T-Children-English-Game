using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using CEG_DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Implements
{
    public class AccountRepositories : RepositoryBase<Account>, IAccountRepositories
    {
        private readonly MyDBContext _dbContext;
        public AccountRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account?> GetByLogin(string userName, string password)
        {
            return await _dbContext.Accounts.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(acc => acc.Username == userName && acc.Password == password);
        }

        public async Task<List<Account>> GetAccountList()
        {
            return await _dbContext.Accounts.Include(a => a.Role).ToListAsync();
        }

        public async Task<Account?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Accounts.AsNoTrackingWithIdentityResolution().Include(a => a.Role).SingleOrDefaultAsync(acc => acc.AccountId == id);
        }

        public async Task<Account?> GetByUsername(string username)
        {
            return await _dbContext.Accounts.AsNoTrackingWithIdentityResolution().Include(a => a.Role).SingleOrDefaultAsync(acc => acc.Username == username);
        }

        public async Task<string?> GetRoleByAccountId(int id)
        {
            var acc = await _dbContext.Accounts.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(acc => acc.AccountId == id);
            if (acc != null)
            {
                var roleId = acc.RoleId;
                var role = await _dbContext.Roles.AsNoTracking().SingleOrDefaultAsync(r => r.RoleId == roleId);
                var roleName = role.RoleName;
                return roleName;
            }
            return null;
        }

        public async Task<int> GetIdByUsername(string username)
        {
            var result = await (from acc in _dbContext.Accounts where acc.Username.Trim().ToLower() == username.Trim().ToLower() select acc).FirstOrDefaultAsync();
            if (result != null) return result.AccountId;
            return 0;
        }

        public async Task<bool> UpdateStatusById(string status, int id)
        {
            var acc = await _dbContext.Accounts.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(acc => acc.AccountId == id);
            if (acc != null)
            {
                acc.Status = status;
                _dbContext.Accounts.Update(acc);
                return true;
            }
            return false;
        }
    }
}
