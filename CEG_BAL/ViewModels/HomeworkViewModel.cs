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

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }

        public virtual GameConfig GameConfig { get; set; } = null!;

        public virtual ICollection<HomeworkResult> HomeworkResults { get; set; } = new List<HomeworkResult>();

        public virtual Course Session { get; set; } = null!;

        public virtual ICollection<StudentHomework> StudentHomeworks { get; set; } = new List<StudentHomework>();
    }
}
