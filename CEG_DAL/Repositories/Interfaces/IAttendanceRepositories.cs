using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IAttendanceRepositories : IRepositoryBase<Attendance>
    {
        Task<List<Attendance>?> GetListNoTracking();
        Task<List<Attendance>?> GetListByScheduleIdNoTracking(int scheduleId);
        Task<Attendance?> GetByIdNoTracking(int id);
    }
}
