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
        public int HomeworkId { get; set; }

        public int StudentProgressId { get; set; }

        public int HomeworkResultId { get; set; }

        public int HomeworkNumber { get; set; }

        public int Point { get; set; }

        public TimeSpan Playtime { get; set; }

        public string? Status { get; set; }

        public int? CorrectAnswers { get; set; }

        public HomeworkViewModel Homework { get; set; } = null!;

        public HomeworkResultViewModel HomeworkResult { get; set; } = null!;

        public List<StudentAnswerViewModel> StudentAnswers { get; set; } = new List<StudentAnswerViewModel>();

        public StudentProgressViewModel StudentProgress { get; set; } = null!;
    }
}
