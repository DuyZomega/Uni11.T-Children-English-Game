using BAL.ViewModels.Manager;

namespace CEG_WebMVC.Models.Member
{
    public class GetListMemberResponse : DefaultResponseViewModel<List<GetMembershipExpire>>
    {
        public GetListMemberResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
