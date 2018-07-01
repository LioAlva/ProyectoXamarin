using PModelo.ViewModels;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using System;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParqueaderoDetailPage : ContentPage
	{
		public ParqueaderoDetailPage ()
		{
			InitializeComponent ();
            

            //this.BindingContext = new ParqueaderoItemViewModel();
            //var mainViewModel = MainViewModel.GetInstance();
            //foreach (Pin item in mainViewModel.Pins2)
            //{
            //    //MyMapa.Pins.Add(item);
            //}
            //Locator(mainViewModel.PositionsState);
        }

        private void Locator(Position PositionsState)
        {
            //var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;
            var position = new Position(PositionsState.Latitude, PositionsState.Longitude);
            //MyMapa.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.5)));

        }
    }
}