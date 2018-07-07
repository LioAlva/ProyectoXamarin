﻿using PModelo.Pages;
using PModelo.Pages.Account;
using PModelo.Pages.GridP;
using PModelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PModelo.Services
{
    public class NavigationService
    {
        #region Attributes
        private DataService dataService;
        #endregion

        #region Constructor
        public NavigationService()
        {
            dataService = new DataService();
        }
        #endregion


        public async Task Navigate(string PageName)
        {

             var main = MainViewModel.GetInstance();
            //App.Master=m;
            App.Master.IsPresented = false;
            //App.MasterBar.Master.IsPresented=false;

            switch (PageName)
            {
                case "DashboardPage":
                    await App.Navigator.PushAsync(new DashboardPage());
                    break;
                case "CierreVentasPage":
                    await App.Navigator.PushAsync(new CierreVentasPage());
                    break;
                case "MapUbicateParkingPage":
                    await App.Navigator.PushAsync(new MapUbicateParkingPage());
                    break;

                //PlacesPage
                case "PlacesPage":
                    await App.Navigator.PushAsync(new PlacesPage());
                    break;
                case "Page":
                    await App.Navigator.PushAsync(new BreakfastPage());
                    break;
                case "TabbedCenterPage":
                    await App.Navigator.PushAsync(new TabbedCenterPage());
                    break;
                case "ReservaTimePickerPage":
                    await App.Navigator.PushAsync(new ReservaTimePickerPage());
                    break;
                case "MainPage2":
                   // await App.Navigator.PushAsync(new ParkingAdminPage());
                    break;


                    //case "ListaRojaPage":
                    //    await App.Navigator.PushAsync(new ProbaPage());
                    //    break;
                    //case "LocatorioPage":
                    //    await App.Navigator.PushAsync(new ProbaPage());
                    //    break;
                    //case "EventosPage":
                    //    await App.Navigator.PushAsync(new EventosPage());
                    //    break;
                    //case "EventoPage":
                    //    await App.Navigator.PushAsync(new EventoPage());
                    //    break;
                    //case "ConfiguracionPage":
                    //    await App.Navigator.PushAsync(new NewOrderPage());
                    //    break;

                    //UsuariosPage
                    //case "UsuariosPage":
                    //    await App.Navigator.PushAsync(new UsuariosPage());
                    //    break;
                    //UsuariosPage
                    
                case "AdminParkinReservePage":
                    main.ReservaIdentity.Conectarse();
                    await App.Navigator.PushAsync(new AdminParkinReservePage());
                    break;
                case "ParqueaderoDetailPage":
                    await App.Navigator.PushAsync(new ParqueaderoDetailPage());
                    break;
                //ParkingDetailPage
                case "ListViewPage":
                    await App.Navigator.PushAsync(new DataGridViewPage());
                    break;
                case "ReportsPage":
                    await App.Navigator.PushAsync(new ReportsPage());
                    break;
                case "UsuariosPage":
                    await App.Navigator.PushAsync(new UsuariosPage());
                    break;
                case "UsersGroupPage":
                    await App.Navigator.PushAsync(new UsersGroupPage());
                    break;
                case "SearchParkingPage":
                    await App.Navigator.PushAsync(new SearchParkingPage());
                    break;
                case "ParkingPage":
                    await App.Navigator.PushAsync(new ParkingPage());
                    break;
                default:
                    break;
                case "EventoPopPage":
                    //await App.Navigator.PushAsync(new EventoPopPage());
                    break;
                case "UsuariosGroupPage":
                    await App.Navigator.PushAsync(new UsuariosGroupPage());
                    break;
                case "MainPage":
                    App.Current.MainPage = new MasterPage();
                    break;
                case "LogutPage":
                    Logout();
                    ; break;
            }
        }

        private void Logout()
        {
            App.CurrentUser.IsRemembered = false; //lo dejamos de recordar por que hicimos un logut ,ahora creamos un servicio que nos ayudaraupdetear uduarios
            dataService.UpdateUser(App.CurrentUser);
            App.Current.MainPage = new NewLoginPage();
        }

        public void SetMainPage(string page)
        {
            switch (page)
            {
                case "RegisterUser":
                    App.Current.MainPage = new RegisterUser();
                    break;
                case "NewLoginPage":
                    App.Current.MainPage = new NewLoginPage();
                    break;
                case "MasterPage":
                    App.Current.MainPage = new MasterPage();
                    break;
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
