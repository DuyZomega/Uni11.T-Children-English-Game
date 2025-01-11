using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_BAL.ViewModels.Student;

namespace CEG_BAL.Services.Interfaces
{
    public interface IStudentProgressService
    {
        Task Create(CreateNewStudentProgress model);
        Task Update(int id, UpdateStudentProgress upHomRes);
        Task<List<StudentProgressViewModel>> GetList();
        Task<StudentProgressViewModel?> GetById(int id);
        Task<StudentDashboard> GetByStudentAccountId(int id);
    }
}
