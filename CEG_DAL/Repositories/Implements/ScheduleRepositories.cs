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
    public class ScheduleRepositories : RepositoryBase<Schedule>, IScheduleRepositories
    {
        private readonly MyDBContext _dbContext;

        public ScheduleRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateBySessionId(int sessionId)
        {
            
        }

        public async Task<Schedule?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Schedules
                .AsNoTrackingWithIdentityResolution()
                .Select(sch => new Schedule()
                {
                    ScheduleId = sch.ScheduleId,
                    ScheduleDate = sch.ScheduleDate,
                    StartTime = sch.StartTime,
                    EndTime = sch.EndTime,
                    Status = sch.Status,
                    SessionId = sch.SessionId,
                    ClassId = sch.ClassId,
                    Session = new Session()
                    {
                        SessionId = sch.SessionId,
                        SessionNumber = sch.Session.SessionNumber,
                        Title = sch.Session.Title,
                        Description = sch.Session.Description,
                        Hours = sch.Session.Hours,
                        CourseId = sch.Session.CourseId,
                    },
                    Class = new Class()
                    {
                        ClassId = sch.ClassId,
                        ClassName = sch.Class.ClassName,
                        Status = sch.Class.Status,
                    },
                    Attendances = sch.Attendances.Select(att => new Attendance()
                    {
                        AttendanceId = att.AttendanceId,
                        ScheduleId = att.ScheduleId,
                        StudentId = att.StudentId,
                        HasAttended = att.HasAttended,
                    }).ToList(),
                })
                .FirstOrDefaultAsync(s => s.ScheduleId == id);
        }

        public async Task<List<Schedule>?> GetListByClassId(int claId)
        {
            return await _dbContext.Schedules
                .AsNoTrackingWithIdentityResolution()
                .Where(s => s.ClassId == claId)
                .Select(sch => new Schedule()
                {
                    ScheduleId = sch.ScheduleId,
                    ScheduleDate = sch.ScheduleDate,
                    StartTime = sch.StartTime,
                    EndTime = sch.EndTime,
                    Status = sch.Status,
                    Session = new Session()
                    {
                        SessionId = sch.SessionId,
                        SessionNumber = sch.Session.SessionNumber,
                        Title = sch.Session.Title,
                        Description = sch.Session.Description,
                        Hours = sch.Session.Hours
                    }
                })
                .OrderBy(sch => sch.ScheduleDate)
                .ToListAsync();
        }

        public async Task<bool> IsListScheduleDateValidByClassId(int claId)
        {
            return await _dbContext.Schedules
                .AsNoTrackingWithIdentityResolution()
                .Where(sch => sch.ClassId == claId)
                .AllAsync(sch => 
                    sch.ClassId == claId &&
                    sch.ScheduleDate >= sch.Class.StartDate && 
                    sch.ScheduleDate <= sch.Class.EndDate
                );
        }

        public async Task<List<string>> IsListScheduleDateHasValidSequenceByClassId(int claId)
        {
            var errorList = new List<string>();

            // Fetch schedules for the given class, ordered by date
            var schedules = await _dbContext.Schedules
                .AsNoTrackingWithIdentityResolution()
                .Where(sch => sch.ClassId == claId)
                .Select(sch => new Schedule
                {
                    ScheduleDate = sch.ScheduleDate, 
                    Class = new Class
                    {
                        StartDate = sch.Class.StartDate,
                        EndDate = sch.Class.EndDate,
                    }
                })
                .OrderBy(sch => sch.ScheduleDate) // Ensure sequential processing
                .ToListAsync();

            // Check for errors
            for (int i = 0; i < schedules.Count; i++)
            {
                var currentSchedule = schedules[i];

                // Validate the schedule date is within the class range
                if (currentSchedule.ScheduleDate < currentSchedule.Class.StartDate ||
                    currentSchedule.ScheduleDate > currentSchedule.Class.EndDate)
                {
                    errorList.Add(
                        $"Schedule {i + 1} has a date " +
                        $"({currentSchedule.ScheduleDate.Value.ToString("dd/MM/yyyy hh:mm tt")}) " +
                        $"outside the class range " +
                        $"({currentSchedule.Class.StartDate.Value.ToString("dd/MM/yyyy hh:mm tt")} " +
                        $"to " +
                        $"{currentSchedule.Class.EndDate.Value.ToString("dd/MM/yyyy hh:mm tt")}).\n");
                }

                // Validate the schedule sequence (if this is not the first schedule)
                if (i > 0)
                {
                    var previousSchedule = schedules[i - 1];
                    if (currentSchedule.ScheduleDate.Value.Date == previousSchedule.ScheduleDate.Value.Date)
                    {
                        errorList.Add(
                            $"Schedule {i + 1} has the same date " +
                            $"({currentSchedule.ScheduleDate.Value.ToString("dd/MM/yyyy hh:mm tt")}) " +
                            $"as the previous schedule {i}.\n");
                    }
                }
            }

            return errorList;
        }
    }
}
