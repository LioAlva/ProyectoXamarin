using PModelo.Models;
using PModelo.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace PModelo.ViewModels
{
    public class ReservaViewModel: Reserva, INotifyPropertyChanged
    {
        #region Attributes
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private TimeSpan horaInicio;
        private TimeSpan horaFin;
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


        #region Reloads
        public void ReloadReservaIdentity(Reserva reserva)
        {
            if (reserva != null)
            {
                Nombre_Espacio = reserva.Nombre_Espacio;
                Nombre_Parqueadero = reserva.Nombre_Parqueadero;
                Nombre_Tipo_Parqueadero = reserva.Nombre_Tipo_Parqueadero;
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
