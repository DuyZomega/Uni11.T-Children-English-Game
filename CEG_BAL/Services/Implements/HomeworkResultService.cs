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
    public class HomeworkResultService : IHomeworkResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public HomeworkResultService(
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
        public void Create(HomeworkResultViewModel model)
        {
            var home = _mapper.Map<HomeworkResult>(model);
            _unitOfWork.HomeworkResultRepositories.Create(home);
            _unitOfWork.Save();
        }

        public async Task<List<HomeworkResultViewModel>> GetAllHomeworkResult()
        {
            return _mapper.Map<List<HomeworkResultViewModel>>(await _unitOfWork.HomeworkResultRepositories.GetHomeworkResultsList());
        }

        public async Task<HomeworkResultViewModel> GetHomeworkResultById(int id)
        {
            var user = await _unitOfWork.HomeworkResultRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<HomeworkResultViewModel>(user);
                return urs;
            }
            return null;
        }

        public void Update(HomeworkResultViewModel model)
        {
            var home = _mapper.Map<HomeworkResult>(model);
            _unitOfWork.HomeworkResultRepositories.Update(home);
            _unitOfWork.Save();
        }
    }
}
