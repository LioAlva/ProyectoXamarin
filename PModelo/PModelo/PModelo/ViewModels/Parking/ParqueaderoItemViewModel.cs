using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using Plugin.Media.Abstractions;
using PModelo.Classes;
using PModelo.Classes.NoMapping;
using PModelo.Models;
using PModelo.Services;
using PModelo.Util;
using Syncfusion.SfBusyIndicator.XForms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PModelo.ViewModels
{
    public class ParqueaderoItemViewModel: Parqueadero, INotifyPropertyChanged
    {
        #region Attributes
        //public NetService netService;
        public DataService dataService;
        public ApiService apiService;
        public DialogService dialogService;
        public NavigationService navigationService;
        public GeolocatorService geolocatorService;
        //private bool isRunning;
        private bool isBusy;
        private bool isEnabled;
        SfBusyIndicator busyIndicator;
        //private List<string> _color;
        //private List<TypeUser> _typeUsers { get; set; }
        //MediaFile file;
        private bool isRefreshingPlaces;
        private bool isRewelcome;
        private ImageSource _imageSource;
        //List<BreakfastMenu> breakfastMenuList;
        #endregion

        #region Properties
        public string NombreTipoParqueadero { get; set; }

        //BreakfastMenuList = new ObservableCollection<BreakfastMenu>();

        //public ObservableCollection<BreakfastMenu> BreakfastMenuList
        //{
        //    set
        //    {
        //        if (breakfastMenuList != value)
        //        {
        //            breakfastMenuList = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BreakfastMenuList"));
        //        }
        //    }
        //    get
        //    {
        //        return breakfastMenuList;
        //    }
        //}

        public bool IsRefreshingPlaces
        {
            set
            {
                if (isRefreshingPlaces != value)
                {
                    isRefreshingPlaces = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshingPlaces"));
                }
            }
            get
            {
                return isRefreshingPlaces;
            }
        }
        public bool IsRewelcome
        {
            set
            {
                if (isRewelcome != value)
                {
                    isRewelcome = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRewelcome"));
                }
            }
            get
            {
                return isRewelcome;
            }
        }

        public ImageSource ImageSource
        {
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ImageSource)));
                }
            }
            get
            {
                return _imageSource;
            }
        }


        public SfBusyIndicator BusyIndicator
        {
            set
            {
                if (busyIndicator != value)
                {
                    busyIndicator = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BusyIndicator"));
                }
            }
            get
            {
                return busyIndicator;
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

        #region Contructor
        public ParqueaderoItemViewModel()
        {
            //ImageSource = "icon";
            dialogService = new DialogService();
            navigationService = new NavigationService();
            apiService = new ApiService();
            dataService = new DataService();
            //netService = new NetService();
            //TypeUsers = new List<TypeUser>();
            //LoadTypeUsers();

            isBusy = false;
            isEnabled = true;

            //SeePlaces();
            ///AQQUIIII III
            BreakfastMenuList = new ObservableCollection<BreakfastMenu>();
            MenuTappedCommand = new Command(async () => await MenuSelectedAsync());
        }
        #endregion

        //SeePlacesCommand


        #region Commands
        /*
                     RefreshCommand="{Binding RefreshPlaceListCommand}"
                              IsRefreshing="{Binding IsRefreshingPlaces, Mode=TwoWay}"

             RefreshCommand="{Binding RefreshPlaceListCommand}"
                                  IsRefreshing="{Binding IsRefreshingPlaces, Mode=TwoWay}"
                                 
         */
        public ICommand ParkingPlacesStatusCommand { get { return new RelayCommand(ParkingPlacesStatus); } }

        public async void ParkingPlacesStatus()
        {
            //var idEspacio = Id_Parqueadero;

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (isReachable)
            {
                var currentUser = dataService.First<User>(false);
                if (currentUser != null)
                {
                    if (currentUser.UserId > 0 && !string.IsNullOrEmpty(currentUser.AccessToken))
                    {
                        var registerReserveForm = new ReserveForm()
                        {
                            Id_Parqueadero = Id_Parqueadero,//SOLO
                            //Fecha_Hora_Fin = FechaFin + HoraFin,
                            //Fecha_Hora_Inicio = FechaInicio + HoraInicio,
                            Id_Cliente = currentUser.UserId,
                            //Id_Espacio = Id_Espacio,
                            Latitud = Latitud,
                            Longitud = Longitud,
                            UserTypeId = currentUser.UserTypeId
                        };

                        var response = await apiService.Post<ReserveForm, ResponseT<Reserva>>(Configuration.SERVER, "/api", "/Reserva/ReserveEspace", currentUser.TokenType, currentUser.AccessToken, registerReserveForm);

                        if (response != null)
                        {
                            var result = (ResponseT<Reserva>)response.Resullt;
                            //isBusy = false;
                            //IsEnabled = !isBusy;
                            if (result != null)
                            {
                                if (result.IsSuccess)
                                {
                                    //breakfastMenuList.Clear();
                                    //ObservableCollection<BreakfastMenu> PlacesMenuList = new ObservableCollection<BreakfastMenu>();
                                    //PlacesMenuList.Clear();
                                    //foreach (var item in resultList.Result.Where(x => x.Ocupado.Equals("D")))
                                    //{
                                    //    PlacesMenuList.Add(new BreakfastMenu
                                    //    {
                                    //        ImageSource = item.Descripcion,
                                    //        MenuTitle = item.Descripcion
                                    //    });
                                    //}
                                    //BreakfastMenuList = PlacesMenuList;
                                    await dialogService.ShowMessage("Mensaje", result.Message);

                                }
                                else
                                {
                                    await dialogService.ShowMessage("Mensaje", result.Message);
                                    return;
                                }
                            }
                            else
                            {
                                await dialogService.ShowMessage("Mensaje", "En estos momentos tenemos inconvenientes , por favor intentelo mas tarde.");
                            }

                        }
                    }
                    else
                    {
                        await dialogService.ShowMessage("Mensaje", "Su sesión a caducado, por favor vuelva a ingresar con sus credenciales.");
                        //var app = App.GetInstance();
                        //app.CargarMain();
                    }
                }
                else
                {
                    await dialogService.ShowMessage("Mensaje", "Usuario no encontrado.");
                    return;
                }
            }
            else
            {
                //isBusy = false;
                //IsEnabled = !isBusy;
                await dialogService.ShowMessage("Mensaje", "Es necesario tener conexión a internet para poder registrarse");
                return;
            }
        }



        public ICommand RefreshPlaceListCommand { get { return new RelayCommand(RefreshPlaceList); } }

        public void RefreshPlaceList()
        {
            IsRefreshingPlaces = true;
            SeePlaces();
            IsRefreshingPlaces = false;
        }


        public ICommand SeePlacesCommand { get { return new RelayCommand(SeePlaces); } }

        public async void SeePlaces()
        {
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (isReachable)
            {
                var currentUser = dataService.First<User>(false);
                if (currentUser != null)
                {
                    if (!string.IsNullOrEmpty(currentUser.AccessToken))//!=null
                    {
                        var searchPlacesForm = new SearchPlacesForm()
                        {
                            Id_Parqueadero= Id_Parqueadero,//SOLO
                            UserTypeId= currentUser.UserTypeId         
                        };

                        var response = await apiService.Post<SearchPlacesForm, ResponseT<List<Espacio>>>(Configuration.SERVER, "/api", "/Parking/GetPlacesForIdParque", currentUser.TokenType, currentUser.AccessToken , searchPlacesForm);

                        if (response != null)
                        {
                            var resultList = (ResponseT<List<Espacio>>)response.Resullt;
                            isBusy = false;
                            IsEnabled = !isBusy;
                            if (resultList!=null) {
                                if (resultList.IsSuccess)
                                {
                                    breakfastMenuList.Clear();
                                    ObservableCollection<BreakfastMenu> PlacesMenuList = new ObservableCollection<BreakfastMenu>();
                                    PlacesMenuList.Clear();
                                    foreach (var item in resultList.Result.Where(x => x.Ocupado.Equals("D")))
                                    {
                                        PlacesMenuList.Add(new BreakfastMenu
                                        {
                                            ImageSource = item.Descripcion,
                                            MenuTitle = item.Descripcion,
                                            Id_Espacio = item.Id_Espacio

                                        });
                                    }
                                    BreakfastMenuList = PlacesMenuList;


                                    //await dialogService.ShowMessage("Confirmación", result.Message);
                                    //var plas = new BreakfastMenu();
                                    //var placesViewModel = new PlacesViewModel(null);
                                    //var mainViewModel = MainViewModel.GetInstance();
                                    //var placess = mainViewModel.Places.BreakfastMenuList;
                                    //foreach (var item in resultList.Result)
                                    //{
                                    //    placess.Add(new BreakfastMenu {
                                    //        ImageSource = item.Descripcion,
                                    //        MenuTitle=item.Descripcion
                                    //    });
                                    //}


                                    //await navigationService.Navigate("PlacesPage");
                                }
                                else
                                {
                                    await dialogService.ShowMessage("Mensaje", resultList.Message);
                                    return;
                                }
                            }
                            else
                            {
                                await dialogService.ShowMessage("Sistema", "En estos momentos no hay servicio, intentelo mas tarde");
                                return;
                            }
                        }
                    }
                    else
                    {
                        await dialogService.ShowMessage("Mensaje", "Su sesión a caducado, por favor vuelva a ingresar con sus credenciales.");
                        //var app = App.GetInstance();
                        //app.CargarMain();
                    }
                }
                else
                {
                    await dialogService.ShowMessage("Mensaje", "Usuario no encontrado.");
                    return;
                }
            }
            else
            {
                isBusy = false;
                IsEnabled = !isBusy;
                await dialogService.ShowMessage("Mensaje", "Es necesario tener conexión a internet para poder registrarse");
                return;
            }
        }

        public ICommand CancelCommand { get { return new RelayCommand(Cancel); } }

        private void Cancel()
        {
            var user = new User();

            //navigationService.SetMainPage("LoginPage", user);
            navigationService.SetMainPage("NewLoginPage");

        }

        #endregion

        #region AQUIIII VER

        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<BreakfastMenu> breakfastMenuList;
        private BreakfastMenu selectedBreakfastMenu;

        public ObservableCollection<BreakfastMenu> BreakfastMenuList
        {
            get => breakfastMenuList;
            set => SetObservableProperty(ref breakfastMenuList, value);
        }

        public BreakfastMenu SelectedBreakfastMenu
        {
            get => selectedBreakfastMenu;
            set => SetObservableProperty(ref selectedBreakfastMenu, value);
        }

        public ICommand MenuTappedCommand { get; set; }

        //public PlacesViewModel()
        //{
        //    BreakfastMenuList = new ObservableCollection<BreakfastMenu>();
        //    MenuTappedCommand = new Command(async () => await MenuSelectedAsync());
        //}

        private async Task MenuSelectedAsync()
        {
            //Reserva
            var mainViewModel = MainViewModel.GetInstance();
            var reserva = new Reserva
            {
                Nombre_Espacio = SelectedBreakfastMenu.MenuTitle,
                Nombre_Parqueadero = Nombre,
                Nombre_Tipo_Parqueadero = NombreTipoParqueadero,
                Id_Parqueadero = Id_Parqueadero,
                Id_Espacio = SelectedBreakfastMenu.Id_Espacio
            };

            mainViewModel.ReservaIdentity.ReloadReservaIdentity(reserva);

            await navigationService.Navigate("ReservaTimePickerPage");
            //switch (SelectedBreakfastMenu.MenuTitle)
            //{
            //    case "BURGER":
            //        await navigation.PushModalAsync(new ProbaPage());
            //        break;
            //    case "PIZZA":
            //        //await navigation.PushModalAsync(new ProbaPage());
            //        break;
            //    case "BACON":
            //        //await navigation.PushModalAsync(new ProbaPage());
            //        break;
            //    case "SANDWICH":
            //        //await navigation.PushModalAsync(new ProbaPage());
            //        break;
            //}
        }

        protected void SetObservableProperty<T>(ref T field, T value,
        [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    

    #endregion


    //#region Event

    //public event PropertyChangedEventHandler PropertyChanged;

    //    #endregion
    }
}


