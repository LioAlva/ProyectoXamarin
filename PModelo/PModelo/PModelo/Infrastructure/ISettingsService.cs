namespace PModelo.Infrastructure
{
    public interface ISettingsService
    {
        bool IsPermit { get; }
        void OpenSettings();
        bool IsConnected2();
    }
}
