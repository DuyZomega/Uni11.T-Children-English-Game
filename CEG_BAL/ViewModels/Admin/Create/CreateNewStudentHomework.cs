using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Create
{
    public class CreateNewStudentHomework
    {
        [Range(1, int.MaxValue)]
        public int HomeworkId { get; set; }
        [Range(1, int.MaxValue)]
        public int StudentProgressId { get; set; }
        [Range(1, int.MaxValue)]
        public int HomeworkResultId { get; set; }

        public int Point { get; set; }

        public TimeOnly? Playtime { get; set; }

        public string? Status { get; set; }

        public int? CorrectAnswers { get; set; }
    }
}
