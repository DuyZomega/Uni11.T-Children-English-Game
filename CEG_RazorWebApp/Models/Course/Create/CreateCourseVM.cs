using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Course.Create
{
    public class CreateCourseVM
    {
        public CreateCourseVM()
        {
            ChildrenEnglishGameLibrary lib = new ChildrenEnglishGameLibrary();
            Category = Constants.COURSE_CATEGORY_MIDDLE_SCHOOL;
            Difficulty = Constants.COURSE_DIFFICULTY_BEGINNER;
            DefaultCourseDifficultySelectList = lib.GetCourseDifficultySelectableList(Difficulty);
            DefaultCourseCategorySelectList = lib.GetCourseCategorySelectableList(Category);
            RequiredAge = Constants.COURSE_MINIMUM_AGE_REQ;
            TotalHours = Constants.COURSE_TOTAL_HOURS;

        }
        [Required(ErrorMessage = "Course Name is required")]
        [DisplayName("Course Name")]
        public string CourseName { get; set; } = null!;
        [Required(ErrorMessage = "Course Type is required")]
        [DisplayName("Course Type")]
        public string CourseType { get; set; } = null!;
        [Required(ErrorMessage = "Total Hours is required")]
        [Range(1, int.MaxValue)]
        [DisplayName("Total Hours")]
        public int? TotalHours { get; set; }
        public string? CourseImageHeader { get; set; }
        [Required(ErrorMessage = "Required Age is required")]
        [Range(11, 18)]
        [DisplayName("Age Require")]
        public int? RequiredAge { get; set; }
        [Required(ErrorMessage = "Course Difficulty is required")]
        [DisplayName("Difficulty")]
        public string? Difficulty { get; set; }
        [Required(ErrorMessage = "Course Category is required")]
        [DisplayName("Category")]
        public string? Category { get; set; }
        [Required(ErrorMessage = "Course Description is required")]
        [MinLength(1)]
        [DisplayName("Description")]
        public string Description { get; set; } = null!;
        public List<SelectListItem> DefaultCourseDifficultySelectList { get; set; }
        public List<SelectListItem> DefaultCourseCategorySelectList { get; set; }
    }
}
