using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using CEG_DAL.Repositories.Interfaces;
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

        public void Create(AccountViewModel account, CreateNewAccount newacc)
        {
            var acc = _mapper.Map<Account>(account);
            _unitOfWork.AccountRepositories.Create(acc);
            _unitOfWork.Save();
        }

        public void Update(AccountViewModel account)
        {
            var acc = _mapper.Map<Account>(account);
            _unitOfWork.AccountRepositories.Update(acc);
            _unitOfWork.Save();
        }
    }
}
