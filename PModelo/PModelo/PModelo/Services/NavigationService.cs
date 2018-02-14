using PModelo.Pages;
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

        internal void SetMainPage(Page page)
        {
            App.Current.MainPage = page;
        }
    }

}
