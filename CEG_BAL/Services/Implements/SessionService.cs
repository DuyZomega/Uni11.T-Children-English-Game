﻿using AutoMapper;
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
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public SessionService(
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
        public void Create(SessionViewModel model)
        {
            var sess = _mapper.Map<Session>(model);
            _unitOfWork.SessionRepositories.Create(sess);
            _unitOfWork.Save();
        }

        public async Task<List<SessionViewModel>> GetAllSessions()
        {
            return _mapper.Map<List<SessionViewModel>>(await _unitOfWork.SessionRepositories.GetSessionsList());
        }

        public async Task<SessionViewModel> GetSessionById(int id)
        {
            var user = await _unitOfWork.SessionRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<SessionViewModel>(user);
                return urs;
            }
            return null;
        }

        public void Update(SessionViewModel model)
        {
            var sess = _mapper.Map<Session>(model);
            _unitOfWork.SessionRepositories.Update(sess);
            _unitOfWork.Save();
        }
    }
}
