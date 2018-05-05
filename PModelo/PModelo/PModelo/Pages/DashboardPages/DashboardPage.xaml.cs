using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
            InitializeComponent();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    App.MasterDashboardPage = this;
        //}

        private void DataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.CellValue.ToString() == "4")
            {
                e.Style.BackgroundColor = Color.YellowGreen;
                e.Style.ForegroundColor = Color.White;
            }
            e.Handled = true;
        }
    }
}