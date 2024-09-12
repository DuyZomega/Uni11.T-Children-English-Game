using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface ICourseService
    {
        void Create(CourseViewModel course);
        void Update(CourseViewModel course);
        Task<List<CourseViewModel>> GetCourseList();
        Task<CourseViewModel> GetCourseById(int id);
    }
}
