using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;

namespace CEG_BAL.Services.Implements
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public ClassService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTService jwtServices,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtServices;
            _configuration = configuration;
        }

        public void Create(ClassViewModel classModel, CreateNewClass newClass)
        {
            var clas = _mapper.Map<Class>(classModel);
            if (newClass != null)
            {
                clas.ClassName = newClass.ClassName;
                clas.TeacherId = _unitOfWork.TeacherRepositories.GetByFullname(newClass.TeacherName).Result.TeacherId;
                clas.CourseId = _unitOfWork.CourseRepositories.GetIdByName(newClass.CourseName).Result;
                clas.StartDate = newClass.StartDate;
                clas.EndDate = newClass.EndDate;
                clas.MinimumStudents = newClass.MinStudents;
                clas.MaximumStudents = newClass.MaxStudents;
                clas.EnrollmentFee = newClass.EnrollmentFee;
                clas.Status = "Draft";
                clas.Schedules = _mapper.Map<List<Schedule>>(newClass.Schedules);
                foreach(var schedule in clas.Schedules)
                {
                    schedule.Status = "Draft";
                    schedule.StartTime = schedule.ScheduleDate.HasValue ? TimeOnly.FromDateTime(schedule.ScheduleDate.Value) : default;
                    schedule.EndTime = schedule.StartTime.Value.AddHours(_unitOfWork.SessionRepositories.GetByIdNoTracking(schedule.SessionId).Result.Hours.Value);
                }
                /*if (newClass.WeeklySchedule != null)
                {
                    var sessionList = _unitOfWork.SessionRepositories.GetSessionListByCourseId(clas.CourseId).Result;
                    if (sessionList.Count > 0 && clas.StartDate.HasValue)
                    {
                        // Extract logic to handle different schedules into a helper function
                        // AssignSchedulesBasedOnDays(newClass.WeeklySchedule, clas.StartDate.Value.DayOfWeek, clas, sessionList, newClass.StartDate, sessionList.Select(s => s.Hours).ToList());
                    }
                }*/
            }
            _unitOfWork.ClassRepositories.Create(clas);
            _unitOfWork.Save();
        }

        public async Task<ClassViewModel?> GetClassById(int id)
        {
            var clas = await _unitOfWork.ClassRepositories.GetByIdNoTracking(id, true, true);
            if (clas != null)
            {
                var c = _mapper.Map<ClassViewModel>(clas);
                return c;
            }
            return null;
        }

        public async Task<ClassViewModel?> GetByIdAdmin(int id)
        {
            var clas = await _unitOfWork.ClassRepositories.GetByIdNoTracking(id, true, true, true, true);
            if (clas != null)
            {
                var c = _mapper.Map<ClassViewModel>(clas);
                return c;
            }
            return null;
        }

        public async Task<ClassViewModel?> GetByIdParent(int id)
        {
            var clas = await _unitOfWork.ClassRepositories.GetByIdNoTracking(id, true, true, true, true);
            if (clas != null)
            {
                var c = _mapper.Map<ClassViewModel>(clas);
                return c;
            }
            return null;
        }

        public async Task<List<ClassViewModel>> GetClassList()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassList());
        }

        public async Task<List<GetClassForTransaction>> GetClassOptionListByStatusOpen()
        {
            return _mapper.Map<List<GetClassForTransaction>>(await _unitOfWork.ClassRepositories.GetClassOptionListByStatusOpen());
        }

        public async Task<List<ClassViewModel>> GetListAdmin()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassListAdmin());
        }

        public async Task<List<ClassViewModel>> GetClassListParent()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassListParent());
        }

        public async Task<List<ClassViewModel>> GetClassListByTeacherAccountId(int id)
        {
            var teacherId = await _unitOfWork.TeacherRepositories.GetIdByAccountId(id);
            if (teacherId == 0) return null;
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassListByTeacherId(teacherId));
        }

        public void Update(ClassViewModel classModel)
        {
            var clas = _mapper.Map<Class>(classModel);
            _unitOfWork.ClassRepositories.Update(clas);
            _unitOfWork.Save();
        }
        public void Update(ClassViewModel classModel, UpdateClass classNewModel)
        {
            var mainClass = _mapper.Map<Class>(classModel);
            if (classNewModel != null)
            {
                mainClass.TeacherId = _unitOfWork.TeacherRepositories.GetByFullname(classNewModel.TeacherName).Result.TeacherId;
                mainClass.ClassName = classNewModel.ClassName;
                mainClass.MinimumStudents = classNewModel.MinimumStudents;
                mainClass.MaximumStudents = classNewModel.MaximumStudents;
                mainClass.StartDate = classNewModel.StartDate;
                mainClass.EndDate = classNewModel.EndDate;
                mainClass.EnrollmentFee = classNewModel.EnrollmentFee;
            }
            mainClass.CourseId = mainClass.Course.CourseId;
            mainClass.Schedules = null;
            mainClass.Course = null;
            mainClass.Teacher = null;
            _unitOfWork.ClassRepositories.Update(mainClass);
            _unitOfWork.Save();
        }

        public void UpdateStatus(int classId, string classStatus)
        {
            var clas = _unitOfWork.ClassRepositories.GetByIdNoTracking(classId).Result;
            if (clas == null) return;
            foreach(var sche in clas.Schedules){
                var schedule = _unitOfWork.ScheduleRepositories.GetByIdNoTracking(sche.ScheduleId).Result;
                schedule.Status = Constants.SCHEDULE_STATUS_UPCOMING;
                _unitOfWork.ScheduleRepositories.Update(schedule);
            }
            clas.Schedules = null;
            clas.Status = classStatus;
            _unitOfWork.ClassRepositories.Update(clas);
            _unitOfWork.Save();
        }

        public async Task<bool> IsClassEditableById(int id)
        {
            var clas = await _unitOfWork.ClassRepositories.GetByIdNoTracking(id);
            if (clas != null && (clas.Status.Equals(Constants.CLASS_STATUS_DRAFT) || clas.Status.Equals(Constants.CLASS_STATUS_POSTPONED))) return true;
            return false;
        }

        // Helper Function to handle schedule assignment
        /*private void AssignSchedulesBasedOnDays(string scheduleType, DayOfWeek startDay, Class clas, List<Session> sessionList, DateTime startDate, List<int?> sessionHours)
        {
            // Define possible day pairs for each schedule type
            var dayPairs = new Dictionary<string, (DayOfWeek, DayOfWeek)>
            {
                { Configurations.Constants.CLASS_SCHEDULE_MONDAY_THURSDAY, (DayOfWeek.Monday, DayOfWeek.Thursday) },
                { Configurations.Constants.CLASS_SCHEDULE_TUESDAY_FRIDAY, (DayOfWeek.Tuesday, DayOfWeek.Friday) },
                { Configurations.Constants.CLASS_SCHEDULE_WEDNESDAY_SATURDAY, (DayOfWeek.Wednesday, DayOfWeek.Saturday) }
            };

            if (!dayPairs.TryGetValue(scheduleType, out (DayOfWeek, DayOfWeek) value))
                return; // Invalid schedule type

            var (firstDay, secondDay) = value;

            // Only proceed if the start day matches one of the schedule days
            if (startDay == firstDay || startDay == secondDay)
            {
                TimeOnly startTime = TimeOnly.FromDateTime(startDate);

                for (int i = 0; i < sessionList.Count; i++)
                {
                    int? sessionDuration = sessionHours[i];
                    if (sessionDuration.HasValue)
                    {
                        clas.Schedules.Add(new Schedule()
                        {
                            SessionId = sessionList[i].SessionId,
                            DayOfWeek = i % 2 == 0 ? firstDay.ToString() : secondDay.ToString(),
                            StartTime = startTime,
                            EndTime = startTime.AddHours(sessionDuration.Value),
                            Status = "Draft"
                        });
                    }
                }
            }
        }*/
    }
}
