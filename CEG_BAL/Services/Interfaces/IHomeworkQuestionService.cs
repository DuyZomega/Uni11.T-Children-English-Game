﻿using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IHomeworkQuestionService
    {
        void Create(HomeworkQuestionViewModel model, CreateNewQuestion newQus);
        void Update(HomeworkQuestionViewModel model);
        Task<List<HomeworkQuestionViewModel>> GetQuestionList();
        Task<HomeworkQuestionViewModel?> GetQuestionById(int id);
    }
}
