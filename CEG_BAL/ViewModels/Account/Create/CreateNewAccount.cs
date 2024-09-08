using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Account.Create
{
    public class CreateNewAccount
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; }

        public string Fullname { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string Gender { get; set; } = null!;

        public string Status { get; set; } = null!;

        public virtual RoleViewModel Role { get; set; } = null!;
    }
}
