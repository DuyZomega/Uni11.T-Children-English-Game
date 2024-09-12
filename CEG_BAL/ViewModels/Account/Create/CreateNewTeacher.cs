using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Account.Create
{
    public class CreateNewTeacher
    {
        public CreateNewTeacher()
        {
            Account = new CreateNewAccount
            {
                Role = "Teacher"
            };
        }
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public virtual CreateNewAccount Account { get; set; }
    }
}
