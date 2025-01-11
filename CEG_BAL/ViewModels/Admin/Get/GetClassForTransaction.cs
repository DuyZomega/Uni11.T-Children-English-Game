using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Get
{
    public class GetClassForTransaction
    {
        public string ClassName { get; set; } = null!;
        public int EnrollmentFee { get; set; }
    }
}
