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
    public class HomeworkQuestionRepositories : RepositoryBase<HomeworkQuestion>, IHomeworkQuestionRepositories
    {
        private readonly MyDBContext _dbContext;

        public HomeworkQuestionRepositories(MyDBContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<HomeworkQuestion?> GetByIdNoTracking(int id)
        {
            return await _dbContext.HomeworkQuestions.Include(h => h.HomeworkAnswers).AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(ques => ques.HomeworkQuestionId == id);
        }

        public async Task<HomeworkQuestion?> GetByQuestion(string question)
        {
            return await _dbContext.HomeworkQuestions.Include(s => s.HomeworkAnswers).AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(ques => ques.Question == question);
        }

        public async Task<int> GetIdByQuestion(string question)
        {
            var result = await(from s in _dbContext.HomeworkQuestions where s.Question == question select s).FirstOrDefaultAsync();
            if (result != null) return result.HomeworkId;
            return 0;
        }

        public async Task<List<HomeworkQuestion>?> GetQuestionListByHomeworkId(int homeworkId)
        {
            return await _dbContext.HomeworkQuestions.AsNoTrackingWithIdentityResolution().Where(ques => ques.HomeworkId == homeworkId).ToListAsync();
        }

        public async Task<List<HomeworkQuestion>> GetQuestionsList()
        {
            return await _dbContext.HomeworkQuestions.ToListAsync();
        }
    }
}
