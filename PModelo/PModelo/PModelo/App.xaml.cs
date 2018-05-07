using PModelo.Models;
using PModelo.Pages;

using Xamarin.Forms.Internals;
using Xamarin.Forms;
using System;
using PModelo.ViewModels;
using Rg.Plugins.Popup.Pages;
using PModelo.Services;

namespace PModelo
{
    [Preserve(AllMembers = true)]
    public partial class App : Application
    {
        //public static MasterPage Master { get; internal set; }

        //public static NavigationPage Navigator { get; internal set; }

        
        //public static User CurrentUser { get; internal set; }

        #region Attributes
        public DataService dataService;
        public DialogService dialogService;
        #endregion

        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public static User CurrentUser { get; internal set; }
        #endregion


        public App ()
		{

			InitializeComponent();

            try
            {
                dataService = new DataService();
                dialogService = new DialogService();

                var user = dataService.First<User>(false);

                if (user != null && user.IsRemembered && user.TokenExpires>DateTime.Now)
                {
                    //var mainViewModel = MainViewModel.GetInstance();
                    //if (string.IsNullOrEmpty(user.Phone) && user.UserTipeId == 1)
                    //{
                    //    var profile = new FacebookResponse
                    //    {
                    //        Name = user.FirstName,
                    //        Link = user.Picture
                    //    };

                    //    mainViewModel.Profile = new ProfileViewModel(profile, 1, profile.Link);
                    //    MainPage = new ProfilePage();
                    //}
                    //else
                    //{
                    //    //mainViewModel.ReloadUser(user);
                    //    //mainViewModel.LoadNewUserWhite();
                    //    //mainViewModel.LoadListAlertsForUser(true);

                    //    //App.CurrentUser = user;
                    //    MainPage = new MasterPage();
                    //}
                    App.CurrentUser = user;
                    MainPage = new MasterPage();
                }
                else
                {
                    MainPage = new NewLoginPage();
                }

                //MainPage = new MasterPage();
                //MainPage = new Loginpage();

            }
            catch (Exception ex)
            {

                throw;
            }  
        }


        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

    }
}




/******
 * 
 //var page = SampleBrowser.Core.SampleBrowser.GetMainPage("PModelo", "PModelo.Pages");

      MainPage = new BarPage();
                //MainPage = new CierreVentarPage();

//public static NavigationPage NavigatorL { get; internal set; }
        //public static INavigation NavigationI { get; internal set; }

        //public static Navigation NavigationI { get; internal set; }

      //  public static MasterPage Master { get; internal set; }

        //public static Loginpage MasterLogin { get; internal set; }

    //var page = SampleBrowser.Core.SampleBrowser.GetMainPage("DataSource", "SampleBrowser.DataSource");

                //var page = SampleBrowser.Core.SampleBrowser.GetMainPage("DataSource", "SampleBrowser.DataSource");
                //var page = SampleBrowser.Core.SampleBrowser.GetMainPage("DataSource", "PModelo.Pages");


                //MainPage = page;

//public static BarPage MasterBar { get; internal set; }

//public static Dashboard MasterDashboard { get; internal set; }
//public static TabbedPage MasterBar { get; internal set; }

//public static NavigationPage NavigatorBar { get; internal set; }

#region Navigators

public static TabPage NavigatorBar { get; internal set; }
public static NavigationPage NavigatorD { get; internal set; }

#endregion

      #region Singleton

        static App instance;

        public static App GetInstance()
        {
            if (instance == null)
            {
                instance = new App();
            }

            return instance;
        }

        #endregion

**************/


