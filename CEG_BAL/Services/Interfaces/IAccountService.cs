using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Authenticates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenResponse> AuthenticateAccount(AuthenRequest request);
        Task<AccountViewModel?> GetByLogin(string userName, string password);
        Task<AccountViewModel?> GetById(int id);
        void CreateTeacher(AccountViewModel account, CreateNewAccount newTeach);
        void CreateAccount(AccountViewModel account, CreateNewAccount newAcc);
        //void CreateStudent(AccountViewModel account, CreateNewStudent newstu);
        void Update(AccountViewModel account);
        Task<int> GetIdByUsername(string username);
    }
}
