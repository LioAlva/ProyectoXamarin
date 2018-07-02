﻿using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PModelo.Controls
{
    public class CustomTimePicker : Syncfusion.SfPicker.XForms.SfPicker
    {
        public ObservableCollection<object> Time { get; set; }
        public ObservableCollection<object> Minute;
        public ObservableCollection<object> Hour;
        public ObservableCollection<object> Format;

        public ObservableCollection<string> Headers { get; set; }
        public CustomTimePicker()
        {
            Time = new ObservableCollection<object>();
            Hour = new ObservableCollection<object>();
            Minute = new ObservableCollection<object>();
            Format = new ObservableCollection<object>();
            Headers = new ObservableCollection<string>();
            if (Device.OS == TargetPlatform.Android)
            {
                Headers.Add("HOUR");
                Headers.Add("MINUTE");
                Headers.Add("MERIDIEM");
            }
            else
            {
                Headers.Add("Hour");
                Headers.Add("Minute");
                Headers.Add("Meridiem");
            }
            PopulateTimeCollection();

            this.ItemsSource = Time;
            this.ColumnHeaderText = Headers;
        }

        private void PopulateTimeCollection()
        {
            for (int i = 1; i <= 12; i++)
            {
                Hour.Add(i.ToString());
            }
            for (int j = 0; j < 60; j++)
            {
                if (j < 10)
                {
                    Minute.Add("0" + j);
                }
                else
                    Minute.Add(j.ToString());
            }

            Format.Add("AM");
            Format.Add("PM");

            Time.Add(Hour);
            Time.Add(Minute);
            Time.Add(Format);
        }
    }
}
