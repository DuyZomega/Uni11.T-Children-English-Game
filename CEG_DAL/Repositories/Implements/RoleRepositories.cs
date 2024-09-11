using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using CEG_DAL.Repositories.Interfaces;
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

        public int GetRoleIdByRoleName(string roleName)
        {
            var result = (from r in _dbContext.Roles where r.RoleName == roleName select r).FirstOrDefault();
            if (result != null) return result.RoleId;
            return 0;
        }
    }
}
