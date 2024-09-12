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
    public class EnrollService : IEnrollService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public EnrollService(
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
        public void Create(EnrollViewModel model)
        {
            var en = _mapper.Map<Enroll>(model);
            _unitOfWork.EnrollRepositories.Create(en);
            _unitOfWork.Save();
        }

        public async Task<EnrollViewModel> GetEnrollById(int id)
        {
            var user = await _unitOfWork.EnrollRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<EnrollViewModel>(user);
                return urs;
            }
            return null;
        }

        public async Task<List<EnrollViewModel>> GetEnrollsList()
        {
            return _mapper.Map<List<EnrollViewModel>>(await  _unitOfWork.EnrollRepositories.GetEnrollsList());
        }

        public void Update(EnrollViewModel model)
        {
            var en = _mapper.Map<Enroll>(model);
            _unitOfWork.EnrollRepositories.Update(en);
            _unitOfWork.Save();
        }
    }
}
