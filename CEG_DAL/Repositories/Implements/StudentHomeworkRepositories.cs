using CEG_DAL.Infrastructure;
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
    public class StudentHomeworkRepositories : RepositoryBase<StudentHomework>,IStudentHomeworkRepositories
    {
        private readonly MyDBContext _dbContext;
        public StudentHomeworkRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentHomework?> GetByIdNoTracking(int id)
        {
            return await _dbContext.StudentHomeworks
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(home => home.StudentHomeworkId == id);
        }

        public async Task<List<StudentHomework>> GetList()
        {
            return await _dbContext.StudentHomeworks
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<List<StudentHomework>> GetListByStudentId(int? id)
        {
            return await _dbContext.StudentHomeworks
                .AsNoTrackingWithIdentityResolution()
                .Where(s => s.StudentProgress.StudentId == id)
                .Select(stuHom => new StudentHomework()
                {
                    StudentHomeworkId = stuHom.StudentHomeworkId,
                    StudentProgressId = stuHom.StudentProgressId,
                    HomeworkResultId = stuHom.HomeworkResultId,
                    HomeworkId = stuHom.HomeworkId,
                    Point = stuHom.Point,
                    Playtime = stuHom.Playtime,
                    CorrectAnswers = stuHom.CorrectAnswers,
                    Status = stuHom.Status,
                    StudentProgress = new StudentProgress()
                    {
                        StudentProgressId = stuHom.StudentProgress.StudentProgressId,
                        //StudentId = (int)id,
                        TotalPoint = stuHom.StudentProgress.TotalPoint,
                        Playtime = stuHom.StudentProgress.Playtime,
                        Class = new Class()
                        {
                            ClassName = stuHom.StudentProgress.Class.ClassName
                        }
                    },
                    Homework = new Homework()
                    {
                        Title = stuHom.Homework.Title,
                        Session = new Session()
                        {
                            Title = stuHom.Homework.Session.Title,
                        }
                    }
                })
                .ToListAsync();
        }
    }
}
