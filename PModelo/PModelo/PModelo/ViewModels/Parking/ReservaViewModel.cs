using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using PModelo.Classes;
using PModelo.Classes.NoMapping;
using PModelo.Models;
using PModelo.Services;
using PModelo.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;

namespace PModelo.ViewModels
{
    public class ReservaViewModel: Reserva, INotifyPropertyChanged
    {
        #region Attributes
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private TimeSpan horaInicio;
        private TimeSpan horaFin;
        public DataService dataService;
        public ApiService apiService;
        public DialogService dialogService;
        public NavigationService navigationService;
        public GeolocatorService geolocatorService;
        #endregion

        #region Properties
        public DateTime FechaInicio
        {
            set
            {
                if (fechaInicio != value)
                {
                    fechaInicio = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FechaInicio"));
                }
            }
            get
            {
                return fechaInicio;
            }
        }

        public DateTime FechaFin
        {
            set
            {
                if (fechaFin != value)
                {
                    fechaFin = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FechaFin"));
                }
            }
            get
            {
                return fechaFin;
            }
        }

        public TimeSpan HoraInicio
        {
            set
            {
                if (horaInicio != value)
                {
                    horaInicio = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HoraInicio"));
                }
            }
            get
            {
                return horaInicio;
            }
        }

        public TimeSpan HoraFin
        {
            set
            {
                if (horaFin != value)
                {
                    horaFin = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HoraFin"));
                }
            }
            get
            {
                return horaFin;
            }
        }

        #endregion

        #region Constructor
        public ReservaViewModel()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
            apiService = new ApiService();
            dataService = new DataService();

            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            HoraInicio = DateTime.Now.TimeOfDay;
            HoraFin = DateTime.Now.TimeOfDay;
            FechaInicio =Utilities.GetFecha();
            FechaFin = Utilities.GetFecha();


            //Select today dates
            todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month).Substring(0, 3));
            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());
            todaycollection.Add(DateTime.Now.Date.Year.ToString());

            this.StartDate = todaycollection;
        }
        #endregion

        #region Methos
        public ICommand ReservePlaceCommand { get { return new RelayCommand(ReservePlace); } }

        public async void ReservePlace()
        {
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (isReachable)
            {
                var currentUser = dataService.First<User>(false);
                if (currentUser != null)
                {
                    if (!string.IsNullOrEmpty(currentUser.AccessToken))
                    {
                        var registerReserveForm = new ReserveForm()
                        {
                            Id_Parqueadero = Id_Parqueadero,//SOLO
                            Fecha_Hora_Fin = FechaFin+HoraFin,
                            Fecha_Hora_Inicio = FechaInicio+HoraInicio,
                            Id_Cliente = currentUser.UserId,
                            Id_Espacio=Id_Espacio,
                            Latitud=Latitud,
                            Longitud=Longitud,
                            UserTypeId = currentUser.UserTypeId
                        };

                        var response = await apiService.Post<ReserveForm, ResponseT<Reserva>>(Configuration.SERVER, "/api", "/Reserva/ReserveEspace", currentUser.TokenType, currentUser.AccessToken, registerReserveForm);

                        if (response != null)
                        {
                            var result = (ResponseT<Reserva>)response.Resullt;
                            //isBusy = false;
                            //IsEnabled = !isBusy;

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
        #endregion

        #region Reloads
        public void ReloadReservaIdentity(Reserva reserva)
        {
            if (reserva != null)
            {
                Nombre_Espacio = reserva.Nombre_Espacio;
                Nombre_Parqueadero = reserva.Nombre_Parqueadero;
                Nombre_Tipo_Parqueadero = reserva.Nombre_Tipo_Parqueadero;
                Id_Parqueadero = reserva.Id_Parqueadero;
                Id_Espacio = reserva.Id_Espacio;
            }
        }

        #endregion

        

        #region Datepicker
        private ObservableCollection<object> _startdate;

        public ObservableCollection<object> StartDate
        {
            get { return _startdate; }
            set { _startdate = value; RaisePropertyChanged("StartDate"); }
        }

        //public DatePickerViewModel()
        //{
        //    ObservableCollection<object> todaycollection = new ObservableCollection<object>();

        //    //Select today dates
        //    todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month).Substring(0, 3));
        //    if (DateTime.Now.Date.Day < 10)
        //        todaycollection.Add("0" + DateTime.Now.Date.Day);
        //    else
        //        todaycollection.Add(DateTime.Now.Date.Day.ToString());
        //    todaycollection.Add(DateTime.Now.Date.Year.ToString());

        //    this.StartDate = todaycollection;
        //}

        void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}



//#region Event

//public event PropertyChangedEventHandler PropertyChanged;

//#endregion
