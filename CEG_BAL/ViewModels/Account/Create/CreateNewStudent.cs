using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Account.Create
{
    public class CreateNewStudent
    {
        public CreateNewStudent()
        {
            TotalPoints = 0;
            Account.Role = "Student";
        }
        public string Description { get; set; } = null!;
        public int TotalPoints { get; set; }
        public DateTime Birthdate { get; set; }
        public string ParentFullname { get; set; } = null!;
        public CreateNewAccount Account { get; set; } = new CreateNewAccount();
    }
}
