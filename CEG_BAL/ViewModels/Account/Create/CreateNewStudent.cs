using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Account.Create
{
    public class CreateNewStudent
    {
        public string Description { get; set; }
        public int Highscore { get; set; }
        public DateTime Birthdate { get; set; }
        public string ParentUsername { get; set; }
        public virtual CreateNewAccount Account { get; set; }
    }
}
