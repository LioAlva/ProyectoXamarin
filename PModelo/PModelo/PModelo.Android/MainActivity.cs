
using Android.App;
using Android.Content.PM;
using Android.OS;
using Java.Lang;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Debug = System.Diagnostics.Debug;

//https://www.youtube.com/watch?v=0Qjx671cgYg
namespace PModelo.Droid
{
    // [Activity(Label = "PModelo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "PModelo", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override  void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            //Rg.Plugins.Popup.Popup.Init(this, bundle);
            //Couchbase.Lite.Storage.CustomSQLite.Plugin.Register();

            // CrossCurrentActivity.Current.Init(this, bundle);
            // await CrossCurrentActivity.Current.Activity;

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ButtonCircle.FormsPlugin.Droid.ButtonCircleRenderer.Init();//Button circle
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

