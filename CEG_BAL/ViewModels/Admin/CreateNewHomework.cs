using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin
{
    public class CreateNewHomework
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Hours { get; set; }
        public string SessionTitle { get; set; }
    }
}
