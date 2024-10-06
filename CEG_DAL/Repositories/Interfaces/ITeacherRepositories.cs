using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface ITeacherRepositories : IRepositoryBase<Teacher>
    {
        Task<List<Teacher>> GetTeacherList();
        Task<List<string>> GetTeacherNameList();
        Task<Teacher?> GetByIdNoTracking(int id);
        Task<Teacher?> GetByEmail(string email);
        Task<Teacher?> GetByFullname(string fullname);
        Task<int> GetIdByUsername(string username);
    }
}
