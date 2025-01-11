using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface ICourseService
    {
        void Create(CourseViewModel course, CreateNewCourse newCourse);
        void Update(CourseViewModel course);
        void UpdateStatus(int courseId, string courseStatus);
        Task<List<CourseViewModel>> GetCourseList();
        Task<int> GetTotalAmount();
        Task<List<CourseViewModel>?> GetListByStatus(string status);
        Task<List<string>> GetCourseNameList();
        Task<List<string>> GetCourseNameByStatusList(string status);
        Task<CourseViewModel?> GetCourseById(int id);
        Task<bool> IsExistByName(string name);
        Task<bool> IsAvailableByName(string name);
        Task<bool> IsExistById(int id);
        Task<bool> IsAvailableById(int id);
    }
}
