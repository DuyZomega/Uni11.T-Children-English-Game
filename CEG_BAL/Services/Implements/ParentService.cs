using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
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
            acc.Account.AccountId = _unitOfWork.AccountRepositories.GenerateNewAccountId().Result;
            acc.Account.CreatedDate = DateTime.Now;
            acc.Account.Status = "Active";
            acc.Account.RoleId = _unitOfWork.RoleRepositories.GetRoleIdByRoleName("Parent").Result;
            if (newPar != null)
            {
                acc.Account.Fullname = newPar.Account.Fullname;
                acc.Account.Username = newPar.Account.Username;
                acc.Account.Gender = newPar.Account.Gender;
                acc.Email = newPar.Email;
                acc.Phone = newPar.Phone;
                acc.Address = newPar.Address;
            }
            _unitOfWork.ParentRepositories.Create(acc);
            _unitOfWork.Save();
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

        public async Task<bool> IsParentExistByEmail(string email)
        {
            var acc = await _unitOfWork.ParentRepositories.GetByEmail(email);
            if (acc != null) return true;
            return false;
        }

        public void Update(ParentViewModel parent)
        {
            var par = _mapper.Map<Parent>(parent);
            _unitOfWork.ParentRepositories.Update(par);
            _unitOfWork.Save();
        }
    }
}
