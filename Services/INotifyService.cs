namespace PasechnikovaPR33p18.Services
{
    public interface INotifyService
    {
        void Error(string message);
        bool HasError { get; }
        string? ErrorMessage { get; }
        void Success(string message);
        bool HasSuccess { get; }
        string? SuccessMessage { get; }
        void Clear();
    }
}
