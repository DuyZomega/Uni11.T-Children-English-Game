using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IClassService
    {
        void Create(ClassViewModel classModel);
        void Update(ClassViewModel classModel);
        Task<List<ClassViewModel>> GetClassList();
        Task<ClassViewModel> GetClassById(int id);
    }
}
