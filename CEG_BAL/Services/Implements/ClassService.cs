using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Home;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;

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

        public async Task Create(CreateNewClass newCla)
        {
            if (newCla == null)
                throw new ArgumentNullException(nameof(newCla), "The new class info cannot be null.");

            var cla = new Class
            {
                NumberOfStudents = 0,
                Status = CEGConstants.CLASS_STATUS_DRAFT
            };
            _mapper.Map(newCla, cla);
            foreach(var schedule in cla.Schedules)
            {
                schedule.EndTime = schedule.StartTime.Value.AddHours((await _unitOfWork.SessionRepositories.GetByIdNoTracking(schedule.SessionId)).Hours.Value);
            }

            // Save to the database
            try
            {
                _unitOfWork.ClassRepositories.Create(cla);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating new class.", ex);
            }
        }
        /// <summary>
        /// Get Class Info by Class id. 
        /// int id, class id to be use to query. 
        /// bool includeTeacher = true, determine whether if the query should include teacher info. 
        /// bool includeCourse = true, determine whether if the query should include course info. 
        /// bool includeSession = false, determine whether if the query should include course's sessions info. 
        /// bool filterSession = false, determine whether if the query should include filter session infos to only contain unscheduled session. 
        /// </summary>
        /// <param name="id">Class id</param>
        /// <param name="includeTeacher">Default: false, determine whether if the query should include teacher info</param>
        /// <param name="includeCourse">Default: false, determine whether if the query should include course info</param>
        /// <param name="includeSession">Default: false, determine whether if the query should include course's sessions info</param>
        /// <param name="filterSession">Default: false, determine whether if the query should include filter session infos to only contain unscheduled session</param>
        /// <param name="includeSchedule">Default: false, determine whether if the query should include class's schedules info</param>
        /// <param name="includeAttendances">Default: false, determine whether if the query should include class's schedules attendances info</param>
        public async Task<ClassViewModel?> GetById(
            int id, 
            bool includeTeacher = true, 
            bool includeCourse = true,
            bool includeSession = false, 
            bool filterSession = false,
            bool includeSchedule = true,
            bool includeAttendances = true
        )
        {
            var clas = await _unitOfWork.ClassRepositories.GetByIdNoTracking(
                id, 
                includeTeacher: includeTeacher, 
                includeCourse: includeCourse, 
                includeSession: includeSession, 
                filterSession: filterSession,
                includeSchedule: includeSchedule,
                includeAttendances: includeAttendances
            );
            if (clas == null) return null;
            var viewCla = _mapper.Map<ClassViewModel>(clas);
            if(viewCla.Schedules != null && viewCla.Schedules.Count > 0)
            {
                for (int i = 0; i < viewCla.Schedules.Count; i++)
                {
                    viewCla.Schedules[i].ScheduleNumber = i + 1;
                }
            }
            return viewCla;
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

        public async Task<ClassViewModel?> GetByClassCode(string className)
        {
            var clas = await _unitOfWork.ClassRepositories.GetByClassName(className);
            return clas != null ? _mapper.Map<ClassViewModel>(clas) : null;
        }

        public async Task<List<ClassViewModel>> GetClassList()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetList());
        }

        public async Task<List<ClassViewModel>> GetClassListHome()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetListHome());
        }

        public async Task<List<GetClassForTransaction>> GetOptionListByStatusOpen(string filterClassByStudentName = "")
        {
            return _mapper.Map<List<GetClassForTransaction>>(await _unitOfWork.ClassRepositories.GetOptionListByStatusOpen(filterClassByStudentName));
        }

        public async Task<List<ClassViewModel>> GetClassListParent()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassListParent());
        }

        public async Task<List<ClassViewModel>> GetListByTeacherAccountId(int id)
        {
            var teacherId = await _unitOfWork.TeacherRepositories.GetIdByAccountId(id);
            if (teacherId == 0) return new List<ClassViewModel>();
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetListByTeacherId(teacherId));
        }
        
        public async Task<List<ClassViewModel>> GetListByStudentAccountId(int id)
        {
            var studentId = await _unitOfWork.StudentRepositories.GetIdByAccountIdNoTracking(id);
            if (studentId == 0) return new List<ClassViewModel>();
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetListByStudentId(studentId.Value));
            //return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetListByStudentId(studentId));
        }

        public async Task<List<ClassViewModel>> GetClassListFilter(ClassFilter filter)
        {
            var classes = await _unitOfWork.ClassRepositories.GetList();

            // Apply filtering
            if (!string.IsNullOrEmpty(filter.Status))
            {
                classes = classes.Where(c => c.Status.Equals(filter.Status, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return _mapper.Map<List<ClassViewModel>>(classes);
        }
        public async Task Update(int claId, UpdateClass upCla)
        {
            if (upCla == null)
                throw new ArgumentNullException(nameof(upCla), "update class info cannot be null.");

            // Fetch the existing record
            var cla = await _unitOfWork.ClassRepositories.GetByIdNoTracking(claId)
                ?? throw new KeyNotFoundException("Class not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upCla, cla);

            // Reattach entity and mark it as modified
            _unitOfWork.ClassRepositories.Update(cla);

            // Save changes
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., row modified by another user)
                throw new InvalidOperationException("Update failed due to a concurrency conflict.", ex);
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception("An unexpected error occurred while updating the class.", ex);
            }
        }

        public async Task UpdateStatus(int claId, string upClaStatus)
        {
            // Fetch the existing record
            var cla = await _unitOfWork.ClassRepositories.GetByIdNoTracking(claId, includeSchedule: true)
                ?? throw new KeyNotFoundException("Class not found.");
            // check is schedule date valid
            var errorList = await _unitOfWork.ScheduleRepositories.IsListScheduleDateHasValidSequenceByClassId(claId);
            if (errorList.Count > 0)
            {
                var errorMessage = "Class contain invalid schedule date: \n";
                foreach (var error in errorList) errorMessage += error;
                throw new ArgumentException(errorMessage);
            }
            if (upClaStatus == CEGConstants.CLASS_STATUS_OPEN)
            {
                foreach (var sche in cla.Schedules)
                {
                    sche.Status = CEGConstants.SCHEDULE_STATUS_UPCOMING;
                    _unitOfWork.ScheduleRepositories.Update(sche);
                }
            }

            if (upClaStatus == CEGConstants.CLASS_STATUS_ONGOING)
            {
                // Extract distinct session IDs from the class's schedules

                // - `cla.Schedules`: A collection of schedules associated with the class
                // - `.Select(s => s.SessionId)`: Extracts the SessionId from each schedule
                // - `.Distinct()`: Ensures each SessionId appears only once
                var sessionIds = cla.Schedules.Select(s => s.Session.SessionId).Distinct();

                // Fetch a dictionary mapping session IDs to their associated homework lists

                // - `_unitOfWork.HomeworkRepositories.GetListBySessionIds(sessionIds)`:
                //   Retrieves all homework items for the given session IDs from the database.
                // - `.GroupBy(h => h.SessionId)`:
                //   Groups the retrieved homework items by their SessionId.
                // - `.ToDictionary(g => g.Key, g => g.ToList())`:
                //   Converts the grouped items into a dictionary where:
                //     - Key: SessionId (unique identifier for each session)
                //     - Value: List of homework items associated with that SessionId
                var homeworkDictionary = (await _unitOfWork.HomeworkRepositories.GetListBySessionIds(sessionIds.ToArray()))
                    .GroupBy(h => h.SessionId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var enr in cla.Enrolls)
                {
                    bool isStuProExist = cla.StudentProgresses != null &&
                        cla.StudentProgresses.Any(stuPro => stuPro.ClassId == enr.ClassId && stuPro.StudentId == enr.StudentId);
                    // Add student progress
                    var stuPro = new StudentProgress()
                    {
                        Playtime = new TimeSpan(0,0,0),
                        TotalPoint = 0,
                        StudentId = enr.StudentId,
                        ClassId = enr.ClassId,
                    };
                    if (isStuProExist)
                    {
                        var stuProExist = cla.StudentProgresses
                            .FirstOrDefault(stuPro => stuPro.ClassId == enr.ClassId && stuPro.StudentId == enr.StudentId);
                        if(stuProExist != null)
                            stuPro = await _unitOfWork.StudentProgressRepositories.GetByIdNoTracking(stuProExist.StudentProgressId);
                    }

                    foreach (var sche in cla.Schedules)
                    {
                        var att = new Attendance()
                        {
                            StudentId = enr.StudentId,
                            ScheduleId = sche.ScheduleId,
                            HasAttended = CEGConstants.ATTENDANCE_STATUS_ABSENT,
                        };
                        var scheExist = await _unitOfWork.ScheduleRepositories.GetByIdNoTracking(sche.ScheduleId);
                        if(scheExist != null)
                        {
                            bool isScheAttExist = scheExist.Attendances != null && scheExist.Attendances.Any(att => att.StudentId == enr.StudentId && att.ScheduleId == sche.ScheduleId);
                            if (!isScheAttExist)
                            {
                                sche.Attendances ??= new List<Attendance>();
                                // Add attendance
                                sche.Attendances.Add(att);
                            }
                        }

                        enr.Student = null;

                        // Get homework list for the session using TryGetValue method
                        if (homeworkDictionary.TryGetValue(sche.Session.SessionId, out List<Homework> homList))
                        {
                            // Add student homework for each homework item in the list
                            foreach (var hom in homList)
                            {
                                if(stuPro.StudentHomeworks.Any(stuHom => stuHom.HomeworkId.HasValue && stuHom.HomeworkId.GetValueOrDefault() == hom.HomeworkId)) continue;
                                stuPro.StudentHomeworks.Add(new StudentHomework()
                                {
                                    CorrectAnswers = 0,
                                    HomeworkId = hom.HomeworkId,
                                    Playtime = new TimeSpan(0,0,0),
                                    Point = 0,
                                    Status = CEGConstants.STUDENT_HOMEWORK_STATUS_NOT_SUBMITTED,
                                    HomeworkResult = new HomeworkResult()
                                    {
                                        TotalCorrectAnswers = 0,
                                        Playtime = new TimeSpan(0,0,0),
                                        TotalPoint = 0,
                                    }
                                });
                            }
                        }
                    }
                    if (!isStuProExist)
                    {
                        cla.StudentProgresses ??= new List<StudentProgress>();
                        cla.StudentProgresses.Add(stuPro);
                    }
                    // Add attendance without setting the Schedule navigation property
                    /*var attendance = new Attendance()
                    {
                        StudentId = enr.StudentId,
                        ScheduleId = sche.ScheduleId, // Set only the foreign key
                        HasAttended = CEGConstants.ATTENDANCE_STATUS_ABSENT
                    };*/
                    // Add attendance directly to the database or to the collection
                    //_unitOfWork.AttendanceRepositories.Create(attendance);
                }
            }

            if (upClaStatus == CEGConstants.CLASS_STATUS_ENDED)
            {
                var adm = (await _unitOfWork.AccountRepositories.GetListByRole(CEGConstants.ACCOUNT_ROLE_ADMIN)).FirstOrDefault() ?? throw new KeyNotFoundException("Admin account not found for transaction.");
                var tea = (await _unitOfWork.TeacherRepositories.GetByIdNoTracking(cla.TeacherId)) ?? throw new KeyNotFoundException("Teacher account not found for transaction.");
                int amo = cla.EnrollmentFee * cla.Enrolls.Count * 70 / 100; // Transaction Amount
                var tra = new Transaction()
                {
                    VnpayId = null,
                    AccountId = adm.AccountId,
                    TransactionAmount = amo,
                    TransactionDate = DateTime.UtcNow,
                    ConfirmDate = DateTime.UtcNow,
                    TransactionStatus = CEGConstants.TRANSACTION_STATUS_COMPLETED,
                    TransactionType = CEGConstants.TRANSACTION_TYPE_EARNING,
                    Description = CEGConstants.TRANSACTION_PAYER_LABEL + CEGConstants.TRANSACTION_USER_SYSTEM_NAME_LABEL + "," +
                                CEGConstants.TRANSACTION_METHOD_LABEL + CEGConstants.TRANSACTION_METHOD_SYSTEM_DEPOSIT + "," +
                                CEGConstants.TRANSACTION_RECEIVER_LABEL +  CEGConstants.TRANSACTION_USER_TEACHER_NAME_LABEL + $"{tea.Account.Fullname}," +
                                CEGConstants.TRANSACTION_USER_TEACHER_ID_LABEL + $"{cla.TeacherId}," +
                                CEGConstants.TRANSACTION_DESCRIPTION_ASSIGNED_CLASS_NAME_LABEL + $"{cla.ClassName}",
                };
                _unitOfWork.TransactionRepositories.Create(tra);
                cla.Enrolls = null;
                tea.Account.TotalAmount += amo;
                _unitOfWork.TeacherRepositories.Update(tea);
            }

            cla.Status = upClaStatus;
            // Reattach entity and mark it as modified
            _unitOfWork.ClassRepositories.Update(cla);

            // Save changes
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., row modified by another user)
                throw new InvalidOperationException("Update failed due to a concurrency conflict.", ex);
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception("An unexpected error occurred while updating the class.", ex);
            }
        }

        public async Task<bool> IsEditableById(int id)
        {
            var clas = await _unitOfWork.ClassRepositories.GetByIdNoTracking(id);
            return clas != null && (clas.Status.Equals(CEGConstants.CLASS_STATUS_DRAFT) || clas.Status.Equals(CEGConstants.CLASS_STATUS_POSTPONED));
        }

        public async Task<int> GetTotalAmount()
        {
            return await _unitOfWork.ClassRepositories.GetTotalAmount();
        }

        public async Task<int> GetTotalAmountByTeacherAccountIdAndClassStatus(int id, string? status = null)
        {
            var teacherId = await _unitOfWork.TeacherRepositories.GetIdByAccountId(id);
            if (teacherId == 0) return 0;
            return await _unitOfWork.ClassRepositories.GetTotalAmountByTeacherId(teacherId, status);
        }

        public async Task<bool> CheckClassFull(string className)
        {
            var cla = await _unitOfWork.ClassRepositories.GetByClassName(className);
            if (cla.NumberOfStudents == cla.MaximumStudents) return true;
            return false;
        }

        public async Task<bool> UpdateListStatusByDate()
        {
            bool isOpenClassesUpdated = false;
            bool isOngoingClassesUpdated = false;
            // Fetch the existing record
            var claforOpenToOngoing = await _unitOfWork.ClassRepositories.GetListByStartDate(DateTime.Now)
                ?? throw new ("Open class list not found or empty.");
            foreach(var clafor in claforOpenToOngoing)
            {
                // Extract distinct session IDs from the class's schedules

                // - `cla.Schedules`: A collection of schedules associated with the class
                // - `.Select(s => s.SessionId)`: Extracts the SessionId from each schedule
                // - `.Distinct()`: Ensures each SessionId appears only once
                var sessionIds = clafor.Schedules.Select(s => s.SessionId).Distinct();

                // Fetch a dictionary mapping session IDs to their associated homework lists

                // - `_unitOfWork.HomeworkRepositories.GetListBySessionIds(sessionIds)`:
                //   Retrieves all homework items for the given session IDs from the database.
                // - `.GroupBy(h => h.SessionId)`:
                //   Groups the retrieved homework items by their SessionId.
                // - `.ToDictionary(g => g.Key, g => g.ToList())`:
                //   Converts the grouped items into a dictionary where:
                //     - Key: SessionId (unique identifier for each session)
                //     - Value: List of homework items associated with that SessionId
                var homeworkDictionary = (await _unitOfWork.HomeworkRepositories.GetListBySessionIds(sessionIds.ToArray()))
                    .GroupBy(h => h.SessionId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var enr in clafor.Enrolls)
                {
                    bool isStuProExist =
                        clafor.StudentProgresses.Any(stuPro => stuPro.ClassId == enr.ClassId && stuPro.StudentId == enr.StudentId);
                    // Add student progress
                    var stuPro = new StudentProgress()
                    {
                        Playtime = new TimeSpan(0,0,0),
                        TotalPoint = 0,
                        StudentId = enr.StudentId,
                        ClassId = enr.ClassId,
                    };

                    if (isStuProExist)
                    {
                        var stuProExist = clafor.StudentProgresses
                            .FirstOrDefault(stuPro => stuPro.ClassId == enr.ClassId && stuPro.StudentId == enr.StudentId);
                        if (stuProExist != null)
                            stuPro = await _unitOfWork.StudentProgressRepositories.GetByIdNoTracking(stuProExist.StudentProgressId);
                    }

                    foreach (var sche in clafor.Schedules)
                    {
                        var att = new Attendance()
                        {
                            StudentId = enr.StudentId,
                            ScheduleId = sche.ScheduleId,
                            HasAttended = CEGConstants.ATTENDANCE_STATUS_ABSENT,
                        };
                        var scheExist = await _unitOfWork.ScheduleRepositories.GetByIdNoTracking(sche.ScheduleId);
                        bool isScheAttExist = scheExist != null && scheExist.Attendances.Any(att => att.StudentId == enr.StudentId && att.ScheduleId == sche.ScheduleId);
                        if (!isScheAttExist)
                        {
                            // Add attendance
                            sche.Attendances.Add(att);
                        }

                        enr.Student = null;

                        // Get homework list for the session using TryGetValue method
                        if (homeworkDictionary.TryGetValue(sche.SessionId, out List<Homework> homList))
                        {
                            // Add student homework for each homework item in the list
                            foreach (var hom in homList)
                            {
                                if (stuPro.StudentHomeworks.Any(stuHom => stuHom.HomeworkId.HasValue && stuHom.HomeworkId.GetValueOrDefault() == hom.HomeworkId)) continue;
                                stuPro.StudentHomeworks.Add(new StudentHomework()
                                {
                                    CorrectAnswers = 0,
                                    HomeworkId = hom.HomeworkId,
                                    Playtime = new TimeSpan(0,0,0),
                                    Point = 0,
                                    Status = CEGConstants.STUDENT_HOMEWORK_STATUS_NOT_SUBMITTED,
                                    HomeworkResult = new HomeworkResult()
                                    {
                                        TotalCorrectAnswers = 0,
                                        Playtime = new TimeSpan(0,0,0),
                                        TotalPoint = 0,
                                    }
                                });
                            }
                        }
                    }
                    if (!isStuProExist)
                        clafor.StudentProgresses.Add(stuPro);
                }

                clafor.Status = CEGConstants.CLASS_STATUS_ONGOING;

                // Reattach entity and mark it as modified
                _unitOfWork.ClassRepositories.Update(clafor);
            }
            if (claforOpenToOngoing.Count > 0 && claforOpenToOngoing.All(cla => cla.Status == CEGConstants.CLASS_STATUS_ONGOING)) 
                isOpenClassesUpdated = true;

            // Fetch the existing record
            var claforOngoingToEnded = await _unitOfWork.ClassRepositories.GetListByEndDate(DateTime.Now)
                ?? throw new("Ongoing class list not found or empty.");

            var adm = (await _unitOfWork.AccountRepositories.GetListByRole(CEGConstants.ACCOUNT_ROLE_ADMIN)).FirstOrDefault() ?? throw new KeyNotFoundException("Admin account not found for transaction.");

            // key: teacherId, value: totalAmount
            var teacherEaringForUpdates = new Dictionary<int, int>();
            foreach (var clafor in claforOngoingToEnded)
            {
                int amo = clafor.EnrollmentFee * clafor.Enrolls.Count * 70 / 100; // Transaction Amount

                var tea = await _unitOfWork.TeacherRepositories.GetByIdNoTracking(clafor.TeacherId) ?? throw new KeyNotFoundException("Teacher account not found for transaction.");

                // record teacher's new earning to dictionary
                teacherEaringForUpdates[clafor.TeacherId] = teacherEaringForUpdates.ContainsKey(clafor.TeacherId)
                                                    ? teacherEaringForUpdates[clafor.TeacherId] + amo
                                                    : amo;
                var tra = new Transaction()
                {
                    VnpayId = null,
                    AccountId = adm.AccountId,
                    TransactionAmount = amo,
                    TransactionDate = DateTime.UtcNow,
                    ConfirmDate = DateTime.UtcNow,
                    TransactionStatus = CEGConstants.TRANSACTION_STATUS_COMPLETED,
                    TransactionType = CEGConstants.TRANSACTION_TYPE_EARNING,
                    Description = CEGConstants.TRANSACTION_PAYER_LABEL + CEGConstants.TRANSACTION_USER_SYSTEM_NAME_LABEL + "," +
                                CEGConstants.TRANSACTION_METHOD_LABEL + CEGConstants.TRANSACTION_METHOD_SYSTEM_DEPOSIT + "," +
                                CEGConstants.TRANSACTION_RECEIVER_LABEL + CEGConstants.TRANSACTION_USER_TEACHER_NAME_LABEL + $"{tea.Account.Fullname}," +
                                CEGConstants.TRANSACTION_USER_TEACHER_ID_LABEL + $"{clafor.TeacherId}," +
                                CEGConstants.TRANSACTION_DESCRIPTION_ASSIGNED_CLASS_NAME_LABEL + $"{clafor.ClassName}",
                };

                _unitOfWork.TransactionRepositories.Create(tra);
                clafor.Enrolls = null;
                clafor.Status = CEGConstants.CLASS_STATUS_ENDED;

                // Reattach entity and mark it as modified
                _unitOfWork.ClassRepositories.Update(clafor);
            }

            // Update teacher totals in bulk
            foreach (var teacherUpdate in teacherEaringForUpdates)
            {
                var teacher = await _unitOfWork.TeacherRepositories.GetByIdNoTracking(teacherUpdate.Key) ?? throw new KeyNotFoundException("Teacher account not found for transaction.");
                teacher.Account.TotalAmount += teacherUpdate.Value;
                _unitOfWork.TeacherRepositories.Update(teacher);
            }

            if (claforOngoingToEnded.Count > 0 && claforOngoingToEnded.All(cla => cla.Status == CEGConstants.CLASS_STATUS_ENDED))
                isOngoingClassesUpdated = true;
            // Save changes
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., row modified by another user)
                throw new InvalidOperationException("Update failed due to a concurrency conflict.", ex);
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception("An unexpected error occurred while updating all classes.", ex);
            }
            return isOpenClassesUpdated && isOngoingClassesUpdated;
        }
    }
}
