using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Create
{
    public class CreateNewStudentAnswer
    {
        public int GameId { get; set; }
        public int StudentHomeworkId { get; set; }
        public string? Answer { get; set; }

        public string? Type { get; set; }
    }
}
