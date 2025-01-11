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
        Task<List<Class>> GetList();
        Task<List<Class>> GetListByStartDate(DateTime claStartDate);
        Task<List<Class>> GetListByEndDate(DateTime claEndDate);
        Task<List<Class>> GetListHome();
        Task<List<Class>> GetOptionListByStatusOpen(string filterClassByStudentName = "");
        Task<List<Class>> GetClassListParent();
        /// <summary>
        /// Get Class Info by Class id
        /// </summary>
        /// <param name="id">Class id</param>
        /// <param name="includeTeacher">Default: false, determine whether if the query should include teacher info</param>
        /// <param name="includeCourse">Default: false, determine whether if the query should include course info</param>
        /// <param name="includeSession">Default: false, determine whether if the query should include course's sessions info</param>
        /// <param name="filterSession">Default: false, determine whether if the query should include filter session infos to only contain unscheduled session</param>
        /// <param name="includeSchedule">Default: false, determine whether if the query should include class's schedules info</param>
        /// <param name="includeAttendances">Default: false, determine whether if the query should include class's schedules attendances info</param>
        Task<Class?> GetByIdNoTracking(
            int id, 
            bool includeTeacher = false, 
            bool includeCourse = false, 
            bool includeSession = false, 
            bool filterSession = false,
            bool includeSchedule = false,
            bool includeAttendances = false
        );
        Task<List<Class>> GetListByTeacherId(int teacherId);
        Task<List<Class>> GetListByStudentId(int studentId);
        Task<Class?> GetByClassName(string className);
        Task<int> GetIdByClassId(int id);
        Task<int> GetTotalAmount();
        Task<int> GetTotalAmountByTeacherId(int id, string? status = null);
    }
}
