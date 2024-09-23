using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class HomeworkQuestionViewModel
    {
        public string? Question { get; set; }

        public HomeworkViewModel Homework { get; set; } = null!;

        public List<HomeworkAnswerViewModel> HomeworkAnswers { get; set; } = new List<HomeworkAnswerViewModel>();
    }
}
