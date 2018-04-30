using PModelo.Services;
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
	public partial class ProbaPage : ContentPage    
	{
		public ProbaPage ()
		{
			InitializeComponent ();
           // var nav = NavigationPage(new TabPage());
           
		}

      

        public async void OnNavigateButtonClicked(object sender, EventArgs e)
        {

            NavigationService ns = new NavigationService();

            //var contact = new Contact
            //{
            //    Name = "Jane Doe",
            //    Age = 30,
            //    Occupation = "Developer",
            //    Country = "USA"
            //};

            //var secondPage = new SecondPage();
            //secondPage.BindingContext = contact;

           // await Navigation.PushAsync(new WelcomePage());
           
            await ns.Navigate("WelcomePage");
        }
    }
}