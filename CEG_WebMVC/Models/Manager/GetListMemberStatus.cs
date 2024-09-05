using BAL.ViewModels.Manager;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Manager
{
    public class GetListMemberStatus : DefaultResponseViewModel<IEnumerable<GetMemberStatus>>
    {
        public GetListMemberStatus(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
