using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Contest
{
    public class GetListContestParticipation : DefaultResponseViewModel<List<ContestParticipantViewModel>>
    {
        public GetListContestParticipation(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
