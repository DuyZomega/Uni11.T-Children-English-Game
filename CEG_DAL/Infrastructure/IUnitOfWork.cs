using CEG_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepositories AccountRepositories { get; }
        IClassRepositories ClassRepositories { get; }
        ICourseRepositories CourseRepositories { get; }
        IEnrollRepositories EnrollRepositories { get; }
        IGameConfigRepositories GameConfigRepositories { get; }
        IGameLevelRepositories GameLevelRepositories { get; }
        IGameRepositories GameRepositories { get; }
        IHomeworkRepositories HomeworkRepositories { get; }
        IHomeworkQuestionRepositories HomeworkQuestionRepositories { get; }
        IHomeworkAnswerRepositories HomeworkAnswerRepositories { get; }
        IHomeworkResultRepositories HomeworkResultRepositories { get; }
        IParentRepositories ParentRepositories { get; }
        ITransactionRepositories TransactionRepositories { get; }
        IRoleRepositories RoleRepositories { get; }
        ISessionRepositories SessionRepositories { get; }
        IStudentHomeworkRepositories StudentHomeworkRepositories { get; }
        IStudentProgressRepositories StudentProgressRepositories { get; }
        IStudentRepositories StudentRepositories { get; }
        ITeacherRepositories TeacherRepositories { get; }
        void Save();
    }
}
