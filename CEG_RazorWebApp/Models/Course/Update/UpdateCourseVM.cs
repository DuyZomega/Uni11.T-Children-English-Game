using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CEG_RazorWebApp.Models.Course.Update
{
    public class UpdateCourseVM
    {
        public UpdateCourseVM()
        {
            var lib = new ChildrenEnglishGameLibrary();
            Category = Constants.COURSE_CATEGORY_MIDDLE_SCHOOL;
            Difficulty = Constants.COURSE_DIFFICULTY_BEGINNER;
            Status = Constants.COURSE_STATUS_DRAFT;
            DefaultCourseDifficultySelectList = lib.GetCourseDifficultySelectableList(Difficulty);
            DefaultCourseCategorySelectList = lib.GetCourseCategorySelectableList(Category);
            DefaultCourseStatusSelectList = lib.GetCourseStatusSelectableList(Status);
            RequiredAge = Constants.COURSE_MINIMUM_AGE_REQ;
            TotalHours = Constants.COURSE_TOTAL_HOURS;

        }
        [Required(ErrorMessage = "Course Name is required")]
        [DisplayName("Course Name")]
        public string? CourseName { get; set; }
        [Required(ErrorMessage = "Course Type is required")]
        [DisplayName("Course Type")]
        public string? CourseType { get; set; }
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
        public string? Description { get; set; }
        [Required(ErrorMessage = "Course Status is required")]
        [DisplayName("Status")]
        public string? Status { get; set; }
        public List<SelectListItem> DefaultCourseDifficultySelectList { get; set; }
        public List<SelectListItem> DefaultCourseCategorySelectList { get; set; }
        public List<SelectListItem> DefaultCourseStatusSelectList { get; set; }
    }
}
