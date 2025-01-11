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
    public class TeacherRepositories : RepositoryBase<Teacher>, ITeacherRepositories
    {
        private readonly MyDBContext _dbContext;
        public TeacherRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Teacher>> GetTeacherList()
        {
            return await _dbContext.Teachers.ToListAsync();
        }
        public async Task<List<Teacher>?> GetTeacherNameOptionList()
        {
            return await _dbContext.Teachers
                .Select(t => new Teacher
                {
                    TeacherId = t.TeacherId,
                    Account = new Account
                    {
                        Fullname = t.Account.Fullname,
                    }
                })
                .ToListAsync();
        }

        public async Task<Teacher?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Teachers
                .AsNoTrackingWithIdentityResolution()
                .Where(tea => tea.TeacherId == id)
                .Select(tea => new Teacher()
                {
                    TeacherId = tea.TeacherId,
                    AccountId = tea.AccountId,
                    Address = tea.Address,
                    Certificate = tea.Certificate,
                    Image = tea.Image,
                    Email = tea.Email,
                    Phone = tea.Phone,
                    Account = new Account()
                    {
                        Fullname = tea.Account.Fullname,
                        AccountId = tea.AccountId,
                        CreatedDate = tea.Account.CreatedDate,
                        Gender = tea.Account.Gender,
                        Password = tea.Account.Password,
                        Status = tea.Account.Status,
                        Username = tea.Account.Username,
                        RoleId = tea.Account.RoleId,
                        TotalAmount = tea.Account.TotalAmount,
                    },
                })
                .SingleOrDefaultAsync();
        }

        public async Task<Teacher?> GetByEmail(string email)
        {
            return await _dbContext.Teachers.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(t => t.Email == email);
        }

        public async Task<int> GetIdByUsername(string username)
        {
            var result = await (from t in _dbContext.Teachers where t.Account.Username == username select t).FirstOrDefaultAsync();
            if (result != null) return result.TeacherId;
            return 0;
        }

        public async Task<Teacher?> GetByFullname(string fullname)
        {
            return await _dbContext.Teachers
                .Include(t => t.Account)
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(t => t.Account.Fullname == fullname);
        }

        public async Task<Teacher?> GetByAccountIdNoTracking(int id)
        {
            return await _dbContext.Teachers
                .Include(t => t.Account)
                .ThenInclude(a => a.Role)
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(t => t.Account.AccountId == id);
        }
        public async Task<int> GetIdByAccountId(int id)
        {
            return await _dbContext.Teachers
                .Where(par => par.AccountId == id)
                .Select(p => p.TeacherId)
                .FirstOrDefaultAsync();
        }
    }
}
