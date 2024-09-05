using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Notification
{
    public class GetUserNotificationResponse : DefaultResponseViewModel<IEnumerable<NotificationViewModel>>
    {
        public GetUserNotificationResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
