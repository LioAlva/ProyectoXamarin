using PModelo.Models;
using PModelo.ViewModels;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CierreVentasPage : ContentPage
	{
     
        public CierreVentasPage()
        {
          
            InitializeComponent();
            instance = this;
            //var mainviewmodel=MainViewModel.GetInstance();
   
            //dataFormX.DataObject = new CierreVentaItemViewModel();
            //dataFormX.LabelPosition = LabelPosition.Top;
            //dataForm.BindingContext = mainviewmodel.CierreVenta;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //dataFormX.DataObject = new CierreVentaItemViewModel();
            //dataFormX.LabelPosition = LabelPosition.Top;
        }


        #region Singleton

        static CierreVentasPage instance;

        public static CierreVentasPage GetInstance()
        {
            if (instance == null)
            {
                instance = new CierreVentasPage();
            }

            return instance;
        }

        #endregion
    }
}