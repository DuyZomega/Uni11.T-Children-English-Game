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
        IHomeworkResultRepositories HomeworkResultRepositories { get; }
        IParentRepositories ParentRepositories { get; }
        IPaymentRepositories PaymentRepositories { get; }
        IRegisteredCourseRepositories RegisteredCourseRepositories { get; }
        IRoleRepositories RoleRepositories { get; }
        ISessionRepositories SessionRepositories { get; }
        IStudentHomeworkRepositories StudentHomeworkRepositories { get; }
        IStudentProcessRepositories StudentProcessRepositories { get; }
        IStudentRepositories StudentRepositories { get; }
        ITeacherRepositories TeacherRepositories { get; }
        void Save();
    }
}
