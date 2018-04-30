using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventosPage : ContentPage
	{

        //private EventoPopPage _loginPopup;

        public EventosPage ()
		{
			InitializeComponent ();
           // _loginPopup = new EventoPopPage();

        }

        //private async void OnOpenPupup()
        //{
        //    //var page = new LoginPopupPage();
        //    var response = await Navigation.PushModalAsync(new EventoPopPage());
        //    //await navigationService.Navigate("EventoPopPage");
        //}



        private async void OnOpenPupup(object sender, EventArgs e)
        {
            //var page = new LoginPopupPage();
           // await Navigation.PushPopupAsync(_loginPopup);
        }




        //public async void OnDelete(object sender, EventArgs e)
        //{
        //    var mi = ((MenuItem)sender);

        //    var result = await DisplayAlert("Delete?", "Are you sure you want to remove this store?", "Yes", "No");
        //    if (result)
        //    {
        //        //await viewModel.DeleteStore(mi.CommandParameter as Store);
        //    }
        //}
    }
}