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
        public int? TotalHours { get; set; }
        public string? CourseImageHeader { get; set; }
        public int? RequiredAge { get; set; }
        public string? Difficulty { get; set; }
        public string? Category { get; set; }

        public string Description { get; set; } = null!;

        public string? Status { get; set; }

        public List<ClassViewModel>? Classes { get; set; } = new List<ClassViewModel>();

        public List<SessionViewModel>? Sessions { get; set; } = new List<SessionViewModel>();
    }
}
