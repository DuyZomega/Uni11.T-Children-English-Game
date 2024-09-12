using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IStudentRepositories : IRepositoryBase<Student>
    {
        Task<List<Student>> GetStudentList();
        Task<Student?> GetByIdNoTracking(int id);
    }
}
