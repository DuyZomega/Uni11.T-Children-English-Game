using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IClassRepositories : IRepositoryBase<Class>
    {
        Task<List<Class>> GetClassList();
        Task<List<Class>> GetClassOptionListByStatusOpen();
        Task<List<Class>> GetClassListAdmin();
        Task<List<Class>> GetClassListParent();
        Task<Class?> GetByIdNoTracking(int id, bool includeTeacher = false, bool includeCourse = false, bool includeSession = false, bool filterSession = false);
        Task<List<Class>> GetClassListByTeacherId(int teacherId);
        Task<Class?> GetByClassName(string className);
        Task<int> GetIdByClassId(int id);
    }
}
