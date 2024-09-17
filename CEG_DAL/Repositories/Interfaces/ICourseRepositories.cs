using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface ICourseRepositories : IRepositoryBase<Course>
    {
        Task<List<Course>> GetCourseList();
        Task<Course?> GetByIdNoTracking(int id);
        Task<Course?> GetByName(string name);
        Task<int> GetIdByName(string name);
    }
}
