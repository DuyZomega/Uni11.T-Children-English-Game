using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class HomeworkViewModel
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public int? Hours { get; set; }

        public string? Status { get; set; }

        public GameConfigViewModel? GameConfig { get; set; }

        public List<HomeworkResultViewModel>? HomeworkResults { get; set; } = new List<HomeworkResultViewModel>();

        public SessionViewModel? Session { get; set; }

        public List<StudentHomeworkViewModel>? StudentHomeworks { get; set; } = new List<StudentHomeworkViewModel>();
    }
}
