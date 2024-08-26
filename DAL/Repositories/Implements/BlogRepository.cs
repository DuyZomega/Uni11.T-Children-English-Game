﻿using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        private readonly BirdClubContext _context;
        public BlogRepository(BirdClubContext context) : base(context) 
        {
            _context = context;
        }
    }
}
