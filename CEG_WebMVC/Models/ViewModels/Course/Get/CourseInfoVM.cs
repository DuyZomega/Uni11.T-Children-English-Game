﻿using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.ViewModels.Course.Get
{
    public class CourseInfoVM
    {
        public string CourseName { get; set; } = null!;

        public string CourseType { get; set; } = null!;

        public string Description { get; set; } = null!;
        public int? SessionAmount { get; set; }
        public int? ClassAmount { get; set; }

        public string? Status { get; set; }

        public List<SessionViewModel> Sessions { get; set; } = new List<SessionViewModel>();
    }
}
