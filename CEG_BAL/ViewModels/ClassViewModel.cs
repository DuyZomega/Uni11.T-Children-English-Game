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

        public int? NumberOfStudents { get; set; }

        public virtual ICollection<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

        public virtual ICollection<StudentProcessViewModel> StudentProcesses { get; set; } = new List<StudentProcessViewModel>();

        public virtual TeacherViewModel Teacher { get; set; } = null!;
    }
}
