﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin
{
    public class CreateNewSession
    {
        public int? Number { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Hours { get; set; }
        public string? CourseName { get; set; }
    }
}
