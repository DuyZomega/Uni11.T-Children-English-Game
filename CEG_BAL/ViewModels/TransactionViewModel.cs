using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        public string VnpayId { get; set; }

        public int TransactionAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public bool TransactionStatus { get; set; }

        public string TransactionType { get; set; } = null!;

        public DateTime ConfirmDate { get; set; }

        public virtual ICollection<EnrollViewModel> Enrolls { get; set; } = new List<EnrollViewModel>();

        public virtual ParentViewModel Parents { get; set; } = null!;
    }
}
