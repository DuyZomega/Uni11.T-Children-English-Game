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
        Task<List<string>> GetStudentNameList();
        Task<List<string>> GetStudentNameListByParent(int id);
        Task<List<string>> GetStudentNameListByParentName(string parentName);
        Task<Student?> GetByIdNoTracking(int id);
        Task<Student?> GetByAccountIdNoTracking(int id);
        Task<Student?> GetByFullname (string fullname);
        Task<int?> GetIdByAccountIdNoTracking(int id);
        Task<List<Student>> GetStudentByParentId(int parentId);
        Task<List<Student>> GetStudentByClassId(int classId);
    }
}
