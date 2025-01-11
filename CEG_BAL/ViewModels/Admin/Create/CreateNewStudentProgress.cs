using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Create
{
    public class CreateNewStudentProgress
    {
        public int StudentId { get; set; }

        public int? TotalPoint { get; set; }

        public TimeOnly? Playtime { get; set; }
    }
}
