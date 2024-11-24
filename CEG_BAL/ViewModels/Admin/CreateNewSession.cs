using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin
{
    public class CreateNewSession
    {
        [Required(ErrorMessage = "Session Number is required")]
        [Range(0, 100)]
        public int? Number { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Hours to learn is required")]
        [Range(0, int.MaxValue)]
        public int Hours { get; set; }
        [Required(ErrorMessage = "Course Id is required")]
        public int? CourseId { get; set;}
    }
}
