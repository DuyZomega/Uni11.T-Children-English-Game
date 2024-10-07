using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CEG_RazorWebApp.Models.Account.Get
{
    public class AccountInfoVM
    {
        public int? AccountId { get; set; }
        public string Username { get; set; } = null!;
        //[PasswordPropertyText]
        //[DataType(DataType.Password)]
        //public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string Gender { get; set; } = null!;

        public string? Role { get; set; }

        public string Status { get; set; } = null!;
    }
}
