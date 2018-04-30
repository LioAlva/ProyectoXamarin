using GalaSoft.MvvmLight.Command;
using PModelo.Models;
using PModelo.Pages;
using PModelo.Services;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace PModelo.ViewModels
{
    public class ContactItemViewModel : ContactInfo
    {
        #region Attributes
        public NavigationService navigationService;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public ContactItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region 
        //EventDetailCommand

        #endregion


        #region Commands
        public ICommand EventDetailCommand { get { return new RelayCommand(EventDetail); } }

        private async void EventDetail()
        {
            await  navigationService.Navigate("EventoPopPage");
        }
        #endregion

        private async void OnOpenPupup()
        {
            //var page = new LoginPopupPage();
            //           var response= await  Navigation.PushPopupAsync(new EventoPopPage());
            await navigationService.Navigate("EventoPopPage");
            
        }
    }
}
