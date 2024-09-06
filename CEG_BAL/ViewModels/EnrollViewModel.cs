using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class EnrollViewModel
    {

        public DateTime EnrolledDate { get; set; }

        public virtual Student Student { get; set; } = null!;
    }
}
