using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
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
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public ParentService(
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
        public void Create(ParentViewModel parent, CreateNewParent newPar)
        {
            var acc = _mapper.Map<Parent>(parent);
            acc.Account.CreatedDate = DateTime.Now;
            acc.Account.Status = "Active";
            acc.Account.RoleId = _unitOfWork.RoleRepositories.GetRoleIdByRoleName("Parent").Result;
            if (newPar != null)
            {
                acc.Account.Fullname = newPar.Account.Fullname;
                acc.Account.Username = newPar.Account.Username;
                acc.Account.Gender = newPar.Account.Gender;
                acc.Account.Password = newPar.Account.Password;
                acc.Email = newPar.Email;
                acc.Phone = newPar.Phone;
                acc.Address = newPar.Address;
            }
            _unitOfWork.ParentRepositories.Create(acc);
            _unitOfWork.Save();
        }

        public async Task<ParentViewModel?> GetParentByAccountId(int id)
        {
            var user = await _unitOfWork.ParentRepositories.GetByAccountIdNoTracking(id);
            if (user != null)
            {
                var usr = _mapper.Map<ParentViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<ParentViewModel?> GetParentById(int id)
        {
            var user = await _unitOfWork.ParentRepositories.GetByIdNoTracking(id);
            if(user != null)
            {
                var usr = _mapper.Map<ParentViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<List<ParentViewModel>> GetParentList()
        {
            return _mapper.Map < List <ParentViewModel >>(await _unitOfWork.ParentRepositories.GetParentList());
        }

        public async Task<List<GetParentNameOption>> GetParentNameList()
        {
            return _mapper.Map<List<GetParentNameOption>>(await _unitOfWork.ParentRepositories.GetParentNameList());
        }

        public async Task<bool> IsParentExistByEmail(string email)
        {
            var acc = await _unitOfWork.ParentRepositories.GetByEmail(email);
            if (acc != null) return true;
            return false;
        }

        public async Task<bool> IsExistByFullname(string fullname)
        {
            return await _unitOfWork.ParentRepositories.GetByFullname(fullname) != null;
        }

        public void Update(ParentViewModel parent, UpdateParent parentNewInfo)
        {
            var par = _mapper.Map<Parent>(parent);
            if (parentNewInfo != null)
            {
                par.ParentId = _unitOfWork.ParentRepositories.GetIdByAccountIdNoTracking(par.Account.AccountId).Result.Value;
                par.AccountId = par.Account.AccountId;
                par.Account.Fullname = parentNewInfo.Account.Fullname;
                par.Account.Gender = parentNewInfo.Account.Gender;
                par.Phone = parentNewInfo.Phone;
                par.Address = parentNewInfo.Address;
            }
            _unitOfWork.ParentRepositories.Update(par);
            _unitOfWork.Save();
        }
    }
}
