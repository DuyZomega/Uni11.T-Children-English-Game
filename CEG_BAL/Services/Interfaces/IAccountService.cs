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
        Task<List<AccountViewModel>> GetAccountList();
        Task<AuthenResponse> AuthenticateAccount(AuthenRequest request);
        Task<AccountViewModel?> GetByLogin(string userName, string password);
        Task<AccountViewModel?> GetById(int id);
        Task<bool> IsAccountExistByUsername(string username);
        void Create(AccountViewModel account, CreateNewAccount newAcc);
        void Update(AccountViewModel account);
        Task<bool> Delete(int id);
        Task<int> GetIdByUsername(string username);
    }
}
