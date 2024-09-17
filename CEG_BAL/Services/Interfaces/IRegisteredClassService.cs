using CEG_BAL.ViewModels;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IRegisteredClassService
    {
        void Create(RegisteredClassViewModel model);
        void Update(RegisteredClassViewModel model);
        Task<List<RegisteredClassViewModel>> GetAllRegisteredCourse();
        Task<RegisteredClassViewModel> GetRegisteredCourseById(int id);
    }
}
