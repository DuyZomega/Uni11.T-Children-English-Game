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

        public DateTime RegistrationDate { get; set; }

        public DateTime EnrolledDate { get; set; }

        public string? Status { get; set; }
        public ClassViewModel Class { get; set; }

        public StudentViewModel Student { get; set; }

        public TransactionViewModel Transaction { get; set; }
    }
}
