using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IStudentAnswerRepositories : IRepositoryBase<StudentAnswer>
    {
        Task<List<StudentAnswer>> GetList();
        Task<StudentAnswer?> GetByIdNoTracking(int id);
    }
}
