﻿using CEG_DAL.Infrastructure;
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

        public async Task<Account?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Accounts.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(acc => acc.AccountId == id);
        }
    }
}
