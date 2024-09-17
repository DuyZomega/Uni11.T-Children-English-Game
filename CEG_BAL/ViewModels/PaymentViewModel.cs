using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class PaymentViewModel
    {
        public DateTime PaymentDate { get; set; }

        public bool PaymentStatus { get; set; }

        public string PaymentType { get; set; } = null!;

        public DateTime ConfirmDate { get; set; }

        public virtual Parent Parents { get; set; } = null!;

        public virtual ICollection<RegisteredClass> RegisteredCourses { get; set; } = new List<RegisteredClass>();
    }
}
