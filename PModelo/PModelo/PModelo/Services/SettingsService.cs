using PModelo.Infrastructure;
using Xamarin.Forms;

namespace PModelo.Services
{
    public class SettingsService
    {
        public bool IsConnected()
        {
            var permitedConnection = DependencyService.Get<ISettingsService>();
            permitedConnection.OpenSettings();
            return permitedConnection.IsPermit;
        }

        public bool IsPermited()
        {
            var permitedConnection = DependencyService.Get<ISettingsService>();
            return permitedConnection.IsConnected2();
        }

    }
}
