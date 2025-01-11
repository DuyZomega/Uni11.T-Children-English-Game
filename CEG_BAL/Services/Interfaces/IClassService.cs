using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IClassService
    {
        Task Create(CreateNewClass newCla);
        Task Update(int claId, UpdateClass upCla);
        Task UpdateStatus(int claId, string upClaStatus);
        Task<bool> UpdateListStatusByDate();
        Task<List<ClassViewModel>> GetClassList();
        Task<List<ClassViewModel>> GetClassListHome();
        Task<List<GetClassForTransaction>> GetOptionListByStatusOpen(string filterClassByStudentName = "");
        Task<List<ClassViewModel>> GetClassListParent();
        Task<List<ClassViewModel>> GetListByTeacherAccountId(int id);
        Task<List<ClassViewModel>> GetListByStudentAccountId(int id);
        Task<List<ClassViewModel>> GetClassListFilter(ClassFilter filter);
        /// <summary>
        /// Get Class Info by Class id. 
        /// int id, class id to be use to query. 
        /// bool includeTeacher = true, determine whether if the query should include teacher info. 
        /// bool includeCourse = true, determine whether if the query should include course info. 
        /// bool includeSession = false, determine whether if the query should include course's sessions info. 
        /// bool filterSession = false, determine whether if the query should include filter session infos to only contain unscheduled session. 
        /// </summary>
        /// <param name="id">Class id</param>
        /// <param name="includeTeacher">Default: false, determine whether if the query should include teacher info</param>
        /// <param name="includeCourse">Default: false, determine whether if the query should include course info</param>
        /// <param name="includeSession">Default: false, determine whether if the query should include course's sessions info</param>
        /// <param name="filterSession">Default: false, determine whether if the query should include filter session infos to only contain unscheduled session</param>
        /// <param name="includeSchedule">Default: false, determine whether if the query should include class's schedules info</param>
        /// <param name="includeAttendances">Default: false, determine whether if the query should include class's schedules attendances info</param>
        Task<ClassViewModel?> GetById(
            int id, 
            bool includeTeacher = false, 
            bool includeCourse = false, 
            bool includeSession = false, 
            bool filterSession = false,
            bool includeSchedule = false,
            bool includeAttendances = false
        );
        Task<ClassViewModel?> GetByIdParent(int id);
        Task<ClassViewModel?> GetByClassName(string className);
        Task<bool> IsEditableById(int id);
        Task<int> GetTotalAmount();
        Task<int> GetTotalAmountByTeacherAccountIdAndClassStatus(int id, string? status = null);
        Task<bool> CheckClassFull(string className);
    }
}
