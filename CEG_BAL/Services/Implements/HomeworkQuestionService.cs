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
    public class HomeworkQuestionService : IHomeworkQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeworkQuestionService(
            IUnitOfWork unitOfWork, 
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(HomeworkQuestionViewModel model, CreateNewQuestion newQus)
        {
            var ques = _mapper.Map<HomeworkQuestion>(model);
            if (newQus != null)
            {
                ques.Question = newQus.Question;
                if(ques.Homework?.HomeworkId == 0)
                {
                    ques.Homework = null;
                }
                /*ques.HomeworkId = _unitOfWork.HomeworkRepositories.GetIdByTitle(newQus.HomeworkTitle).Result;*/
            }
            _unitOfWork.HomeworkQuestionRepositories.Create(ques);
            _unitOfWork.Save();
        }

        public async Task<List<HomeworkQuestionViewModel>?> GetOrderedQuestionList()
        {
            return _mapper.Map<List<HomeworkQuestionViewModel>?>(await _unitOfWork.HomeworkQuestionRepositories.GetOrderedQuestionList());
        }

        public async Task<HomeworkQuestionViewModel?> GetQuestionById(int id)
        {
            var ques = await _unitOfWork.HomeworkQuestionRepositories.GetByIdNoTracking(id);
            if (ques != null)
            {
                var quesvm = _mapper.Map<HomeworkQuestionViewModel>(ques);
                if (ques.HomeworkId != null)
                {
                    var home = await _unitOfWork.HomeworkRepositories.GetByIdNoTracking(ques.HomeworkId.Value);
                    quesvm.HomeworkStatus = home != null ? home.Status : null;
                }
                return quesvm;
            }
            return null;
        }

        public async Task<List<HomeworkQuestionViewModel>> GetQuestionList()
        {
            return _mapper.Map<List<HomeworkQuestionViewModel>>(await _unitOfWork.HomeworkQuestionRepositories.GetQuestionList());
        }

        public void Update(HomeworkQuestionViewModel model)
        {
            var ques = _mapper.Map<HomeworkQuestion>(model);

            /*var questionDefault = _unitOfWork.HomeworkQuestionRepositories.GetByIdNoTracking(model.HomeworkQuestionId.Value).Result;
            //ques.HomeworkId = questionDefault.HomeworkId;
            questionDefault.Question = model.Question;*/
            if (ques.Homework?.HomeworkId == 0)
            {
                ques.Homework = null;
            }
            _unitOfWork.HomeworkQuestionRepositories.Update(ques);
            _unitOfWork.Save();
        }
    }
}
