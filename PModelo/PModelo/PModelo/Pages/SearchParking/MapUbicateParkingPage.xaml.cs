
using Plugin.Geolocator;
using PModelo.Services;
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
       

        public  MapUbicateParkingPage ()
		{
                InitializeComponent();
                NavigationService navigationService = new NavigationService();
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.SetGeolocationParqueaderos(
                mainViewModel.Parqueaderos);

                foreach (Pin item in mainViewModel.Pins)
                {
                     item.Clicked +=async (sender, args) =>
                     {
                         var _pin = sender as Pin;
                         var _model =(ParqueaderoItemViewModel)_pin.BindingContext;
                         mainViewModel.LoadparqueaderoSeleccionado(_model);
                         mainViewModel.SetGeolocation(_model.Latitud??0,_model.Longitud??0,_model.Nombre,_model.Direccion);
                         await navigationService.Navigate("ParqueaderoDetailPage");
                     };
                    MyMap.Pins.Add(item);
                }
            Locator();
        }

        private async void Locator()
        {
                try
                {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var location = await locator.GetPositionAsync();
                var position = new Position(location.Latitude, location.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.1)));
            }
            catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }
    }



//MyMap.+= (sender, args) =>
//{
//    var pin = args.SelectedItem as Pin;
//    var details = PinMap[pin];
//    var page = new DetailPage<T>(details);
//    Navigation.PushAsync(page);
//};



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