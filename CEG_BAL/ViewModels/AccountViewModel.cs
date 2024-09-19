using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class AccountViewModel
    {
        public string Username { get; set; } = null!;
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string Gender { get; set; } = null!;

        public string Status { get; set; } = null!;

        public virtual RoleViewModel Role { get; set; } = null!;

    }
}
