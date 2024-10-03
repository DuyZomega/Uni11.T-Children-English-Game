using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using CEG_DAL.Repositories.Interfaces;
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

        public Task<List<HomeworkAnswer>?> GetAnswerListByQuestionId(int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<HomeworkAnswer>> GetAnswersList()
        {
            throw new NotImplementedException();
        }

        public Task<HomeworkAnswer?> GetByAnswer(string answer)
        {
            throw new NotImplementedException();
        }

        public Task<HomeworkAnswer?> GetByIdNoTracking(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetIdByAnswer(string answer)
        {
            throw new NotImplementedException();
        }
    }
}
