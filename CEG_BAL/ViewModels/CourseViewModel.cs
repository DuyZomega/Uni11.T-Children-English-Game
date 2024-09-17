using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class CourseViewModel
    {

        public string CourseName { get; set; } = null!;

        public string CourseType { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? Status { get; set; }

        public virtual ICollection<ClassViewModel> Classes { get; set; } = new List<ClassViewModel>();

        public virtual ICollection<HomeworkViewModel> Homeworks { get; set; } = new List<HomeworkViewModel>();

        public virtual ICollection<RegisteredClassViewModel> RegisteredClasses { get; set; } = new List<RegisteredClassViewModel>();

        public virtual ICollection<SessionViewModel> Sessions { get; set; } = new List<SessionViewModel>();
    }
}
