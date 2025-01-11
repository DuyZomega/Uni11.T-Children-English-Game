using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class StudentProgressViewModel
    {
        public int? TotalPoint { get; set; }

        public int StudentId { get; set; }

        public TimeSpan Playtime { get; set; }

        public ClassViewModel? Class { get; set; }

        public List<HomeworkResultViewModel> HomeworkResults { get; set; } = new List<HomeworkResultViewModel>();

        public StudentViewModel? Student { get; set; }

        public List<StudentHomeworkViewModel> StudentHomeworks { get; set; } = new List<StudentHomeworkViewModel>();
    }
}
