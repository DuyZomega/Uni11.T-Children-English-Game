using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin
{
    public class CreateNewClass
    {
        public string ClassName { get; set; }
        public int MinStudents { get; set; }
        public int MaxStudents { get; set; }
        public string TeacherName { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
