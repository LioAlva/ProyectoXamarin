using PModelo.Pages;
using PModelo.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PModelo.Services
{
    public class NavigationService
    {

        public async Task Navigate(string PageName)
        {

            //var m = MasterPage.GetInstance();
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
                case "ListaRojaPage":
                    await App.Navigator.PushAsync(new ProbaPage());
                    break;
                case "LocatorioPage":
                    await App.Navigator.PushAsync(new ProbaPage());
                    break;
                case "EventosPage":
                    await App.Navigator.PushAsync(new EventosPage());
                    break;
                case "EventoPage":
                    await App.Navigator.PushAsync(new EventoPage());
                    break;
                case "ConfiguracionPage":
                    await App.Navigator.PushAsync(new NewOrderPage());
                    break;
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
                //UsuariosPage

                default:
                    break;

                case "EventoPopPage":
                    //await App.Navigator.PushAsync(new EventoPopPage());
                    break;//UsuariosGroupPage


                case "UsuariosGroupPage":
                    await App.Navigator.PushAsync(new UsuariosGroupPage());
                    break;//UsuariosGroupPage
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
