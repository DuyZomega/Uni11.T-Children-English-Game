using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Create
{
    public class CreateNewHomeworkResult
    {
        public int? TotalPoint { get; set; }

        public int? TotalCorrectAnswers { get; set; }

        public TimeSpan Playtime { get; set; }
    }
}
