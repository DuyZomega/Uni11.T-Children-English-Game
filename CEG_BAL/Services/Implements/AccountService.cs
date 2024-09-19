using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Authenticates;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public AccountService (
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTService jwtServices,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtServices;
            _configuration = configuration;
        }

        public async Task<List<AccountViewModel>> GetAccountList()
        {
            return _mapper.Map<List<AccountViewModel>>(await _unitOfWork.AccountRepositories.GetAccountList());
        }

        public async Task<AuthenResponse> AuthenticateAccount(AuthenRequest request)
        {
            var acc = await _unitOfWork.AccountRepositories.GetByLogin(request.Username, request.Password);
            if (acc != null)
            {
                if (acc.Status != "Active")
                {
                    return new AuthenResponse()
                    {
                        AccountId = acc.AccountId.ToString(),
                        UserName = acc.Username,
                        Status = acc.Status,
                    };
                }
                var role = await _unitOfWork.AccountRepositories.GetRoleByAccountId(acc.AccountId);
                var accessToken = _jwtService.GenerateJWTToken(acc.AccountId.ToString(), acc.Username, role, _configuration);
                return new AuthenResponse()
                {
                    AccountId = acc.AccountId.ToString(),
                    RoleName = role,
                    UserName = acc.Username,
                    AccessToken = accessToken,
                    Status = acc.Status,
                };
            }
            return null;
        }

        public async Task<AccountViewModel?> GetByLogin(string username, string password)
        {
            var user = await _unitOfWork.AccountRepositories.GetByLogin(username, password);
            if (user != null)
            {
                var usr = _mapper.Map<AccountViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<AccountViewModel?> GetById(int id)
        {
            var user = await _unitOfWork.AccountRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                //var mem = await _unitOfWork.MemberRepository.GetByIdNoTracking(user.MemberId);
                var usr = _mapper.Map<AccountViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<bool> IsAccountExistByUsername(string username)
        {
            var acc = await _unitOfWork.AccountRepositories.GetByUsername(username);
            if (acc != null) return true;
            return false;
        }

        public void Create(AccountViewModel account, CreateNewAccount newAcc)
        {
            var acc = _mapper.Map<Account>(account);
            acc.CreatedDate = DateTime.Now;
            acc.Status = "Active";
            if (newAcc != null)
            {
                acc.Fullname = newAcc.Fullname;
                acc.Username = newAcc.Username;
                acc.Password = newAcc.Password;
                acc.Gender = newAcc.Gender;
                acc.RoleId = _unitOfWork.RoleRepositories.GetRoleIdByRoleName(newAcc.Role).Result;
            }
            _unitOfWork.AccountRepositories.Create(acc);
            _unitOfWork.Save();
        }

        //public void CreateStudent(AccountViewModel account, CreateNewStudent newstu)
        //{
        //    var acc = _mapper.Map<Account>(account);
        //    acc.Role.RoleName = "Student";
        //    acc.CreatedDate = DateTime.Now;
        //    _unitOfWork.AccountRepositories.Create(acc);
        //    _unitOfWork.Save();
        //}

        public void Update(AccountViewModel account)
        {
            var acc = _mapper.Map<Account>(account);
            _unitOfWork.AccountRepositories.Update(acc);
            _unitOfWork.Save();
        }

        public async Task<int> GetIdByUsername(string username)
        {
            return await _unitOfWork.AccountRepositories.GetIdByUsername(username);
        }
    }
}
