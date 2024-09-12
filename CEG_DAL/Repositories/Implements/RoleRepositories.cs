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
    public class RoleRepositories : RepositoryBase<Role>, IRoleRepositories
    {
        private readonly MyDBContext _dbContext;
        public RoleRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetRoleIdByRoleName(string roleName)
        {
            var result = await (from r in _dbContext.Roles where r.RoleName == roleName select r).FirstOrDefaultAsync();
            if (result != null) return result.RoleId;
            return 0;
        }
    }
}
