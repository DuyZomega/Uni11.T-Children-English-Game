using BAL.ViewModels;

namespace WebAppMVC.Models.Notification
{
    public class GetUserNotificationResponse : DefaultResponseViewModel<IEnumerable<NotificationViewModel>>
    {
        public GetUserNotificationResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
