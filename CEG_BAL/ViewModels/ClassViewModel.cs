using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class ClassViewModel
    {

        public string ClassName { get; set; } = null!;

        public int? MinimumStudents { get; set; }

        public int? MaximumStudents { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //public virtual ICollection<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

        public virtual ICollection<StudentProgressViewModel> StudentProgress { get; set; } = new List<StudentProgressViewModel>();

        public virtual TeacherViewModel Teacher { get; set; } = null!;
        public virtual CourseViewModel Course { get; set; } = null!;
    }
}
