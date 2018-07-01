
using PModelo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlacesPage : ContentPage
	{
		public PlacesPage ()
		{
			InitializeComponent ();
            //BindingContext = new PlacesViewModel(Navigation);
        }
	}
}