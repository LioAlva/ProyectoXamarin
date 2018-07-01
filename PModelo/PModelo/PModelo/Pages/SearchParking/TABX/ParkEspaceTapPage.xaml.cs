using PModelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParkEspaceTapPage : ContentPage
	{
		public ParkEspaceTapPage ()
		{
			InitializeComponent ();
            //var main = (MainViewModel)this.BindingContext;
            //var main = (MainViewModel.GetInstance());

            ////main.ParqueaderoSelected.RefreshPlaceList();
            //base.Appearing += (object sender, EventArgs e) =>
            //{
            //    main.ParqueaderoSelected.RefreshPlaceList();
            //};
        }
    }
}