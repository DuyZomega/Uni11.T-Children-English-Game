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
    }
}
