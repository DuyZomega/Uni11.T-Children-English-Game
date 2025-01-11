
namespace CEG_BAL.ViewModels
{
    public class ScheduleViewModel
    {
        public int? ScheduleId { get; set; }

        public int ScheduleNumber { get; set; }

        public int? SessionId { get; set; }

        public int? ClassId { get; set; }
        
        public DateTime? ScheduleDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string? Status { get; set; }

        public List<AttendanceViewModel>? Attendances { get; set; } = new List<AttendanceViewModel>();

        public ClassViewModel? Class { get; set; }

        public SessionViewModel? Session { get; set; }
    }
}
