using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Create
{
    public class CreateRemindHomeworkEmail
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int HomeworkId { get; set; }
        [Required]
        public int ScheduleId { get; set; }
    }
}
