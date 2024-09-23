using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_DAL.Infrastructure;
using Microsoft.Extensions.Configuration;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_BAL.ViewModels.Admin;

namespace CEG_BAL.Services.Implements
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public HomeworkService(
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
        public void Create(HomeworkViewModel model, CreateNewHomework newHw)
        {
            var hw = _mapper.Map<Homework>(model);
            hw.Status = "Draft";
            if (newHw != null)
            {
                hw.Hours = newHw.Hours;
                hw.SessionId = _unitOfWork.SessionRepositories.GetIdByTitle(newHw.SessionTitle).Result;
            }
            _unitOfWork.HomeworkRepositories.Create(hw);
            _unitOfWork.Save();
        }

        public async Task<List<HomeworkViewModel>> GetHomeworkList()
        {
            return _mapper.Map<List<HomeworkViewModel>>(await _unitOfWork.HomeworkRepositories.GetHomeworksList());
        }

        public async Task<HomeworkViewModel?> GetHomeworkById(int id)
        {
            var user = await _unitOfWork.HomeworkRepositories.GetByIdNoTracking(id);
            if(user != null)
            {
                var urs = _mapper.Map<HomeworkViewModel>(user);
                return urs;
            }
            return null;
        }

        public void Update(HomeworkViewModel model)
        {
            var home = _mapper.Map<Homework>(model);
            _unitOfWork.HomeworkRepositories.Update(home);
            _unitOfWork.Save();
        }
    }
}
