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
        Task<List<string>> GetCourseNameList();
        Task<CourseViewModel?> GetCourseById(int id);
        Task<bool> IsCourseExistByName(string name);
        Task<bool> IsCourseAvailableByName(string name);
    }
}
