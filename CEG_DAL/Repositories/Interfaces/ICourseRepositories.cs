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
        Task<List<Course>> GetCoursList();
        Task<Course> GetByIdNoTracking(int id);
        Task<int> GenerateNewCourseId();
    }
}
