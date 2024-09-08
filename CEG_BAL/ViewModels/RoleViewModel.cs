using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class RoleViewModel
    {
        public string RoleName { get; set; } = null!;

        public virtual ICollection<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();
    }
}
