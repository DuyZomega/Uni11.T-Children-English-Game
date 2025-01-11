using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class EnrollViewModel
    {
        public int? EnrollId { get; set; }

        public int? ClassId { get; set; }

        public int? StudentId { get; set; }

        //public string? ClassName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime EnrolledDate { get; set; }

        public string? Status { get; set; }
        public ClassViewModel Class { get; set; } = null!;

        public StudentViewModel Student { get; set; } = null!;

        public TransactionViewModel Transaction { get; set; } = null!;
    }
}
