using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IStudentProgressRepositories : IRepositoryBase<StudentProgress>
    {
        Task<List<StudentProgress>> GetList();
        Task<List<StudentProgress>> GetListByHomeworkId(int homId);
        Task<List<StudentProgress>> GetListByMultipleHomeworkId(int[] homIds);
        Task<StudentProgress?> GetByIdNoTracking(int id);
        Task<TimeSpan> GetTotalTimeByStudentId(int? id);
        Task<int> GetTotalPointByStudentId(int? id);
        Task UpdateStudentProgressTotalPointsAsync();
    }
}
