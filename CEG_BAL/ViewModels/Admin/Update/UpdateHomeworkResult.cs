using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Update
{
    public class UpdateHomeworkResult
    {
        public int? TotalPoint { get; set; }

        public int? TotalCorrectAnswers { get; set; }

        public TimeOnly? Playtime { get; set; }
    }
}
