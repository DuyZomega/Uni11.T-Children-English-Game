using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class HomeworkAnswerViewModel
    {
        public string? Answer { get; set; }

        public HomeworkQuestionViewModel HomeworkQuestion { get; set; } = null!;
    }
}
