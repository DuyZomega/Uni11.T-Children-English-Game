using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Update
{
    public class UpdateAttendance
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select an attendance's Status")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Attendant's Status is invalid")]
        public string HasAttended { get; set; } = null!;
    }
}
