using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using PModelo.Classes;
using PModelo.Classes.ApiGoogle;
using PModelo.Classes.NoMapping;
using PModelo.Models;
using PModelo.Services;
using PModelo.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PModelo.ViewModels
{
    public class SearchItemDetailViewModel: INotifyPropertyChanged
    {
        #region Attributes
        private NavigationService navigationService;
        private SettingsService settingsService;
        private  DialogService dialogService;
        private ApiService apiService;
        private GeolocatorService geolocatorService;
        private DataService dataService;
        private bool isEnabled;
        private string imageSource;
        private SearchParkForm searchParkForm;
        private bool isBusy;
        private double Longitude;
        private double Latitud;
        #endregion

        #region Properties
        public bool IsBusy
        {
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBusy"));
                }
            }
            get
            {
                return isBusy;
            }
        }

        public string ImageSource
        {
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                }
            }
            get
            {
                return imageSource;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }
        #endregion

        #region Constructors
        public SearchItemDetailViewModel()
        {
            navigationService = new NavigationService();
            settingsService = new SettingsService();
            dialogService = new DialogService();
            apiService = new ApiService();
            geolocatorService = new GeolocatorService();
            dataService = new DataService();
            IsBusy = false;
            IsEnabled = !IsBusy;
            searchParkForm = new SearchParkForm();
            ImageSource = "search.png";
        }

        #endregion


        #region Methods
        private async void Conectarse(User user)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await geolocatorService.GetLocation();
                Longitude = geolocatorService.Longitude;
                Latitud = geolocatorService.Latitud;

                if (Longitude != 0 && Latitud != 0)
                {
                    searchParkForm.Longitude = Longitude.ToString();
                    searchParkForm.Latitud = Latitud.ToString();

                    var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
                    if (isReachable)
                    {
                        var sear = new SearchParkForm {
                            Latitud= searchParkForm.Latitud,
                            Longitude= searchParkForm.Longitude
                        };

                        var respuesta = await apiService.Post<SearchParkForm, ResponseT<List<Parqueadero>>>(Configuration.SERVER, "/api", "/Parking/SearchParking", user.TokenType, user.AccessToken, sear);
                        if (respuesta != null)
                        {
                            if (respuesta.IsSuccess)
                            {
                                var result = (ResponseT<List<Parqueadero>>)respuesta.Resullt;
                                var parqExits=dataService.Get<Parqueadero>(false).ToList();

                                if (result.IsSuccess)
                                {
                                    var listParqueaderos = (List<Parqueadero>)result.Result;
                                    ReloadParqueaderos(listParqueaderos);
                                    await navigationService.Navigate("MapUbicateParkingPage");
                                    //foreach (var iParq in parqExits)
                                    //{
                                    //    dataService.Delete<Parqueadero>(iParq);
                                    //}
                                    //foreach (var iParqueadero in listParqueaderos)
                                    //{
                                    //    dataService.DeleteAllAndInsert<Parqueadero>(iParqueadero);
                                    //}
                                    IsBusy = false;
                                    IsEnabled = !IsBusy;
                                }
                                else
                                {
                                    IsBusy = false;
                                    IsEnabled = !IsBusy;
                                    await dialogService.ShowMessage("Mensaje", result.Message);
                                    return;
                                }
                            }
                            else {
                                IsBusy = false;
                                IsEnabled = !IsBusy;

                                await dialogService.ShowMessage("Mensaje", "Servicio no encontrado");
                                return;
                            }
                        }
                    }
                    else
                    {
                        IsBusy = false;
                        IsEnabled = !IsBusy;
                        await dialogService.ShowMessage("Mensaje", "Es necesario tener conexión a internet para poder registrarse");
                        return;
                    }
                }
                else
                {
                    IsBusy = false;
                    IsEnabled = !IsBusy;
                    await dialogService.ShowMessage("Mensaje", "Es necesario tener encendido el Gps para poder ubicar los sitios más cercanos");
                    return;
                }
            }
            else
            {
                IsBusy = false;
                IsEnabled = !IsBusy;
                await dialogService.ShowMessage("Mensaje", "Active su Wifi o su paquete de datos.");
                return;
            }
        }

        private void ReloadParqueaderos(List<Parqueadero> listParqueaderos)
        {
            if (listParqueaderos!=null) {
                var mainViewModel = MainViewModel.GetInstance();

                mainViewModel.Parqueaderos.Clear();
                foreach (var itemP in listParqueaderos)
                {
                    mainViewModel.Parqueaderos.Add(new ParqueaderoItemViewModel
                    {
                        Capacidad = itemP.Capacidad,
                        Direccion = itemP.Direccion,
                        Fecha_Creacion = itemP.Fecha_Creacion,
                        Id_Parqueadero = itemP.Id_Parqueadero,
                        Latitud = itemP.Latitud,
                        Longitud = itemP.Longitud,
                        Nombre = itemP.Nombre,
                        Telefono_Fijo = itemP.Telefono_Fijo,
                        Telefono_Movil = itemP.Telefono_Movil,
                        Id_Tipo_Parking = itemP.Id_Tipo_Parking,
                        Estado = itemP.Estado,
                        Plazas_Disponibles=itemP.Plazas_Disponibles,
                        Plazas_Ocupadas=itemP.Plazas_Ocupadas
                    });
                }
            }
        }

        #endregion

        #region Commands
        public ICommand NAlertCommand { get { return new RelayCommand(NewSeachParking); } }


        private async void NewSeachParking()
        {
            ImageSource = "search.png";
            var user = dataService.First<User>(false);
            user.Persona = dataService.First<Persona>(false);

            if (user != null)
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = settingsService.IsPermited();

                    if (!response)
                    {
                        var respuesta = await dialogService.ShowMessageYesAndNot("Mensaje", "¿Desea activar el Gps para poder ver los estableblecimientos de parqueo cercanos?");
                        if (!respuesta)
                        {
                            return;
                        }
                        else
                        {
                            IsBusy = true;
                            IsEnabled = !IsBusy;
                            var resp = settingsService.IsConnected();
                            if (!resp)
                            {
                                IsBusy = false;
                                IsEnabled = !IsBusy;
                                ImageSource = "search.png";
                            }
                            else
                            {
                                var result = await dialogService.ShowMessageYesAndNot("Confimación", "¿Estás seguro de realizar la búsqueda?");
                                if (result)
                                {
                                    Conectarse(user);
                                }
                                else
                                {
                                    IsBusy = false;
                                    IsEnabled = !IsBusy;
                                    ImageSource = "search.png";
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        var result = await dialogService.ShowMessageYesAndNot("Confimación", "¿Estás seguro de realizar la búsqueda?");
                        if (result)
                        {
                            IsBusy = true;
                            IsEnabled = !IsBusy;
                            Conectarse(user);
                        }
                        else
                        {
                            IsBusy = false;
                            IsEnabled = !IsBusy;
                            ImageSource = "search.png";
                            return;
                        }
                    }
                }
                else
                {
                    IsBusy = false;
                    IsEnabled = !IsBusy;
                    ImageSource = "search.png";
                    await dialogService.ShowMessage("Confimación", "Es necesario tener acceso a Internet, active el Wifi o su paquete de Datos por favor.");
                    return;
                }
            }
            else
            {
                IsBusy = false;
                IsEnabled = !IsBusy;
                ImageSource = "search.png";
                await dialogService.ShowMessage("Mensaje", "Llamada inválida.");
                return;
            }

        }

    
        #endregion

        
        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
