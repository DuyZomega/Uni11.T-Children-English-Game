using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IHomeworkService
    {
        void Create(HomeworkViewModel model, CreateNewHomework newHw);
        void Update(HomeworkViewModel model);
        Task<List<HomeworkViewModel>> GetHomeworkList();
        Task<bool> IsHomeworkExistByTitle(string title);
        Task<HomeworkViewModel?> GetHomeworkById(int id);
    }
}
