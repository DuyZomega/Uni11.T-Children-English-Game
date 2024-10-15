﻿using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
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
        public void Create(SessionViewModel model, CreateNewSession newSes)
        {
            var sess = _mapper.Map<Session>(model);
            sess.Status = "Draft";

            if (newSes != null)
            {
                sess.Title = newSes.Title;
                sess.Description = newSes.Description;
                sess.Hours = newSes.Hours;
                sess.Number = newSes.Number;
                sess.CourseId = _unitOfWork.CourseRepositories.GetIdByName(newSes.CourseName).Result;

                /*var sessionList = _unitOfWork.SessionRepositories.GetSessionListByCourseId(sess.CourseId).Result;
                if (sessionList != null && sessionList.Count > 0)
                {
                    foreach(var session in sessionList)
                    {
                        
                    }
                } else
                {
                    sess.Number = 1;
                }*/
            }

            _unitOfWork.SessionRepositories.Create(sess);
            _unitOfWork.Save();
        }

        public async Task<List<SessionViewModel>> GetSessionList()
        {
            return _mapper.Map<List<SessionViewModel>>(await _unitOfWork.SessionRepositories.GetSessionList());
        }

        public async Task<SessionViewModel?> GetSessionById(int id)
        {
            var user = await _unitOfWork.SessionRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<SessionViewModel>(user);
                urs.CourseStatus = await _unitOfWork.CourseRepositories.GetStatusBySessionIdNoTracking(id);
                return urs;
            }
            return null;
        }

        public void Update(SessionViewModel model)
        {
            var sess = _mapper.Map<Session>(model);
            sess.CourseId = _unitOfWork.SessionRepositories.GetByIdNoTracking(model.SessionId.Value).Result.CourseId;
            _unitOfWork.SessionRepositories.Update(sess);
            _unitOfWork.Save();
        }

        public async Task<bool> IsSessionExistByTitle(string title)
        {
            var ses = await _unitOfWork.SessionRepositories.GetByTitle(title);
            if (ses != null) return true;
            return false;
        }

        public async Task<List<SessionViewModel>> GetSessionListByCourseId(int courseId)
        {
            return _mapper.Map<List<SessionViewModel>>(await _unitOfWork.SessionRepositories.GetSessionListByCourseId(courseId));
        }
    }
}
