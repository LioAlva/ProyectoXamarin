using Android.Content;
using Android.Locations;
using PModelo.Infrastructure;
using Xamarin.Forms;

[assembly: Dependency(typeof(PModelo.Droid.SettingsServiceAndroid))]
namespace PModelo.Droid
{
    public class SettingsServiceAndroid : ISettingsService
    {
        public bool IsPermit { get; set; }

        public bool IsConnected2()
        {
            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
            return (bool)locationManager.IsProviderEnabled(LocationManager.GpsProvider);
        }

        public void OpenSettings()
        {
            // Forms.Context.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings));

            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                Forms.Context.StartActivity(gpsSettingIntent);
            }


            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider))
            {
                IsPermit = true;
            }
            else
            {
                IsPermit = false;
            }

        }
    }
}