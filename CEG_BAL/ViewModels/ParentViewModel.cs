﻿using System.ComponentModel.DataAnnotations;

namespace CEG_BAL.ViewModels
{
    public class ParentViewModel
    {
        public ParentViewModel()
        {
            Account = new AccountViewModel();
        }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public AccountViewModel Account { get; set; } = null!;

        public virtual ICollection<TransactionViewModel> Payments { get; set; } = new List<TransactionViewModel>();

        public virtual ICollection<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();
    }
}
