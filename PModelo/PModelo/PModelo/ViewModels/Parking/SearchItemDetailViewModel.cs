using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using PModelo.Classes;
using PModelo.Classes.ApiGoogle;
using PModelo.Models;
using PModelo.Services;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PModelo.ViewModels
{
    public class SearchItemDetailViewModel: INotifyPropertyChanged
    {
        #region Attributes
        public NavigationService navigationService;
        public SettingsService settingsService;
        public DialogService dialogService;
        public ApiService apiService;
        //public NetService netService;
        public GeolocatorService geolocatorService;
        public DataService dataService;
        private bool isEnabled;
        private bool isRunning;
        //PolynomialRegression polynomial;
        public string imageSource;

        #endregion

        #region Properties
        //public AlertItemViewModel Alert { get; set; }
        public double Longitude;
        public double Latitud;

        public string AdminArea { get; set; }
        private string Thoroughfare { get; set; }
        private string Locality { get; set; }
        private string CountryCode { get; set; }
        private string CountryName { get; set; }
        private string PostalCode { get; set; }
        private string SubLocality { get; set; }
        private string SubThoroughfare { get; set; }
        private string SubAdminArea { get; set; }
        private string[] ArrayPostalCodes { get; set; }

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

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
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
            //netService = new NetService();
            geolocatorService = new GeolocatorService();
            dataService = new DataService();
            IsRunning = false;
            IsEnabled = !IsRunning;
            ArrayPostalCodes = new string[15];
            //polynomial = new PolynomialRegression();
            ImageSource = "icon.png";
        }

        #endregion


        #region Methods
        private bool BoolCondition(string PostalCode)
        {
            bool condition = false;
            if (PostalCode.Equals(ArrayPostalCodes[1]) || PostalCode.Equals(ArrayPostalCodes[2]) || PostalCode.Equals(ArrayPostalCodes[3]) || PostalCode.Equals(ArrayPostalCodes[4]))
            {
                condition = true;
            }
            return condition;
        }
        #endregion

        #region Commands
        public ICommand NAlertCommand { get { return new RelayCommand(NewAlert); } }

        private async void NewAlert()
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
                        var respuesta = await dialogService.ShowMessageYesAndNot("Mensaje", "¿Desea activar el Gps para poder ver los estableblecimientos de parqueo?");
                        if (!respuesta)
                        {
                            return;
                        }
                        else
                        {
                            IsRunning = true;
                            IsEnabled = !IsRunning;
                            var resp = settingsService.IsConnected();
                            if (!resp)
                            {
                                IsRunning = false;
                                IsEnabled = !IsRunning;
                                ImageSource = "icon.png";
                            }
                            else
                            {
                                var result = await dialogService.ShowMessageYesAndNot("Confimación", "Estas seguro de enviar la Alerta");
                                if (result)
                                {
                                    Conectarse(user);
                                }
                                else
                                {
                                    IsRunning = false;
                                    IsEnabled = !IsRunning;
                                    ImageSource = "icon.png";
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        var result = await dialogService.ShowMessageYesAndNot("Confimación", "¿Estás seguro de enviar la Alerta?");
                        if (result)
                        {
                            IsRunning = true;
                            IsEnabled = !IsRunning;
                            Conectarse(user);
                        }
                        else
                        {
                            IsRunning = false;
                            IsEnabled = !IsRunning;
                            ImageSource = "icon.png";
                            return;
                        }
                    }
                }
                else
                {
                    IsRunning = false;
                    IsEnabled = !IsRunning;
                    ImageSource = "icon.png";
                    await dialogService.ShowMessage("Confimación", "Es necesario tener acceso a Internet, active el Wifi o su paquete de Datos por favor.");
                    return;
                }
            }
            else
            {
                IsRunning = false;
                IsEnabled = !IsRunning;
                ImageSource = "icon.png";
                await dialogService.ShowMessage("Mensaje", "Es usuario está bloqueado. No podrá enviar alerta hasta que edite su número de teléfono.");
                return;
            }

        }

        private async void Conectarse(User user)
        {
            //if (netService.IsConnected())
            if (CrossConnectivity.Current.IsConnected)
            {
                await geolocatorService.GetLocation();
                Longitude = geolocatorService.Longitude;
                Latitud = geolocatorService.Latitud;

                AdminArea = geolocatorService.AdminArea;
                Thoroughfare = geolocatorService.Thoroughfare;
                Locality = geolocatorService.Locality;
                CountryCode = geolocatorService.CountryCode;
                CountryName = geolocatorService.CountryName;
                PostalCode = geolocatorService.PostalCode;
                SubLocality = geolocatorService.SubLocality;
                SubThoroughfare = geolocatorService.SubThoroughfare;
                SubAdminArea = geolocatorService.SubAdminArea;

                Response response = null;
                if (user != null)
                {
                    //if (polynomial.PoitnInsideArea(Latitud, Longitude))
                    //{
                        if (Longitude != 0 && Latitud != 0)
                        {
                            var cadena = "https://maps.googleapis.com/maps/api/geocode/";
                            var UrlServerMethod = "json?latlng=" + Latitud + "," + Longitude;
                            var request = await apiService.GetGoogleService<ResponseGoogle>(cadena, UrlServerMethod);

                            if (request != null)
                            {
                                if (request.status.Equals("OK"))
                                {
                                    if (request.results.Count() > 0)
                                    {
                                        string[] caracteres = request.results.FirstOrDefault().formatted_address.Split(',');
                                        Thoroughfare = caracteres[0];
                                    }
                                }
                            }
                        }







                        //var mainViewModel = MainViewModel.GetInstance();
                        //response = await apiService.SendAlert(Longitude, Latitud, user.Phone, user.FirstName, user.LastName, user.Motherslastname, user.Email, user.UserId, Thoroughfare);
                        //if (response != null)
                        //{
                        //    if (response.IsSuccess)
                        //    {
                        //        if (response.Result != null)
                        //        {
                        //            var alert = new Alert
                        //            {
                        //                AlertId = ((AlertModelT)response.Result).IdCaso,
                        //                Latitud = ((AlertModelT)response.Result).PosLatitud,
                        //                Longitud = ((AlertModelT)response.Result).PosLongitud,
                        //                FechaCreacion2 = Utilities.dateMilliToDate(((AlertModelT)response.Result).FechaCreacion.ToString()),
                        //                Emergency = ((AlertModelT)response.Result).TipoCaso.Descripcion,
                        //                AlertSantiagoId = response.VarAuxiliar,
                        //                IdItemTabla = ((AlertModelT)response.Result).TipoCaso.IdItemTabla,
                        //                Address = Thoroughfare
                        //            };

                        //            await dialogService.ShowMessage("Mensaje", "Alerta enviada exitosamente");
                        //            mainViewModel.SetCurrentAlertItem(alert);
                        //            mainViewModel.LoadListAlertsForUser(true);
                        //            dataService.Insert<Alert>(alert);

                        //            await navigationService.Navigate("AlertOptionPage");
                        //        }
                        //        IsRunning = false;
                        //        IsEnabled = !IsRunning;
                        //        ImageSource = "icon.png";
                        //    }
                        //}
                }
                else
                {
                    IsRunning = false;
                    IsEnabled = !IsRunning;
                    ImageSource = "icon.png";
                    await dialogService.ShowMessage("Mensaje", response.Message);
                    return;
                }
            }
            else
            {
                ImageSource = "icon.png";
                await dialogService.ShowMessage("Confimación", "Es necesario tener acceso a Internet, active el Wifi o su paquete de datos por favor.");
                return;
            }

        }
        #endregion


        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
