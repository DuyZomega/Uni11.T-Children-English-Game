using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CEG_RazorWebApp.Models.Schedule.Create
{
    public class CreateScheduleVM
    {
        [Required(ErrorMessage = "Schedule date is required")]
        [DisplayName("Schedule date")]
        [DataType(DataType.DateTime)]
        public DateTime? ScheduleDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Schedule Session Id is required")]
        [DisplayName("Schedule Session Id")]
        public int SessionId { get; set; }
    }
}
