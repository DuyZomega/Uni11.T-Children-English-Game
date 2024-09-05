﻿using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class GameLevelViewModel
    {
        public string Title { get; set; } = null!;

        public string? Status { get; set; }

        public virtual Game Game { get; set; } = null!;
    }
}
