﻿using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using PModelo.Classes;
using PModelo.Classes.NoMapping;
using PModelo.Helper;
using PModelo.Helpers;
using PModelo.Models;
using PModelo.Services;
using PModelo.Util;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms.Maps;

namespace PModelo.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        #region Attributes
        private string _titlePage;
        public DataService dataService;
        public ApiService apiService;
        public DialogService dialogService;
        public NavigationService navigationService;
        public GeolocatorService geolocatorService;

        CierreVentaItemViewModel cierreVenta;
        private bool isRefreshingParkings;
        #endregion

        #region Properties

        public bool IsRefreshingParkings
        {
            set
            {
                if (isRefreshingParkings != value)
                {
                    isRefreshingParkings = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshingParkings"));
                }
            }
            get
            {
                return isRefreshingParkings;
            }
        }


        public string TitlePage
        {
            set
            {
                if (_titlePage != value)
                {
                    _titlePage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitlePage"));
                }
            }
            get
            {
                return _titlePage;
            }
        }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<OrderViewModel> Orders { get; set; }

        public LoginViewModel Login { get; set; }

        public ObservableCollection<ContactItemViewModel> ContactList { get; set; }

        public ObservableCollection<OrderInfoItemViewModel> OrderInfoCollection { get; set; }

        public ObservableCollection<ChartDataPoint> DataPoints { get; set; }

        public NewUserViewModel NewUser { get; set; }//EditUser

        public UserSistemViewModel UserLoged { get; set; }
        //UserLoged
        public PlacesViewModel Places { get; set; }

        public ReservaViewModel ReservaIdentity { get; set; }

        SfChart chart { get; set; }

        public List<Person> Data { get; set; }

        public List<Person> Data2 { get; set; }

        public CierreVentaItemViewModel CierreVenta
        {
            get { return this.cierreVenta; }
            set { this.cierreVenta = value; }
        }
       private ObservableCollection<ParqueaderoItemViewModel> parqueaderos;
        public ObservableCollection<UsuarioItemViewModel> Usuarios { get; set; }
        /*
         * user type monkeys**/
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Grouping<string, User>> UsersGrouped { get; set; }
        //public ObservableCollection<User> Users { get; set; }
        public int UserCount => Users.Count;
        /**end User type Monkey**/

        /***TODO EL NEGOCIO**/
        public SearchItemDetailViewModel SearchParking { get; set; }
        public ParkItemViewModel ParkingRegister { get; set; }
        public ObservableCollection<Pin> Pins { get; set; }
        public ObservableCollection<Pin> Pins2 { get; set; }
        public Position PositionsSearch { get; set; }

        public Position PositionsState { get; set; }

        //public ObservableCollection<ParqueaderoItemViewModel> Parqueaderos { get; set; }
        public ParqueaderoItemViewModel ParqueaderoSelected { get; set; }
        public ObservableCollection<ParqueaderoItemViewModel> Parqueaderos
        {
            set
            {
                if (parqueaderos != value)
                {
                    parqueaderos = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Parqueaderos"));
                }
            }
            get
            {
                return parqueaderos;
            }
        }


        public void LoadUser(User user)
        {
            user.Picture = user.Picture;

            if (user.UserTypeId==4)
            {
                UserLoged.FirstName = user.Persona.Apellido_Paterno;
                UserLoged.LastName = user.Persona.Apellido_Materno;
            }
            else {
                UserLoged.FirstName = user.FirstName;
                UserLoged.LastName = user.LastName;
            }
        }

        /**NEGOCIO*/
        #endregion

        #region Singleton

        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }

        #endregion

        public MainViewModel()
        {
            instance = this;
            chart = new SfChart();
            navigationService = new NavigationService();
            Login = new LoginViewModel();
            ContactList = new ObservableCollection<ContactItemViewModel>();
            OrderInfoCollection = new ObservableCollection<OrderInfoItemViewModel>();
            DataPoints = new ObservableCollection<ChartDataPoint>();
            //CierreVenta = new CierreVentaItemViewModel();
            this.cierreVenta = new CierreVentaItemViewModel();
            Usuarios = new ObservableCollection<UsuarioItemViewModel>();
            NewUser = new NewUserViewModel(); 

            Data = new List<Person>();
            Data2 = new List<Person>();
            //INavigation navigation=new INavigation();
            Places = new PlacesViewModel();
            ReservaIdentity = new ReservaViewModel();
            //LoadMenu();
            LoadData();
            LoadContact();
            LoadOrderInfo();
            LoadUsuarios();
            LoadDataPoint();

            /*monkeys**/
            Monkeys = MonkeyHelper.Monkeys;
            MonkeysGrouped = MonkeyHelper.MonkeysGrouped;
            Zoos = new ObservableCollection<Zoo>
            {
                new Zoo
                {
                    ImageUrl = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/23c1dd13-333a-459e-9e23-c3784e7cb434/2016-06-02_1049.png",
                    Name = "Woodland Park Zoo"
                },
                 new Zoo
                {
                    ImageUrl =    "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png",
                    Name = "Cleveland Zoo"
                 },
                new Zoo
                {
                    ImageUrl = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/e8179889-8189-4acb-bac5-812611199a03/2016-06-02_1053.png",
                    Name = "Phoenix Zoo"
                }
            };
            /***/

            /*monkeys**/
            Users = UserHelper.Users;
            UsersGrouped = UserHelper.UsersGrouped;
            Users = new ObservableCollection<User>
            {
                //new User
                //{
                //    Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/23c1dd13-333a-459e-9e23-c3784e7cb434/2016-06-02_1049.png",
                //    FirstName = "Woodland Park Zoo"
                //},
                // new User
                //{
                //    Picture =    "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png",
                //    FirstName= "Cleveland Zoo"
                // },
                //new User
                //{
                //    Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/e8179889-8189-4acb-bac5-812611199a03/2016-06-02_1053.png",
                //    FirstName = "Phoenix Zoo"
                //}


           new User { FirstName= "Aaron",LastName="Alvarez",MotherLastName = "Bartra",DNI="11200033", Cargo="Jefe",Email="AronAlvarez@hotmail.com",Picture="http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/23c1dd13-333a-459e-9e23-c3784e7cb434/2016-06-02_1049.png"},
           new User { FirstName = "Luis", LastName = "Alvarez", MotherLastName = "Caballero", DNI = "11200666", Cargo = "Jefe", Email = "Luisperso_015@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" },
           new User { FirstName = "Juan Ramon", LastName = "Riveiro", MotherLastName = "Carrillo", DNI = "099200033", Cargo = "Rol0", Email = "JuanRamon@hotmail.com" ,Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png"},
           new User { FirstName = "Ruben Dario", LastName = "Cuba", MotherLastName = "Postmodernismo", DNI = "11200055", Cargo = "Rol0", Email = "RubenDario@hotmail.com" },
           new User { FirstName = "Juan Ramon", LastName = "Riveiro", MotherLastName = "Carrillo", DNI = "099200033", Cargo = "Rol0", Email = "JuanRamon@hotmail.com" },
           new User { FirstName = "Aaron", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Jefe", Email = "AronAlvarez@hotmail.com" ,Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png"},
           new User { FirstName = "Ruben Dario", LastName = "Cuba", MotherLastName = "Postmodernismo", DNI = "11200055", Cargo = "Rol0", Email = "RubenDario@hotmail.com" },
           new User { FirstName = "Zaroastro", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol1", Email = "AronAlvarez@hotmail.com" ,Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png"},
           new User { FirstName = "Jhon Lenon", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol2", Email = "AronAlvarez@hotmail.com" },
           new User { FirstName = "Jose Maria", LastName = "Euguren", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol3", Email = "AronAlvarez@hotmail.com" ,Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png"},
        };
            /***/


            /**Nuestro negocio**/
            SearchParking = new SearchItemDetailViewModel();
            ParkingRegister = new ParkItemViewModel();
            Pins = new ObservableCollection<Pin>();
            PositionsSearch = new Position();
            PositionsState = new Position();
            Parqueaderos = new ObservableCollection<ParqueaderoItemViewModel>();
            UserLoged = new UserSistemViewModel();
            ParqueaderoSelected = new ParqueaderoItemViewModel();
            Pins2 = new ObservableCollection<Pin>();
            dataService = new DataService();
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            apiService = new ApiService();
            //Parqueaderos = new ObservableCollection<Parqueadero>();
            //LoadparqueaderoSeleccionado();
            // LoadParqueaderosBuscados();
            /***/
        }

        public async void ListParkingForUser()
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                var currentUser = dataService.First<User>(false);
                var currentPersona = dataService.First<Persona>(false);

                if (currentUser != null)
                {
                    if (currentUser.UserId > 0 && currentPersona.Id_Persona > 0 && !string.IsNullOrEmpty(currentUser.AccessToken))
                    {
                        var searchPakingsForm = new SearchPakingsForm
                        {
                            UserId = currentUser.UserId,
                            UserTypeId = 4
                        };

                        var respuesta = await apiService.Post<SearchPakingsForm, ResponseT<List<Parqueadero>>>(Configuration.SERVER, "/api", "/Reserva/GetParkingWithReserveForIdUser", currentUser.TokenType, currentUser.AccessToken, searchPakingsForm);
                        if (respuesta != null)
                        {
                            if (respuesta.IsSuccess)
                            {
                                var result = (ResponseT<List<Parqueadero>>)respuesta.Resullt;
                                //var parqExits = dataService.Get<Parqueadero>(false).ToList();

                                if (result.IsSuccess)
                                {
                                    var listParqueaderos = (List<Parqueadero>)result.Result;
                                    Parqueaderos = UtilitiesReload.ReloadParqueaderosZS(listParqueaderos);

                                    //Utilities. ReloadParqueaderos(listParqueaderos);
                                    //await navigationService.Navigate("AdminParkingReservePage");
                                    //foreach (var iParq in parqExits)
                                    //{
                                    //    dataService.Delete<Parqueadero>(iParq);
                                    //}
                                    //foreach (var iParqueadero in listParqueaderos)
                                    //{
                                    //    dataService.DeleteAllAndInsert<Parqueadero>(iParqueadero);
                                    //}

                                }
                                else
                                {

                                    await dialogService.ShowMessage("Mensaje", result.Message);

                                }
                            }
                            else
                            {

                                await dialogService.ShowMessage("Mensaje", "Servicio no encontrado");

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
                    await dialogService.ShowMessage("Mensaje", "En estos momentos tenemos inconveniente, intente la busqueda mas tarde.");
                   
                }
            }
            else
            {
                await dialogService.ShowMessage("Mensaje", "Active su Wifi o su paquete de datos.");

            }
        }


        public void LoadparqueaderoSeleccionado(ParqueaderoItemViewModel parqueaderoSelected)
        {
            ParqueaderoSelected = parqueaderoSelected;//modificacion
            parqueaderoSelected.NombreTipoParqueadero =parqueaderoSelected.Id_Tipo_Parking==1 ? "Privado" : "Público";
        }
        
        //public void LoadNewUserWhite()
        //{
        //    NewUser.DNI = string.Empty;
        //    NewUser.Email = string.Empty;
        //    NewUser.FirstName = string.Empty;
        //    NewUser.LastName = string.Empty;
        //    NewUser.Motherslastname = string.Empty;
        //    NewUser.Password = string.Empty;
        //    NewUser.PasswordConfirm = string.Empty;
        //    NewUser.Phone = string.Empty;
        //}


        private void LoadDataPoint()
        {

            DataPoints.Add(new ChartDataPoint(64, 14.4, 20));
            DataPoints.Add(new ChartDataPoint(71, 2, 15));
            DataPoints.Add(new ChartDataPoint(74, 7, 30));
            DataPoints.Add(new ChartDataPoint(80, 4, 22));
            DataPoints.Add(new ChartDataPoint(94, 1, 8));
            DataPoints.Add(new ChartDataPoint(96, 6, 18));
            DataPoints.Add(new ChartDataPoint(98, 12.3, 28));

            //SplineSeries splineSeries = new SplineSeries()
            //{
            //    ItemsSource = DataPoints,
            //    XBindingPath = "Month",
            //    YBindingPath = "Value",
            //    SplineType = SplineType.Cardinal
            //};
            //chart.Series.Add(splineSeries);

            Data = new List<Person>()
            {
                new Person { Name = "David", Height = 180 },
                new Person { Name = "Michael", Height = 170 },
                new Person { Name = "Steve", Height = 160 },
                new Person { Name = "Joel", Height = 182 },

                new Person { Name = "David", Height = 100 },
                new Person { Name = "Michael", Height = 110 },
                new Person { Name = "Steve", Height = 40 },
                new Person { Name = "Joel", Height = 12 },

                new Person { Name = "David", Height = 50 },
                new Person { Name = "Michael", Height = 150 },
                new Person { Name = "Steve", Height = 100 },
                new Person { Name = "Joel", Height = 60 }
            };


            Data2 = new List<Person>()
            {
                new Person { Name = "David", Height = 10 },
                new Person { Name = "Michael", Height = 154 },
                new Person { Name = "Steve", Height = 30 },
                new Person { Name = "Joel", Height = 15 }
            };

            //CategoryAxis primaryAxis = new CategoryAxis();

            //    primaryAxis.Title.Text = "Name";

            //    chart.PrimaryAxis = primaryAxis;

            //    //Initializing secondary Axis
            //    NumericalAxis secondaryAxis = new NumericalAxis();

            //    secondaryAxis.Title.Text = "Height (in cm)";

            //    chart.SecondaryAxis = secondaryAxis;

            //    //Initializing column series
            //    ColumnSeries series = new ColumnSeries();

            //    series.SetBinding(ChartSeries.ItemsSourceProperty, "Data");

            //    series.XBindingPath = "Name";

            //    series.YBindingPath = "Height";

            //    chart.Series.Add(series);

            //Initializing primary axis
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title.Text = "Name";
            chart.PrimaryAxis = primaryAxis;

            //Initializing secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title.Text = "Height (in cm)";
            chart.SecondaryAxis = secondaryAxis;

            //Initializing column series
            //ColumnSeries series = new ColumnSeries();
            //series.ItemsSource = Data;
            //series.XBindingPath = "Name";
            //series.YBindingPath = "Height";
            //series.Label = "Heights";

            //ColumnSeries series = new ColumnSeries();
            //series.ItemsSource = Data;
            //series.XBindingPath = "Name";
            //series.YBindingPath = "Height";
            //series.Label = "Heights";

            SplineSeries splineSeri = new SplineSeries()
            {
                ItemsSource = Data,
                XBindingPath = "Name",
                YBindingPath = "Height",
                SplineType = SplineType.Monotonic,
                DataMarker = new ChartDataMarker(),
            };

            SplineSeries splineSeries = new SplineSeries()
            {
                ItemsSource = Data,
                //XBindingPath = "Month",
                //YBindingPath = "Value",
                XBindingPath = "Name",
                YBindingPath = "Height",
                SplineType = SplineType.Monotonic,
                DataMarker = new ChartDataMarker(),
            };

            SplineSeries splineSerie = new SplineSeries()
            {
                ItemsSource = Data2,
                //XBindingPath = "Month",
                //YBindingPath = "Value",
                XBindingPath = "Name",
                YBindingPath = "Height",
                SplineType = SplineType.Clamped,
                DataMarker = new ChartDataMarker(),
            };
            chart.Series.Add(splineSeries);
            chart.Series.Add(splineSerie);
            chart.Series.Add(splineSeri);

            //SplineSeries splineSerie = new SplineSeries()
            //{
            //    ItemsSource = Data,
            //    XBindingPath = "Month",
            //    YBindingPath = "Value",
            //    SplineType = SplineType.Cardinal
            //};
            //chart.Series.Add(splineSerie);

            //series.DataMarker = new ChartDataMarker();
            //series.EnableTooltip = true;
            //chart.Legend = new ChartLegend();

            //chart.Series.Add(series);

        }

        private void LoadOrderInfo()
        {
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1001, "Maria Anders", "Germany", "ALFKI", "Berlin"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1002, "Ana Trujillo", "Mexico", "ANATR", "Mexico D.F."));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1003, "Ant Fuller", "Mexico", "ANTON", "Mexico D.F."));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1004, "Thomas Hardy", "UK", "AROUT", "London"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1005, "Tim Adams", "Sweden", "BERGS", "London"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1006, "Hanna Moos", "Germany", "BLAUS", "Mannheim"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1007, "Andrew Fuller", "France", "BLONP", "Strasbourg"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1008, "Martin King", "Spain", "BOLID", "Madrid"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1009, "Lenny Lin", "France", "BONAP", "Marsiella"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1010, "John Carter", "Canada", "BOTTM", "Lenny Lin"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1011, "Laura King", "UK", "AROUT", "London"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1012, "Anne Wilson", "Germany", "BLAUS", "Mannheim"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1013, "Martin King", "France", "BLONP", "Strasbourg"));
            OrderInfoCollection.Add(new OrderInfoItemViewModel(1014, "Gina Irene", "UK", "AROUT", "London"));

        }

        private void LoadContact()
        {
            ContactList.Add(new ContactItemViewModel { Name = "Aaron", Number = 7363750 });
            ContactList.Add(new ContactItemViewModel { Name = "Adam", Number = 7323250 });
            ContactList.Add(new ContactItemViewModel { Name = "Adrian", Number = 7239121 });
            ContactList.Add(new ContactItemViewModel { Name = "Alwin", Number = 2329823 });
            ContactList.Add(new ContactItemViewModel { Name = "Alex", Number = 8013481 });
            ContactList.Add(new ContactItemViewModel { Name = "Alexander", Number = 7872329 });
            ContactList.Add(new ContactItemViewModel { Name = "Barry", Number = 7317750 });
        }


        private void LoadUsuarios()
        {
            Usuarios.Add(new UsuarioItemViewModel { FirstName= "Aaron",LastName="Alvarez",MotherLastName = "Bartra",DNI="11200033", Cargo="Jefe",Email="AronAlvarez@hotmail.com",Picture="home.png"});
            Usuarios.Add(new UsuarioItemViewModel { FirstName= "Luis",LastName="Alvarez",MotherLastName = "Caballero",DNI="11200666", Cargo="Jefe",Email="Luisperso_015@hotmail.com", Picture = "Close.png" });
            Usuarios.Add(new UsuarioItemViewModel { FirstName = "Juan Ramon", LastName = "Riveiro", MotherLastName = "Carrillo", DNI = "099200033", Cargo = "Rol0", Email = "JuanRamon@hotmail.com" });
            Usuarios.Add(new UsuarioItemViewModel { FirstName = "Ruben Dario", LastName = "Cuba", MotherLastName = "Postmodernismo", DNI = "11200055", Cargo = "Rol0", Email = "RubenDario@hotmail.com" });
            Usuarios.Add(new UsuarioItemViewModel { FirstName = "Juan Ramon", LastName = "Riveiro", MotherLastName = "Carrillo", DNI = "099200033", Cargo = "Rol0", Email = "JuanRamon@hotmail.com" });
            Usuarios.Add(new UsuarioItemViewModel { FirstName = "Aaron", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Jefe", Email = "AronAlvarez@hotmail.com" });
            Usuarios.Add(new UsuarioItemViewModel { FirstName = "Ruben Dario", LastName = "Cuba", MotherLastName = "Postmodernismo", DNI = "11200055", Cargo = "Rol0", Email = "RubenDario@hotmail.com" });
            Usuarios.Add(new UsuarioItemViewModel { FirstName = "Zaroastro", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol1", Email = "AronAlvarez@hotmail.com" });
            Usuarios.Add(new UsuarioItemViewModel { FirstName = "Jhon Lenon", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol2", Email = "AronAlvarez@hotmail.com" });
            Usuarios.Add(new UsuarioItemViewModel { FirstName = "Jose Maria", LastName = "Euguren", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol3", Email = "AronAlvarez@hotmail.com" });

        }


        #region Commands


        public ICommand RefreshParkinsListCommand { get { return new RelayCommand(RefreshParkinsList); } }

        public void RefreshParkinsList()
        {
            IsRefreshingParkings = true;
            ListParkingForUser();
            IsRefreshingParkings = false;
        }



        public ICommand GoToCommand { get { return new RelayCommand<string>(GoTo); } }

        private async void GoTo(string pageName)
        {
            await navigationService.Navigate(pageName);
        }

        //public ICommand StartCommand { get { return new RelayCommand(Start); } }

        //private void Start()
        //{
        //    navigationService.SetMainPage(new MasterPage());
        //}



        #endregion


        private void LoadData()
        {
            Orders = new ObservableCollection<OrderViewModel>();

            for (int i = 0; i < 10; i++)
            {
                Orders.Add(new OrderViewModel
                {
                    Title = "Lorem Ipsum",
                    DeliveryDate = DateTime.Today,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                });
            }
        }

        #region Methods

        /***METHODS PARKING**/
        public void SetGeolocationParqueaderos(ObservableCollection<ParqueaderoItemViewModel> parqueaderos)
        {
            Pins.Clear();
            foreach (var ip in parqueaderos)
            {
                PositionsSearch = new Position(ip.Latitud??0, ip.Longitud??0);
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = PositionsSearch,
                    Label = ip.Nombre,
                    Address = ip.Direccion,
                    BindingContext=ip,
                };
                Pins.Add(pin);
            }
        }

        public void SetGeolocation(double latitud, double longitud, string name, string address)
        {
            Pins2.Clear();
            PositionsState = new Position(latitud, longitud);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = PositionsState,
                Label = name,
                Address = address
            };
            Pins2.Add(pin);
        }

        public void GetGeolotation()
        {
            var position1 = new Position(6.2652880, -75.5098530);
            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "Pin1",
                Address = "prueba pin1"
            };
            Pins.Add(pin1);

            var position2 = new Position(6.2652880, -75.4598530);
            var pin2 = new Pin
            {
                Type = PinType.Place,
                Position = position2,
                Label = "Pin2",
                Address = "prueba pin2"
            };
            Pins.Add(pin2);

            var position3 = new Position(6.2652880, -75.4898530);
            var pin3 = new Pin
            {
                Type = PinType.Place,
                Position = position3,
                Label = "Pin3",
                Address = "prueba pin3"
            };
            Pins.Add(pin3);
        }

        /****/

        public void LoadMenu(User currentUser)
        {
            //var currentUser = dataService.First<User>(false);
            Menu = new ObservableCollection<MenuItemViewModel>();

            if (currentUser != null) { }
            switch (currentUser.UserTypeId)
            {
                case 2:
                    Menu.Clear();
                    Menu.Add(new MenuItemViewModel
                    {
                        Icon = "icon.png",
                        PageName = "MainPage",
                        Title = "Buscar Parqueadero"
                    });
                   
                    Menu.Add(new MenuItemViewModel
                    {
                        Icon = "home.png",
                        PageName = "DashboardPage",
                        Title = "Dashboard",
                    });

                    //Menu.Add(new MenuItemViewModel
                    //{
                    //    Icon = "icon.png",
                    //    PageName = "CierreVentasPage",
                    //    Title = "Cierre de Ventas",
                    //});
                    //Menu.Add(new MenuItemViewModel
                    //{
                    //    Icon = "icon.png",
                    //    PageName = "Page2",
                    //    Title = "VER2",
                    //});

                    //Menu.Add(new MenuItemViewModel
                    //{
                    //    Icon = "icon.png",
                    //    PageName = "Page",
                    //    Title = "VER",
                    //});

                 

                    Menu.Add(new MenuItemViewModel
                    {
                        Icon = "icon.png",
                        PageName = "LogutPage",
                        Title = "Cerrar Sesión"
                    });

                    Menu.Add(new MenuItemViewModel
                    {
                        Icon = "home.png",
                        PageName = "MainPage2",
                        Title = "MainPage2",
                    });

                    ; break;
                case 4:
                    Menu.Clear();
                    Menu.Add(new MenuItemViewModel
                    {
                        Icon = "icon.png",
                        PageName = "MainPage",
                        Title = "Mis Parqueaderos",
                    });

                    Menu.Add(new MenuItemViewModel
                    {
                        Icon = "icon.png",
                        PageName = "ParkingPage",
                        Title = "Registrar Parqueadero",
                    });
                    Menu.Add(new MenuItemViewModel
                    {
                        Icon = "home.png",
                        PageName = "DashboardPage",
                        Title = "Dashboard",
                    });

                    Menu.Add(new MenuItemViewModel
                    {
                        Icon = "icon.png",
                        PageName = "LogutPage",
                        Title = "Cerrar Sesión"
                    });


                    ; break;
            }



            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "home.png",
            //    PageName = "Parqueaderos",
            //    Title = "Menu",
            //});

            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "icon.png",
            //    PageName = "Page",
            //    Title = "VER",
            //});


            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "icon.png",
            //    PageName = "UsersGroupPage",
            //    Title = "UsuariosGroupo ",
            //});


            //MapUbicateParkingPage

            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "eventos.png",
            //    PageName = "EventosPage",
            //    Title = "Eventos",
            //});


            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "icon.png",
            //    PageName = "LocatorioPage",
            //    Title = "Usuarios",
            //});

            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "eventos.png",
            //    PageName = "EventoPage",
            //    Title = "Evento",
            //});

            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "icon.png",
            //    PageName = "UsuariosPage",
            //    Title = "Usuarios",
            //});//UsuariosGroupPage


            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "icon.png",
            //    PageName = "UsuariosGroupPage",
            //    Title = "UsuariosGroup",
            //});//UsersGroupPage

            //UsuariosPage
            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "locatarios.png",
            //    PageName = "LocatorioPage",
            //    Title = "Locatorio",
            //});


            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "settings.png",
            //    PageName = "ConfiguracionPage",
            //    Title = "Configuración",
            //});

            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "icon.png",
            //    PageName = "ListViewPage",
            //    Title = "ListViewPage",
            //});
            //Menu.Add(new MenuItemViewModel
            //{
            //    Icon = "icon.png",
            //    PageName = "ReportsPage",
            //    Title = "ReportsPage",
            //});
        }
        //public void LoadMenu(User currentUser)
        //{
        //    //var currentUser = dataService.First<User>(false);
        //    Menu = new ObservableCollection<MenuItemViewModel>();

        //    if (currentUser != null) { }
        //    switch (currentUser.UserTypeId)
        //    {
        //        case 2:

        //            Menu.Add(new MenuItemViewModel
        //            {
        //                Icon = "home.png",
        //                PageName = "DashboardPage",
        //                Title = "Dashboard",
        //            });

        //            //Menu.Add(new MenuItemViewModel
        //            //{
        //            //    Icon = "icon.png",
        //            //    PageName = "CierreVentasPage",
        //            //    Title = "Cierre de Ventas",
        //            //});
        //            Menu.Add(new MenuItemViewModel
        //            {
        //                Icon = "icon.png",
        //                PageName = "Page2",
        //                Title = "VER2",
        //            });

        //            Menu.Add(new MenuItemViewModel
        //            {
        //                Icon = "icon.png",
        //                PageName = "Page",
        //                Title = "VER",
        //            });

        //            Menu.Add(new MenuItemViewModel
        //            {
        //                Icon = "icon.png",
        //                PageName = "SearchParkingPage",
        //                Title = "Buscar Parqueadero"
        //            });

        //            Menu.Add(new MenuItemViewModel
        //            {
        //                Icon = "icon.png",
        //                PageName = "LogutPage",
        //                Title = "Cerrar Sesión"
        //            });


        //            ; break;
        //        case 4:

        //            Menu.Add(new MenuItemViewModel
        //            {
        //                Icon = "icon.png",
        //                PageName = "Page",
        //                Title = "VER",
        //            });

        //            Menu.Add(new MenuItemViewModel
        //            {
        //                Icon = "home.png",
        //                PageName = "DashboardPage",
        //                Title = "Dashboard",
        //            });


        //            Menu.Add(new MenuItemViewModel
        //            {
        //                Icon = "icon.png",
        //                PageName = "ParkingPage",
        //                Title = "Registrar Parqueadero",
        //            });


        //            Menu.Add(new MenuItemViewModel
        //            {
        //                Icon = "icon.png",
        //                PageName = "LogutPage",
        //                Title = "Cerrar Sesión"
        //            });
        //            ; break;
        //    }
        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "icon.png",
        //    //    PageName = "UsersGroupPage",
        //    //    Title = "UsuariosGroupo ",
        //    //});


        //    //MapUbicateParkingPage

        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "eventos.png",
        //    //    PageName = "EventosPage",
        //    //    Title = "Eventos",
        //    //});


        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "icon.png",
        //    //    PageName = "LocatorioPage",
        //    //    Title = "Usuarios",
        //    //});

        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "eventos.png",
        //    //    PageName = "EventoPage",
        //    //    Title = "Evento",
        //    //});

        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "icon.png",
        //    //    PageName = "UsuariosPage",
        //    //    Title = "Usuarios",
        //    //});//UsuariosGroupPage


        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "icon.png",
        //    //    PageName = "UsuariosGroupPage",
        //    //    Title = "UsuariosGroup",
        //    //});//UsersGroupPage

        //    //UsuariosPage
        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "locatarios.png",
        //    //    PageName = "LocatorioPage",
        //    //    Title = "Locatorio",
        //    //});


        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "settings.png",
        //    //    PageName = "ConfiguracionPage",
        //    //    Title = "Configuración",
        //    //});

        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "icon.png",
        //    //    PageName = "ListViewPage",
        //    //    Title = "ListViewPage",
        //    //});
        //    //Menu.Add(new MenuItemViewModel
        //    //{
        //    //    Icon = "icon.png",
        //    //    PageName = "ReportsPage",
        //    //    Title = "ReportsPage",
        //    //});
        //}

        public void SetCurrentUser(User user)
        {
            App.CurrentUser = user;
        }
        #endregion

        /************** MONKEYS ********/
        public ObservableCollection<Monkey> Monkeys { get; set; }
        public ObservableCollection<Grouping<string, Monkey>> MonkeysGrouped { get; set; }
        public ObservableCollection<Zoo> Zoos { get; set; }
        public int MonkeyCount => Monkeys.Count;
        //public MonkeysViewModel()
        //{
        //    Monkeys = MonkeyHelper.Monkeys;
        //    MonkeysGrouped = MonkeyHelper.MonkeysGrouped;
        //    Zoos = new ObservableCollection<Zoo>
        //    {
        //        new Zoo
        //        {
        //            ImageUrl = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/23c1dd13-333a-459e-9e23-c3784e7cb434/2016-06-02_1049.png",
        //            Name = "Woodland Park Zoo"
        //        },
        //         new Zoo
        //        {
        //            ImageUrl =    "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png",
        //            Name = "Cleveland Zoo"
        //         },
        //        new Zoo
        //        {
        //            ImageUrl = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/e8179889-8189-4acb-bac5-812611199a03/2016-06-02_1053.png",
        //            Name = "Phoenix Zoo"
        //        }
        //    };
        //}
        /*********************/

        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}


public class Zoo
{
    public string ImageUrl { get; set; }
    public string Name { get; set; }
}

public class Person {
    public int Height { get; set; }
    public string Name { get; set; }
}