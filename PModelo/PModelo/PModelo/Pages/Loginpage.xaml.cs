using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Loginpage : ContentPage
	{
		public Loginpage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //App.MasterLogin = this;
            //App.Navigator = NavigatorL;
        }

        async void OnUpcomingAppointmentsButtonClicked(object sender, EventArgs e)
        {
            //await App.Navigator.PushAsync(new MasterPage());
            await Navigation.PushAsync(new WelcomePage());

        }

        //OnUpcomingAppointmentsRETURNClicked
        async void OnUpcomingAppointmentsRETURNClicked(object sender, EventArgs e)
        {
            //await App.Navigator.PushAsync(new MasterPage());
            await Navigation.PopToRootAsync();

        }
    }
}