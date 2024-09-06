using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Notification
{
    public class GetNotificationPostResponse : DefaultResponseViewModel<NotificationViewModel>
    {
        public GetNotificationPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
