using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class RegisteredClassViewModel
    {
        public bool? PaymentStatus { get; set; }

        public DateTime? RegisteredDate { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public virtual CourseViewModel Course { get; set; } = null!;

        public virtual PaymentViewModel Payment { get; set; } = null!;

        public virtual ICollection<StudentProgressViewModel> StudentProcesses { get; set; } = new List<StudentProgressViewModel>();
    }
}
