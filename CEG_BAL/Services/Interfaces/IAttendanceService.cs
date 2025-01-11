using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels;

namespace CEG_BAL.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task Create(CreateNewClass newCla);
        Task Update(int claId, UpdateClass upCla);
        Task UpdateStatus(int claId, string upClaStatus);
        Task<List<AttendanceViewModel>> GetListNoTracking();
        Task<List<AttendanceViewModel>> GetListByScheduleId(int id);
        Task<AttendanceViewModel?> GetById(int id);
        Task<int> GetTotalAmount();
    }
}
