using CEG_DAL.Models;
using CEG_DAL.Repositories.Implements;
using CEG_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBContext _dbContext;
        private IAccountRepositories _accountRepositories;
        private IClassRepositories _classRepositories;
        private ICourseRepositories _courseRepositories;
        private IEnrollRepositories _enrollRepositories;
        private IGameConfigRepositories _gameConfigRepositories;
        private IGameLevelRepositories _gameLevelRepositories;
        private IGameRepositories _gameRepositories;
        private IHomeworkRepositories _homeworkRepositories;
        private IHomeworkResultRepositories _homeworkResultRepositories;
        private IParentRepositories _parentRepositories;
        private IPaymentRepositories _paymentRepositories;
        private IRegisteredClassRepositories _registeredCourseRepositories;
        private IRoleRepositories _roleRepositories;
        private ISessionRepositories _sessionRepositories;
        private IStudentHomeworkRepositories _studentHomeworkRepositories;
        private IStudentProcessRepositories _studentProcessRepositories;
        private IStudentRepositories _studentRepositories;
        private ITeacherRepositories _teacherRepositories;
        public UnitOfWork(MyDBContext context)
        {
            _dbContext = context;
        }
        public IAccountRepositories AccountRepositories => _accountRepositories ??= new AccountRepositories(_dbContext);
        public IClassRepositories ClassRepositories => _classRepositories ??= new ClassRepositories(_dbContext);
        public ICourseRepositories CourseRepositories => _courseRepositories ??= new CourseRepositories(_dbContext);
        public IEnrollRepositories EnrollRepositories => _enrollRepositories ??= new EnrollRepositories(_dbContext);
        public IGameConfigRepositories GameConfigRepositories => _gameConfigRepositories ??= new GameConfigRepositories(_dbContext);
        public IGameLevelRepositories GameLevelRepositories => _gameLevelRepositories ??= new GameLevelRepositories(_dbContext);
        public IGameRepositories GameRepositories => _gameRepositories ??= new GameRepositories(_dbContext);
        public IHomeworkRepositories HomeworkRepositories => _homeworkRepositories ??= new HomeworkRepositories(_dbContext);
        public IHomeworkResultRepositories HomeworkResultRepositories => _homeworkResultRepositories ??= new HomeworkResultRepositories(_dbContext);
        public IParentRepositories ParentRepositories => _parentRepositories ??= new ParentRepositories(_dbContext);
        public IPaymentRepositories PaymentRepositories => _paymentRepositories ??= new PaymentRepositories(_dbContext);
        public IRegisteredClassRepositories RegisteredCourseRepositories => _registeredCourseRepositories ??= new RegisteredClassRepositories(_dbContext);
        public IRoleRepositories RoleRepositories => _roleRepositories ??= new RoleRepositories(_dbContext);
        public ISessionRepositories SessionRepositories => _sessionRepositories ??= new SessionRepositories(_dbContext);
        public IStudentHomeworkRepositories StudentHomeworkRepositories => _studentHomeworkRepositories ??= new StudentHomeworkRepositories(_dbContext);
        public IStudentProcessRepositories StudentProcessRepositories => _studentProcessRepositories ??= new StudentProcessRepositories(_dbContext);
        public IStudentRepositories StudentRepositories => _studentRepositories ??= new StudentRepositories(_dbContext);
        public ITeacherRepositories TeacherRepositories => _teacherRepositories ??= new TeacherRepositories(_dbContext);
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
