using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface ITeacherService
    {
        void Create(TeacherViewModel teacher, CreateNewTeacher newTeach);
        void Update(TeacherViewModel teacher);
    }
}
