using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
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
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public ClassService(
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

        public void Create(ClassViewModel classModel)
        {
            var clas = _mapper.Map<Class>(classModel);
            _unitOfWork.ClassRepositories.Create(clas);
            _unitOfWork.Save();
        }

        public async Task<ClassViewModel> GetClassById(int id)
        {
            var user = await _unitOfWork.ClassRepositories.GetByIdNoTracking(id);
            if(user != null)
            {
                var usr = _mapper.Map<ClassViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<List<ClassViewModel>> GetClassList()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassList());
        }

        public void Update(ClassViewModel classModel)
        {
            var clas = _mapper.Map<Class>(classModel);
            _unitOfWork.ClassRepositories.Update(clas);
            _unitOfWork.Save();
        }
    }
}
