using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountViewModel?> GetByLogin(string userName, string password);
        Task<AccountViewModel?> GetById(int id);
        void CreateTeacher(AccountViewModel account, CreateNewTeacher newteach);
        void CreateParent(AccountViewModel account, CreateNewParent newpar);
        void CreateStudent(AccountViewModel account, CreateNewStudent newstu);
        void Update(AccountViewModel account);
    }
}
