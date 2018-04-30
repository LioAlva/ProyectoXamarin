using PModelo.Models;
using PModelo.ViewModels;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CierreVentarPage : ContentPage
	{

        public CierreVentarPage ()
		{
            InitializeComponent ();
    
            dataForm.DataObject = new CierreVentaItemViewModel();
            dataForm.LabelPosition = LabelPosition.Top;
        }
	}
}