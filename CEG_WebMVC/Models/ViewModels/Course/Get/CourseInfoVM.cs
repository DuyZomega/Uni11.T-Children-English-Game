﻿using CEG_DAL.Models;

namespace CEG_WebMVC.Models.ViewModels.Course.Get
{
    public class CourseInfoVM
    {
        public string CourseName { get; set; } = null!;

        public string CourseType { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? Status { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
