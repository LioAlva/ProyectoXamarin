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
	public partial class WelcomePage : ContentPage
	{
		public WelcomePage ()
		{
			InitializeComponent ();
		}

        async void OnUpcomingAppointmentsRETURNClicked(object sender, EventArgs e)
        {
            //await App.Navigator.PushAsync(new MasterPage());
            await Navigation.PopToRootAsync();

        }
    }
}