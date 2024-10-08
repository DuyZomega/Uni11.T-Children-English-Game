using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
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
        Task<ParentViewModel?> GetParentById(int id);
        Task<ParentViewModel?> GetParentByAccountId(int id);
        Task<bool> IsParentExistByEmail(string email);
        void Create(ParentViewModel parent, CreateNewParent newPar);
        void Update(ParentViewModel parent);
    }
}
