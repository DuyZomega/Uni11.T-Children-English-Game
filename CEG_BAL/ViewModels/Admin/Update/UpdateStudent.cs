using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Update
{
    public class UpdateStudent
    {
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Birthdate is required")]
        [DisplayName("Birthdate")]
        [DataType(DataType.DateTime)]
        public DateTime? Birthdate { get; set; }
        [Required(ErrorMessage = "Parent's Fullname is required")]
        [DisplayName("Parent's Fullname")]
        public string? ParentFullname { get; set; }
        public string? Image { get; set; }
        public UpdateAccount Account { get; set; } = new UpdateAccount();
    }
}
