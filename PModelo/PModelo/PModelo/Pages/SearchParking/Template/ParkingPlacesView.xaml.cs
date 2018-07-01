using PModelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages.Template
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParkingPlacesView : ContentView
	{
		public ParkingPlacesView ()
		{
			InitializeComponent ();
            //var main = (MainViewModel)this.BindingContext;
            //main.ParqueaderoSelected.RefreshPlaceList();
            //base.Appearing += (object sender, EventArgs e) =>
            //{
            //    main.ParqueaderoSelected.RefreshPlaceList();
            //};
        }
       // override on
	}
}