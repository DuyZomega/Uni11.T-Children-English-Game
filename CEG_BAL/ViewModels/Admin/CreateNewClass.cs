using CEG_BAL.Attributes;
using CEG_BAL.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin
{
    public class CreateNewClass
    {
        [Required(ErrorMessage = "Class name is required")]
        [DisplayName("Class Name")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Minimum students amount is required")]
        [Range(CEGConstants.CLASS_MINIMUM_STUDENTS_REQ, CEGConstants.CLASS_MAXIMUM_STUDENTS_REQ)]
        [DisplayName("Minimum students amount")]
        public int MinimumStudents { get; set; }
        [Required(ErrorMessage = "Maximum students amount is required")]
        [Range(CEGConstants.CLASS_MINIMUM_STUDENTS_REQ, CEGConstants.CLASS_MAXIMUM_STUDENTS_REQ)]
        [DisplayName("Maximum students amount")]
        public int MaximumStudents { get; set; }
        [Required(ErrorMessage = "Assign teacher id is required")]
        [DisplayName("Assign teacher id")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Assign course id is required")]
        [Range(1, int.MaxValue)]
        [DisplayName("Assign course id")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Class start date is required")]
        [DisplayName("Class start date")]
        [DataType(DataType.DateTime)]
        //startDate (30/9), endDate(30/10), daysInWeek(T2, T5) Phải sync ngày và thứ tạo (30/9 là T2)
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Class end date is required")]
        [DateGreaterThan("StartDate", CEGConstants.CLASS_MINIMUM_DAYS_REQ)]
        [DisplayName("Class end date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Enrollment Fee is required")]
        [Range(CEGConstants.CLASS_MINIMUM_ENROLLMENT_FEE, CEGConstants.CLASS_MAXIMUM_ENROLLMENT_FEE)]
        [DisplayName("Enrollment Fee")]
        public int EnrollmentFee { get; set; } = 1000000;
        [Required(ErrorMessage = "Class schedule list is required")]
        [DisplayName("Class schedule list")]
        public List<CreateNewSchedule> Schedules { get; set; } = new List<CreateNewSchedule> { };
    }
}
