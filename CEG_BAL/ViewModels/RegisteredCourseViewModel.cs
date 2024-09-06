using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class RegisteredCourseViewModel
    {
        public bool? PaymentStatus { get; set; }

        public DateTime? RegisteredDate { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public virtual Course Course { get; set; } = null!;

        public virtual Payment Payment { get; set; } = null!;

        public virtual ICollection<StudentProcess> StudentProcesses { get; set; } = new List<StudentProcess>();
    }
}
