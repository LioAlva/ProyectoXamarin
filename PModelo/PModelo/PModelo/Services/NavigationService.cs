using PModelo.Models;
using PModelo.Pages;
using PModelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PModelo.Services
{
    public class NavigationService
    {
        public void Navigate(string PageName)
        {
            App.Master.IsPresented = false;

            switch (PageName)
            {
                case "AlarmsPage":
                    App.Navigator.PushAsync(new ProbaPage());
                    break;
                case "ClientsPage":
                    App.Navigator.PushAsync(new ProbaPage());
                    break;
                case "SettingsPage":
                    App.Navigator.PushAsync(new ProbaPage());
                    break;
                case "NewOrderPage":
                    App.Navigator.PushAsync(new NewOrderPage());
                    break;
                case "MainPage":
                    App.Navigator.PopToRootAsync();
                    break;
                default:
                    break;
            }
        }

        public void SetMainPage(string page)
        {
            switch (page)
            {
                case "MasterPage":
                    App.Current.MainPage = new MasterPage();
                    break;
                //case "LoginPage":
                //    App.Current.MainPage = new LoginPage();
                //    break;
                //case "SignInPage":
                //    App.CurrentUser = user;//esta
                //    App.Current.MainPage = new SignInPage();
                //    break;
                //case "LoginFacebookPage":
                //    App.Current.MainPage = new LoginFacebookPage();
                //    break;
                //case "PasswordRecoverPage":
                //    App.Current.MainPage = new PasswordRecoverPage();
                //    break;
                default: break;
            }
        }
    }

}
