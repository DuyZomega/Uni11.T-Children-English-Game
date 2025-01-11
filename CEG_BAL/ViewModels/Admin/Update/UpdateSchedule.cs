using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Update
{
    public class UpdateSchedule
    {
        [Required(ErrorMessage = "Schedule date is required")]
        [DisplayName("Schedule date")]
        [DataType(DataType.DateTime)]
        public DateTime? ScheduleDate { get; set; } = DateTime.Now;
        //[Required(ErrorMessage = "Schedule Session Id is required")]
        //[DisplayName("Schedule Session Id")]
        //public int SessionId { get; set; }
    }
}
