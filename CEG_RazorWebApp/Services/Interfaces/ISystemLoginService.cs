namespace CEG_RazorWebApp.Services.Interfaces
{
    public interface ISystemLoginService
    {
        Task<string?> GetTokenAsync();
    }
}
