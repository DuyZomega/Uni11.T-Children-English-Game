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
    public class HomeworkAnswerRepositories : RepositoryBase<HomeworkAnswer>, IHomeworkAnswerRepositories
    {
        private readonly MyDBContext _dbContext;

        public HomeworkAnswerRepositories(MyDBContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<HomeworkAnswer>?> GetListByQuestionId(int questionId)
        {
            return await _dbContext.HomeworkAnswers
                .AsNoTrackingWithIdentityResolution()
                .Where(answ => answ.HomeworkQuestionId == questionId)
                .ToListAsync();
        }
        public async Task<List<HomeworkAnswer>?> GetListBySessionId(int sessionId)
        {
            return await _dbContext.HomeworkAnswers
                .AsNoTrackingWithIdentityResolution()
                .Where(
                    answ => answ.HomeworkQuestion.Homework != null && 
                            answ.HomeworkQuestion.Homework.SessionId == sessionId
                )
                .ToListAsync();
        }

        public async Task<List<HomeworkAnswer>?> GetListByCourseId(int courseId)
        {
            return await _dbContext.HomeworkAnswers
                .AsNoTrackingWithIdentityResolution()
                .Where(
                    answ => answ.HomeworkQuestion.Homework != null && 
                            answ.HomeworkQuestion.Homework.Session != null &&
                            answ.HomeworkQuestion.Homework.Session.CourseId == courseId
                )
                .ToListAsync();
        }

        public async Task<List<HomeworkAnswer>> GetList()
        {
            return await _dbContext.HomeworkAnswers.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task<HomeworkAnswer?> GetByAnswer(string answer)
        {
            return await _dbContext.HomeworkAnswers.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(answ => answ.Answer == answer);
        }

        public async Task<HomeworkAnswer?> GetByIdNoTracking(int id)
        {
            return await _dbContext.HomeworkAnswers.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(answ => answ.HomeworkAnswerId == id);
        }

        public async Task<int> GetIdByAnswer(string answer)
        {
            var result = await(from s in _dbContext.HomeworkAnswers where s.Answer == answer select s).FirstOrDefaultAsync();
            if (result != null) return result.HomeworkAnswerId;
            return 0;
        }
    }
}
