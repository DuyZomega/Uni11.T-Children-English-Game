namespace CEG_RazorWebApp.Libraries.Models
{
    public class DefaultResponseModel<T>
    {
        public DefaultResponseModel(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public DefaultResponseModel()
        {
            Status = false;
        }
        public T? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
