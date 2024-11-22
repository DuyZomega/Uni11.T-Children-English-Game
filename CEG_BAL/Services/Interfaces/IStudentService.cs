using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_BAL.ViewModels.Admin.Update;

namespace CEG_BAL.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentViewModel>> GetStudentList();
        Task<List<string>> GetStudentNameList();
        Task<List<string>> GetStudentNameListByParent(int id);
        Task<List<string>> GetStudentNameListByParentName(string parentName);
        Task<StudentViewModel?> GetStudentById(int id);
        Task<StudentViewModel?> GetStudentByAccountId(int id);
        Task<List<StudentViewModel>> GetStudentByParentAccountId(int id);
        Task<List<StudentViewModel>> GetStudentByClassId(int id);
        void Create(StudentViewModel student, CreateNewStudent newStu);
        void Update(StudentViewModel student, UpdateStudent studentNewInfo);
    }
}
