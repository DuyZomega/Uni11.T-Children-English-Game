
namespace CEG_BAL.ViewModels
{
    public class ScheduleViewModel
    {
        public int? ScheduleId { get; set; }

        public string? DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string? Status { get; set; }

        public ClassViewModel? Class { get; set; }

        public SessionViewModel? Session { get; set; }
    }
}
