using CEG_BAL.ViewModels;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IRegisteredCourseService
    {
        void Create(RegisteredCourseViewModel model);
        void Update(RegisteredCourseViewModel model);
        Task<List<RegisteredCourseViewModel>> GetAllRegisteredCourse();
        Task<RegisteredCourseViewModel> GetRegisteredCourseById(int id);
    }
}
