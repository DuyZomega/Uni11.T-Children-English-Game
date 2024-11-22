using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_BAL.Configurations;
using CEG_BAL.Attributes;

namespace CEG_BAL.ViewModels.Admin.Update
{
    public class UpdateClass
    {
        public int? ClassId { get; set; }
        [Required(ErrorMessage = "Class name is required")]
        [DisplayName("Class Name")]
        public string ClassName { get; set; } = null!;
        [Required(ErrorMessage = "Minimum students amount is required")]
        [Range(Constants.CLASS_MINIMUM_STUDENTS_REQ, Constants.CLASS_MAXIMUM_STUDENTS_REQ)]
        [DisplayName("Minimum students amount")]
        public int? MinimumStudents { get; set; }
        [Required(ErrorMessage = "Maximum students amount is required")]
        [Range(Constants.CLASS_MINIMUM_STUDENTS_REQ, Constants.CLASS_MAXIMUM_STUDENTS_REQ)]
        [DisplayName("Maximum students amount")]
        public int? MaximumStudents { get; set; }
        //startDate (30/9), endDate(30/10), daysInWeek(T2, T5) Phải sync ngày và thứ tạo (30/9 là T2)
        [Required(ErrorMessage = "Class start date is required")]
        [DisplayName("Class start date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Class end date is required")]
        [DateGreaterThan("StartDate", Constants.CLASS_MINIMUM_DAYS_REQ)]
        [DisplayName("Class end date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Enrollment Fee is required")]
        [Range(Constants.CLASS_MINIMUM_ENROLLMENT_FEE, Constants.CLASS_MAXIMUM_ENROLLMENT_FEE)]
        [DisplayName("Enrollment Fee")]
        public int EnrollmentFee { get; set; } = 1000000;
        [Required(ErrorMessage = "Assign teacher name is required")]
        [DisplayName("Assign teacher name")]
        public string TeacherName { get; set; } = null!;

    }
}
