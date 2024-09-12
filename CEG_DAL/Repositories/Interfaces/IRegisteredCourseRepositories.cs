using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IRegisteredCourseRepositories : IRepositoryBase<RegisteredCourse>
    {
        Task<List<RegisteredCourse>> GetRegisteredCoursesList();
        Task<RegisteredCourse> GetByIdNoTracking(int id);
    }
}
