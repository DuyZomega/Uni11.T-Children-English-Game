using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account;
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
        void Create(AccountViewModel account, CreateNewAccount newacc);
        void Update(AccountViewModel account);
    }
}
