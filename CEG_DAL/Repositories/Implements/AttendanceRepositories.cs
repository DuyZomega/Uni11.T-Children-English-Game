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
    public class AttendanceRepositories : RepositoryBase<Attendance>, IAttendanceRepositories
    {
        private readonly MyDBContext _dbContext;
        public AttendanceRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Attendance?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Attendances
                .AsNoTrackingWithIdentityResolution()
                .Where(att => att.AttendanceId == id)
                .Select(att => new Attendance()
                {
                    AttendanceId = att.AttendanceId,
                    ScheduleId = att.ScheduleId,
                    StudentId = att.StudentId,
                    HasAttended = att.HasAttended
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<Attendance>?> GetListByScheduleIdNoTracking(int scheduleId)
        {
            return await _dbContext.Attendances
                .AsNoTrackingWithIdentityResolution()
                .Where(att => att.ScheduleId == scheduleId)
                .Select(att => new Attendance()
                {
                    AttendanceId = att.AttendanceId,
                    ScheduleId = att.ScheduleId,
                    StudentId = att.StudentId,
                    HasAttended = att.HasAttended,
                    Student = new Student()
                    {
                        Account = new Account()
                        {
                            Fullname = att.Student.Account.Fullname,
                        }
                    }
                })
                .ToListAsync();
        }

        public async Task<List<Attendance>?> GetListNoTracking()
        {
            return await _dbContext.Attendances
                .AsNoTrackingWithIdentityResolution()
                .Select(att => new Attendance()
                {
                    AttendanceId = att.AttendanceId,
                    ScheduleId = att.ScheduleId,
                    StudentId = att.StudentId,
                    HasAttended = att.HasAttended
                })
                .ToListAsync();
        }
    }
}
