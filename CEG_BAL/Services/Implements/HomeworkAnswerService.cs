using AutoMapper;
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
    public class HomeworkAnswerService : IHomeworkAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HomeworkAnswerService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Create(HomeworkAnswerViewModel model, CreateNewAnswer newAnsw)
        {
            var answ = _mapper.Map<HomeworkAnswer>(model);
            if (newAnsw != null)
            {
                answ.Answer = newAnsw.Answer;
                answ.Type = newAnsw.Type;
                answ.HomeworkQuestionId = newAnsw.QuestionId.Value;
            }
            _unitOfWork.HomeworkAnswerRepositories.Create(answ);
            _unitOfWork.Save();
        }

        public async Task<HomeworkAnswerViewModel?> GetAnswerById(int id)
        {
            var answ = await _unitOfWork.HomeworkAnswerRepositories.GetByIdNoTracking(id);
            if (answ != null)
            {
                var answvm = _mapper.Map<HomeworkAnswerViewModel>(answ);
                return answvm;
            }
            return null;
        }

        public async Task<List<HomeworkAnswerViewModel>> GetAnswerList()
        {
            return _mapper.Map<List<HomeworkAnswerViewModel>>(await _unitOfWork.HomeworkAnswerRepositories.GetAnswersList());
        }

        public void Update(HomeworkAnswerViewModel model)
        {
            var answ = _mapper.Map<HomeworkAnswer>(model);
            var answerDefault = _unitOfWork.HomeworkAnswerRepositories.GetByIdNoTracking(model.HomeworkAnswerId.Value).Result;
            answ.HomeworkQuestionId = answerDefault.HomeworkQuestionId;
            _unitOfWork.HomeworkAnswerRepositories.Update(answ);
            _unitOfWork.Save();
        }
    }
}
