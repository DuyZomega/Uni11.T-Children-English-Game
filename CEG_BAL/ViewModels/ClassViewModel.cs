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
        public int? ClassId { get; set; }

        public string ClassName { get; set; } = null!;

        public int? MinimumStudents { get; set; }

        public int? MaximumStudents { get; set; }
        //startDate (30/9), endDate(30/10), daysInWeek(T2, T5) Phải sync ngày và thứ tạo (30/9 là T2)
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<StudentProgressViewModel> StudentProgress { get; set; } = new List<StudentProgressViewModel>();

        public TeacherViewModel? Teacher { get; set; }
        public CourseViewModel? Course { get; set; }
        public string Status { get; set; }
    }
}
