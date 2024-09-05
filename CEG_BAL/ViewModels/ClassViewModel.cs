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

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

        public virtual ICollection<StudentProcess> StudentProcesses { get; set; } = new List<StudentProcess>();

        public virtual Teacher Teacher { get; set; } = null!;
    }
}
