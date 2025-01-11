using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Update
{
    public class UpdateStudentHomework
    {
        public int HomeworkId { get; set; }

        public int StudentProgressId { get; set; }

        public int HomeworkResultId { get; set; }

        public int Point { get; set; }

        public TimeOnly? Playtime { get; set; }

        public string? Status { get; set; }

        public int? CorrectAnswers { get; set; }
    }
}
