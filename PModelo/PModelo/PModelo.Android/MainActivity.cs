
using Android.App;
using Android.Content.PM;
using Android.OS;
using Debug = System.Diagnostics.Debug;


namespace PModelo.Droid
{
    // [Activity(Label = "PModelo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "PModelo", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            //Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            //if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            //{
            //    Debug.WriteLine("Android back button: There are some pages in the PopupStack");
            //}
            //else
            //{
            //    Debug.WriteLine("Android back button: There are not any pages in the PopupStack");
            //}
        }

    }
}

