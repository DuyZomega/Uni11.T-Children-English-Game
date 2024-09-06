using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBContext _dbContext;
        public UnitOfWork(MyDBContext context)
        {
            _dbContext = context;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
