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
    public class EnrollRepositories : RepositoryBase<Enroll>, IEnrollRepositories
    {
        private readonly MyDBContext _dbContext;
        public EnrollRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        } 
    }
}
