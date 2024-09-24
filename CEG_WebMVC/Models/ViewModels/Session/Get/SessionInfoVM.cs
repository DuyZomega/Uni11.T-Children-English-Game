﻿using CEG_BAL.ViewModels;
using CEG_WebMVC.Models.ViewModels.Homework.Get;

namespace CEG_WebMVC.Models.ViewModels.Session.Get
{
    public class SessionInfoVM
    {
        public SessionInfoVM()
        {
            HomeworkAmount = Homeworks.Count;
        }
        public int? SessionId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? Number {  get; set; }
        public int? Hours { get; set; }
        public string? Status { get; set; }
        public int? HomeworkAmount { get; set; }

        public List<HomeworkInfoVM> Homeworks { get; set; } = new List<HomeworkInfoVM>();
    }
}
