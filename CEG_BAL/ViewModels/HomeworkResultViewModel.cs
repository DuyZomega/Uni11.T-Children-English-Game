using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class HomeworkResultViewModel
    {
        public int? TotalPoint { get; set; }

        public int? TotalCorrectAnswers { get; set; }

        public TimeOnly? Playtime { get; set; }

        public List<StudentHomeworkViewModel> StudentHomework { get; set; } = new List<StudentHomeworkViewModel>();
    }
}
