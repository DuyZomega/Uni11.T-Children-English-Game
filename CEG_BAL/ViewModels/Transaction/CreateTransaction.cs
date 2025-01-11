using CEG_BAL.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Transaction
{
    public class CreateTransaction
    {
        [Required(ErrorMessage = "Parent's Fullname is required")]
        [DisplayName("Parent's Fullname")]
        public string ParentFullname { get; set; } = null!;
        public string VnpayId { get; set; } = "Paid in cashes";
        [Required(ErrorMessage = "Transaction Amount is required")]
        [Range(CEGConstants.TRANSACTION_MINIMUM_AMOUNT, int.MaxValue)]
        [DisplayName("Amount")]
        public int TransactionAmount { get; set; }
        [Required(ErrorMessage = "Transaction Type is required")]
        [DisplayName("Type")]
        public string TransactionType { get; set; } = null!;
        [Required(ErrorMessage = "Student's Fullname is required")]
        [DisplayName("Student's Fullname")]
        public string StudentFullname { get; set; } = null!;
        [Required(ErrorMessage = "Class code is required")]
        [DisplayName("Class code")]
        public string ClassName { get; set; } = null!;
    }
}
