using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IHomeworkRepositories : IRepositoryBase <Homework>
    {
        Task<List<Homework>> GetHomeworksList();
        Task<Homework?> GetByIdNoTracking(int id);
    }
}
