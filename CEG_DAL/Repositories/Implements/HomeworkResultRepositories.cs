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
    public class HomeworkResultRepositories : RepositoryBase<HomeworkResult>, IHomeworkResultRepositories
    {
        private readonly MyDBContext _dbContext;
        public HomeworkResultRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HomeworkResult?> GetByIdNoTracking(int id)
        {
            return await _dbContext.HomeworkResults.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(homere => homere.HomeworkResultId == id);
        }

        public async Task<HomeworkResult?> GetByStudentIdAndHomeworkIdNoTracking(int stuId, int homId)
        {
            return await _dbContext.HomeworkResults
                .AsNoTrackingWithIdentityResolution()
                .Where(homRes =>
                    homRes.StudentHomeworks.Any(stuHom =>
                        stuHom.HomeworkId == homId && stuHom.StudentProgress.StudentId == stuId
                    )
                )
                .Select(homRes => new HomeworkResult
                {
                    HomeworkResultId = homRes.HomeworkResultId,
                    Playtime = homRes.Playtime,
                    TotalCorrectAnswers = homRes.TotalCorrectAnswers,
                    TotalPoint = homRes.TotalPoint,
                    StudentHomeworks = homRes.StudentHomeworks.Select(stuHom => new StudentHomework()
                    {
                        Playtime = stuHom.Playtime,
                        HomeworkId = homId,
                        CorrectAnswers = stuHom.CorrectAnswers,
                        Point = stuHom.Point,
                        Status = stuHom.Status,
                        StudentProgress = new StudentProgress()
                        {
                            StudentId = stuId,
                            Playtime = stuHom.StudentProgress.Playtime,
                            TotalPoint = stuHom.StudentProgress.TotalPoint,
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<HomeworkResult>> GetList()
        {
            return await _dbContext.HomeworkResults.AsNoTrackingWithIdentityResolution().ToListAsync();
        }


    }
}
