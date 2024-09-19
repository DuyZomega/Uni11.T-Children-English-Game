using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin
{
    public class CreateNewCourse
    {
        public string CourseName { get; set; } = null!;

        public string CourseType { get; set; } = null!;

        public string Description { get; set; } = null!;
        public int TotalHours { get; set; }
        public string Image { get; set; }
        public int RequiredAge { get; set; }
        public string Difficulty { get; set; }
        public string Category { get; set; }
    }
}
