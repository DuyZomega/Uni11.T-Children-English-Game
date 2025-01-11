using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IStudentHomeworkRepositories : IRepositoryBase<StudentHomework>
    {
        Task<List<StudentHomework>> GetList();
        Task<StudentHomework?> GetByIdNoTracking(int id);
        Task<List<StudentHomework>> GetListByStudentId(int? id);
    }
}
