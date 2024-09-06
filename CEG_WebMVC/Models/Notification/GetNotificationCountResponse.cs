using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Notification
{
    public class GetNotificationCountResponse : DefaultResponseViewModel<object>
    {
        public GetNotificationCountResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
