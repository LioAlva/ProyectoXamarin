using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using PModelo.Classes;
using PModelo.Classes.ApiGoogle;
using PModelo.Classes.NoMapping;
using PModelo.Models;
using PModelo.Services;
using PModelo.Util;
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
            ImageSource = "icon.png";
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
                    searchParkForm.Longitude = Longitude;
                    searchParkForm.Latitud = Latitud;

                    var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
                    if (isReachable)
                    {

                        var respuesta = await apiService.Post<SearchParkForm, Response>(Configuration.SERVER, "/api", "/Parking/SearchParking", user.TokenType, user.AccessToken, searchParkForm);
                        if (respuesta != null)
                        {
                            if (respuesta.IsSuccess)
                            {
                                var result = (Response)respuesta.Result;
                                IsBusy = false;
                                IsEnabled = !IsBusy;

                                if (result.IsSuccess)
                                {
                                    await dialogService.ShowMessage("Confirmación", result.Message);
                                    await navigationService.Navigate("MainPage");

                                }
                                else
                                {
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

        #endregion

        #region Commands
        public ICommand NAlertCommand { get { return new RelayCommand(NewSeachParking); } }


        private async void NewSeachParking()
        {
            ImageSource = "icon.png";
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
                                ImageSource = "icon.png";
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
                                    ImageSource = "icon.png";
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
                            ImageSource = "icon.png";
                            return;
                        }
                    }
                }
                else
                {
                    IsBusy = false;
                    IsEnabled = !IsBusy;
                    ImageSource = "icon.png";
                    await dialogService.ShowMessage("Confimación", "Es necesario tener acceso a Internet, active el Wifi o su paquete de Datos por favor.");
                    return;
                }
            }
            else
            {
                IsBusy = false;
                IsEnabled = !IsBusy;
                ImageSource = "icon.png";
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
