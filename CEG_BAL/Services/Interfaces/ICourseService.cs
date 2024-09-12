using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Course;
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
        Task<List<CourseViewModel>> GetCourseList();
        Task<CourseViewModel> GetCourseById(int id);
    }
}
