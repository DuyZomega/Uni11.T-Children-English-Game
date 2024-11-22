using CEG_BAL.Attributes;
using CEG_RazorWebApp.Libraries;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Transaction.Create
{
    public class CreateTransactionVM
    {
        [Required(ErrorMessage = "Parent's Fullname is required")]
        [DisplayName("Parent's Fullname")]
        public string? ParentFullname { get; set; }
        //[Required(ErrorMessage = "Student's Fullname is required")]
        //[DisplayName("Student's Fullname")]
        public string? StudentFullname { get; set; }
        //[Required(ErrorMessage = "Class's Fullname is required")]
        //[DisplayName("Class's Name")]
        public string? Classname { get; set; }
        [Required(ErrorMessage = "Transaction Amount is required")]
        [Range(Constants.TRANSACTION_MINIMUM_AMOUNT, int.MaxValue)]
        [DisplayName("Amount")]
        public int TransactionAmount { get; set; } = Constants.TRANSACTION_MINIMUM_AMOUNT;
        //[Required(ErrorMessage = "Transaction Date is required")]
        //[DisplayName("Transaction Date")]
        //[DataType(DataType.DateTime)]
        //public DateTime TransactionDate { get; set; } = DateTime.Now;
        //[Required(ErrorMessage = "Transaction Status is required")]
        //[DisplayName("Status")]
        //public string TransactionStatus { get; set; } = null!;
        [Required(ErrorMessage = "Transaction Type is required")]
        [DisplayName("Type")]
        public string TransactionType { get; set; } = null!;
        //[Required(ErrorMessage = "Transaction Confirm Date is required")]
        //[DateGreaterThan("TransactionDate", Constants.TRANSACTION_CONFIRM_DATE_DIFF)]
        //[DisplayName("Confirm Date")]
        //[DataType(DataType.DateTime)]
        //public DateTime ConfirmDate { get; set; } = new DateTime();
    }
}
