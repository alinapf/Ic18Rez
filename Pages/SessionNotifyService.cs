using PasechnikovaPR33p18.Services;

namespace PasechnikovaPR33p18.Pages
{
    public class SessionNotifyService : INotifyService
    {
        private readonly ISession session;
        private const string successKey = "success_message";
        private const string errorKey = "error_message";

        public SessionNotifyService(IHttpContextAccessor httpContext)
        {
            this.session = httpContext.HttpContext.Session;
        }

        public bool HasError => !string.IsNullOrEmpty(session.GetString(errorKey));

        public string? ErrorMessage => session.GetString(errorKey);

        public bool HasSuccess => !string.IsNullOrEmpty(session.GetString(successKey));

        public string? SuccessMessage => session.GetString(successKey);

        public void Clear()
        {
            session.Remove(errorKey);
            session.Remove(successKey);
        }

        public void Error(string message)
        {
            session.SetString(errorKey, message);
        }

        public void Success(string message)
        {
            session.SetString(successKey, message);
        }
    }
}
