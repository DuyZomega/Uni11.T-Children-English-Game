using CEG_WebMVC.Libraries;
using CEG_WebMVC.Models.ViewModels.Session.Create;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CEG_WebMVC.Models.ViewModels.Course.Create
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
            Status = Constants.COURSE_STATUS_DRAFT;
            RequiredAge = Constants.COURSE_AGE_REQ;
            TotalHours = Constants.COURSE_TOTAL_HOURS;

        }
        public string CourseName { get; set; } = null!;

        public string CourseType { get; set; } = null!;
        public int? TotalHours { get; set; }
        public string? CourseImageHeader { get; set; }
        public int? RequiredAge { get; set; }
        public string? Difficulty { get; set; }
        public string? Category { get; set; }

        public string Description { get; set; } = null!;

        public string? Status { get; set; }
        public List<SelectListItem> DefaultCourseDifficultySelectList { get; set; }
        public List<SelectListItem> DefaultCourseCategorySelectList { get; set; }
        public List<CreateSessionVM> Sessions { get; set; } = new List<CreateSessionVM>();
    }
}
