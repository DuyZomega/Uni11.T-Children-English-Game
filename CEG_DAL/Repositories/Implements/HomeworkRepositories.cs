using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Implements
{
    public class HomeworkRepositories : RepositoryBase<Homework>
    {
        private readonly MyDBContext _dbContext;
        public HomeworkRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
