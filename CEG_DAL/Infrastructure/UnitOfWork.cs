using CEG_DAL.Models;
using CEG_DAL.Repositories.Implements;
using CEG_DAL.Repositories.Interfaces;
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
        private IAccountRepositories _accountRepositories;
        public UnitOfWork(MyDBContext context)
        {
            _dbContext = context;
        }
        public IAccountRepositories AccountRepositories => _accountRepositories ??= new AccountRepositories(_dbContext);
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
