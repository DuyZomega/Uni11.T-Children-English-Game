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

        public TimeOnly? Playtime { get; set; }

        public virtual Class Class { get; set; } = null!;

        public virtual ICollection<HomeworkResult> HomeworkResults { get; set; } = new List<HomeworkResult>();

        public virtual RegisteredClass Session { get; set; } = null!;

        public virtual Student Student { get; set; } = null!;

        public virtual ICollection<StudentHomework> StudentHomeworks { get; set; } = new List<StudentHomework>();
    }
}
