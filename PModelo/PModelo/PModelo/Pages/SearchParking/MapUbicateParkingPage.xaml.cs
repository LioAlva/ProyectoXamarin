
using Plugin.Geolocator;
using PModelo.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapUbicateParkingPage : ContentPage
	{
		public MapUbicateParkingPage ()
		{
                InitializeComponent();

                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.SetGeolocationParqueaderos(
                mainViewModel.Parqueaderos);

                foreach (Pin item in mainViewModel.Pins)
                {
                    MyMap.Pins.Add(item);
                }
                Locator(mainViewModel.PositionsSearch);


            //MyMap.+= (sender, args) =>
            //{
            //    var pin = args.SelectedItem as Pin;
            //    var details = PinMap[pin];
            //    var page = new DetailPage<T>(details);
            //    Navigation.PushAsync(page);
            //};

        }




    private void Locator(Position PositionAlert)
            {
                try
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    var position = new Position(PositionAlert.Latitude, PositionAlert.Longitude);
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.05)));

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }
    }





//    var mainViewModel = new MainViewModel();
//    mainViewModel.GetGeolotation();

//    foreach (Pin item in mainViewModel.Pins)
//    {
//        MyMap.Pins.Add(item);
//    }

//    Locator();
//}

//private async void Locator()
//{
//    try
//    {
//        var locator = CrossGeolocator.Current;
//        locator.DesiredAccuracy = 50;

//        var location = await locator.GetPositionAsync();
//        var position = new Position(location.Latitude, location.Longitude);
//        MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.04)));
//    }
//    catch (Exception ex)
//    {

//        ex.ToString();
//    }

//}