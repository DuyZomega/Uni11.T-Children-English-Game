using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class HomeworkQuestionViewModel
    {
        public int? HomeworkQuestionId { get; set; }
        public string? Question { get; set; }
        public string? HomeworkStatus { get; set; }
        public string? CourseStatus { get; set; }
        public int? AnswersAmount { get; set; }

        public HomeworkViewModel? Homework { get; set; }

        public List<HomeworkAnswerViewModel>? HomeworkAnswers { get; set; } = new List<HomeworkAnswerViewModel>();
    }
}
