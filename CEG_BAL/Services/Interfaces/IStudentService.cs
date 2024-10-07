using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentViewModel>> GetStudentList();
        Task<StudentViewModel?> GetStudentById(int id);
        Task<List<StudentViewModel>> GetStudentByParentId(int parentId);
        void Create(StudentViewModel student, CreateNewStudent newStu);
        void Update(StudentViewModel student);
    }
}
