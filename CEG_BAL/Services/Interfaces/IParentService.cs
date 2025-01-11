using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IParentService
    {
        Task<List<ParentViewModel>> GetParentList();
        Task<List<GetParentNameOption>> GetParentNameList();
        Task<ParentViewModel?> GetParentById(int id);
        Task<ParentViewModel?> GetParentByAccountId(int id);
        Task<bool> IsParentExistByEmail(string email);
        Task<bool> IsExistByFullname(string fullname);
        void Create(ParentViewModel parent, CreateNewParent newPar);
        void Update(ParentViewModel parent, UpdateParent parentNewInfo);
    }
}
