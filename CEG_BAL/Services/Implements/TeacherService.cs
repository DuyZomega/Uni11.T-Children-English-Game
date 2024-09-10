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
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public TeacherService(
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

        public void Create(TeacherViewModel teacher, CreateNewTeacher newTeach)
        {
            var acc = _mapper.Map<Teacher>(teacher);
            _unitOfWork.TeacherRepositories.Create(acc);
            _unitOfWork.Save();
        }

        public void Update(TeacherViewModel teacher)
        {
            var acc = _mapper.Map<Teacher>(teacher);
            _unitOfWork.TeacherRepositories.Update(acc);
            _unitOfWork.Save();
        }
    }
}
