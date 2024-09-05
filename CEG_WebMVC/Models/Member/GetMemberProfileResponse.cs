using BAL.ViewModels;

namespace CEG_WebMVC.Models.Member
{
    public class GetMemberProfileResponse : DefaultResponseViewModel<MemberViewModel>
    {
        public GetMemberProfileResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
