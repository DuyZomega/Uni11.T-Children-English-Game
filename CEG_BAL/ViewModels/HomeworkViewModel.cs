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
        public string Description { get; set; } = null!;

        public string Title { get; set; } = null!;

        public int? TotalPoint { get; set; }

        public int? WordAmount { get; set; }
        public int? Hours { get; set; }

        public string? Status { get; set; }

        public virtual GameConfigViewModel GameConfig { get; set; } = null!;

        public virtual ICollection<HomeworkResultViewModel> HomeworkResults { get; set; } = new List<HomeworkResultViewModel>();

        public virtual SessionViewModel Session { get; set; } = null!;

        public virtual ICollection<StudentHomeworkViewModel> StudentHomeworks { get; set; } = new List<StudentHomeworkViewModel>();
    }
}
