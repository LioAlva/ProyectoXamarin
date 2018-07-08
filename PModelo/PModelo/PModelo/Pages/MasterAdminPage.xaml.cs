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
	public partial class MasterAdminPage : MasterDetailPage
    {
		public MasterAdminPage ()
		{
			InitializeComponent ();
            App.MasterAdmin = this;
            App.Navigator = Navigator;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.MasterAdmin = this;
            App.Navigator = Navigator;
            //var Bar = BarPage.GetInstance();
            //Bar.Master = this;
            //App.Navigator = Navigator;
        }
    }
}