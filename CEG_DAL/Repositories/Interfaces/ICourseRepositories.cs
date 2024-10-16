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
        Task<List<string>?> GetCourseNameList();
        Task<List<string>?> GetCourseNameByStatusList(string status);
        Task<Course?> GetByIdNoTrackingInclude(int id);
        Task<Course?> GetByIdNoTracking(int id);
        Task<string?> GetStatusByCourseIdNoTracking(int courseId);
        Task<string?> GetStatusBySessionIdNoTracking(int sessionId);
        Task<string?> GetStatusByHomeworkIdNoTracking(int homeworkId);
        Task<string?> GetStatusByQuestionIdNoTracking(int questionId);
        Task<Course?> GetByIdNoTracking(int id, bool includeSessions, bool includeHomeworks);
        Task<Course?> GetByName(string name);
        Task<int> GetIdByName(string name);
    }
}
