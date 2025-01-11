using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Get
{
    // Merge of two View Model: Attendance and Student Progress
    public class GetStudentActivity
    {
        public int? AttendanceId { get; set; }
        public int StudentId { get; set; }
        public string HasAttended { get; set; } = null!;

        public int HomeworkCurrentProgress { get; set; } = 0;
        public int HomeworkAmount { get; set; } = 0;

        public StudentProgressViewModel? StudentProgress { get; set; }

        public ScheduleViewModel Schedule { get; set; } = null!;

        public StudentViewModel Student { get; set; } = null!;
    }
}
