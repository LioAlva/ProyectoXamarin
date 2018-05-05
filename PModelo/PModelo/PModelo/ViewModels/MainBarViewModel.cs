using GalaSoft.MvvmLight.Command;
using PModelo.Services;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PModelo.ViewModels
{
    public class MainBarViewModel
    {
        #region Attributes
        private NavigationService navigationService;
        #endregion

        #region Properties
        public MainViewModel Main { get; set; }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<OrderViewModel> Orders { get; set; }

        public LoginViewModel Login { get; set; }

        public ObservableCollection<ContactItemViewModel> ContactList { get; set; }

        public ObservableCollection<OrderInfoItemViewModel> OrderInfoCollection { get; set; }

        public ObservableCollection<ChartDataPoint> DataPoints { get; set; }

        public CierreVentaItemViewModel CierreVenta { get; set; }

        SfChart chart { get; set; }

        public List<Person> Data { get; set; }

        public List<Person> Data2 { get; set; }
        #endregion

        #region Constructor

        public MainBarViewModel()
        {
            Main = new MainViewModel();

            instance = this;
            chart = new SfChart();
            navigationService = new NavigationService();
            Login = new LoginViewModel();
            ContactList = new ObservableCollection<ContactItemViewModel>();
            OrderInfoCollection = new ObservableCollection<OrderInfoItemViewModel>();
            DataPoints = new ObservableCollection<ChartDataPoint>();
            CierreVenta = new CierreVentaItemViewModel();

            Data = new List<Person>();
            Data2 = new List<Person>();

            LoadMenu();
            LoadData();
            LoadContact();
            LoadOrderInfo();
            LoadDataPoint();
        } 
        #endregion

        #region Singleton

        static MainBarViewModel instance;

        public static MainBarViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainBarViewModel();
            }

            return instance;
        }
        #endregion
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


        #region Commands
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
        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();

            Menu.Add(new MenuItemViewModel
            {
                Icon = "home.png",
                PageName = "DashboardPage",
                Title = "Dashboard",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "close.png",
                PageName = "ListaRojaPage",
                Title = "Lista Roja",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "locatarios.png",
                PageName = "LocatorioPage",
                Title = "Locatorio",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "eventos.png",
                PageName = "EventosPage",
                Title = "Eventos",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "settings.png",
                PageName = "ConfiguracionPage",
                Title = "Configuración",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "icon.png",
                PageName = "ListViewPage",
                Title = "ListViewPage",
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "icon.png",
                PageName = "ReportsPage",
                Title = "ReportsPage",
            });
        }
        #endregion
    }
}
