﻿using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using CEG_DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Implements
{
    public class StudentRepositories : RepositoryBase<Student>, IStudentRepositories
    {
        private readonly MyDBContext _dbContext;
        public StudentRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Student>> GetStudentList()
        {
            return await _dbContext.Students.ToListAsync();
        }
        public async Task<Student?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Students.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(stu => stu.StudentId == id);
        }
        public async Task<List<Student>> GetStudentByParentId(int parentId)
        {
            return await _dbContext.Students.Where(stu => stu.ParentId == parentId).ToListAsync();
        }
    }
}
