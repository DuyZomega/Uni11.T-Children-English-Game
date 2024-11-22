using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class StudentHomeworkViewModel
    {
        public int Point { get; set; }

        public TimeOnly? Playtime { get; set; }

        public string? Status { get; set; }

        public int? CorrectAnswers { get; set; }

        public HomeworkViewModel Homework { get; set; } = null!;

        public HomeworkResultViewModel HomeworkResult { get; set; } = null!;

        public List<StudentAnswerViewModel> StudentAnswers { get; set; } = new List<StudentAnswerViewModel>();

        public StudentProgressViewModel StudentProgress { get; set; } = null!;
    }
}
