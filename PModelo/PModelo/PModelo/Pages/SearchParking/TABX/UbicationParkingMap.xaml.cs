using Plugin.Geolocator;
using PModelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UbicationParkingMap : ContentPage
	{
		public UbicationParkingMap ()
		{
			InitializeComponent ();
            this.BindingContext = new ParqueaderoItemViewModel();
            var mainViewModel = MainViewModel.GetInstance();
            foreach (Pin item in mainViewModel.Pins2)
            {
                MyMapa.Pins.Add(item);
            }
            //Locator(mainViewModel.PositionsState);
            Locator(mainViewModel.PositionsState);

        }

        private void Locator(Position PositionsState)
        {
            //var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;
            var position = new Position(PositionsState.Latitude, PositionsState.Longitude);
            MyMapa.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.15)));
        }
    }
}