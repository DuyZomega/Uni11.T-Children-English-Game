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
            DefaultCourseDifficultySelectList = lib.GetCourseDifficultySelectableList(Difficulty);
            DefaultCourseCategorySelectList = lib.GetCourseCategorySelectableList(Category);

        }
        [Required(ErrorMessage = "Course Name is required")]
        [DisplayName("Course Name")]
        public string CourseName { get; set; } = null!;
        [Required(ErrorMessage = "Course Type is required")]
        [DisplayName("Course Type")]
        public string CourseType { get; set; } = null!;
        [Required(ErrorMessage = "Total Hours is required")]
        [Range(Constants.COURSE_TOTAL_HOURS, int.MaxValue)]
        [DisplayName("Total Hours")]
        public int? TotalHours { get; set; } = Constants.COURSE_TOTAL_HOURS;
        public string? CourseImageHeader { get; set; }
        [Required(ErrorMessage = "Required Age is required")]
        [Range(Constants.COURSE_MINIMUM_AGE_REQ, Constants.COURSE_MAXIMUM_AGE_REQ)]
        [DisplayName("Age Require")]
        public int? RequiredAge { get; set; } = Constants.COURSE_MINIMUM_AGE_REQ;
        [Required(ErrorMessage = "Course Difficulty is required")]
        [DisplayName("Difficulty")]
        public string? Difficulty { get; set; } = Constants.COURSE_DIFFICULTY_BEGINNER;
        [Required(ErrorMessage = "Course Category is required")]
        [DisplayName("Category")]
        public string? Category { get; set; } = Constants.COURSE_CATEGORY_MIDDLE_SCHOOL;
        [Required(ErrorMessage = "Course Description is required")]
        [MinLength(1)]
        [DisplayName("Description")]
        public string Description { get; set; } = null!;
        public List<SelectListItem> DefaultCourseDifficultySelectList { get; set; }
        public List<SelectListItem> DefaultCourseCategorySelectList { get; set; }
    }
}
