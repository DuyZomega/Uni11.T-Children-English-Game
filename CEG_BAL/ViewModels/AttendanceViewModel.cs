using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class AttendanceViewModel
    {
        public int? AttendanceId { get; set; }
        public int StudentId { get; set; }
        public string HasAttended { get; set; } = null!;

        public ScheduleViewModel Schedule { get; set; } = null!;

        public StudentViewModel Student { get; set; } = null!;
    }
}
