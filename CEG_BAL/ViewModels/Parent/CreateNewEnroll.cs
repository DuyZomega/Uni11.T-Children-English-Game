using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Parent
{
    public class CreateNewEnroll
    {
        public string ClassName { get; set; } = null!;
        public string StudentName { get; set; } = null!;
        public int TransactionId { get; set; }
    }
}
