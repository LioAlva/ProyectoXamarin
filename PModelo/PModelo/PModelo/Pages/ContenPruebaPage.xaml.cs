﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContenPruebaPage : ContentPage
	{
		public ContenPruebaPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // App.Master = this;
            //App.Navigator = Navigator;
        }
    }
}