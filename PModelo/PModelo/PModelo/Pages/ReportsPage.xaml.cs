using Syncfusion.SfDataGrid.XForms;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportsPage : ContentPage
    {
        //private SfTabView tabView;
        
        //this.dataGrid.QueryCellStyle += DataGrid_QueryCellStyle;
        public ReportsPage ()
		{
			InitializeComponent ();
            //tabView = new SfTabView();
            //tabView.DisplayMode = TabDisplayMode.ImageWithText;
            //var allContactsGrid = new Grid { BackgroundColor = Color.Red };
            //var favoritesGrid = new Grid { BackgroundColor = Color.Green };
            //var contactsGrid = new Grid { BackgroundColor = Color.Blue };
            //var tabItems = new TabItemCollection
            //{
            //    new SfTabItem()
            //    {
            //        Title = "Calls",
            //        //IconFont = "Icon", // setting value for font icons as mentioned in *.ttf.
            //     //FontIconFontFamily = Device.RuntimePlatform == "iOS" ? "Icon" : "Icon.ttf",
            //        FontIconFontColor = Color.LightBlue,
            //        FontIconFontSize =  20,
            //       // Content = allContactsGrid
            //    },
            //    new SfTabItem()
            //    {
            //    Title = "Favorites",
            //    Content = favoritesGrid
            //    },
            //    new SfTabItem()
            //    {
            //    Title = "Contacts",
            //    Content = contactsGrid
            //    }
            //};
            //tabView.Items = tabItems;
            //this.Content = tabView;
            //this.dataGrid.QueryCellStyle += DataGrid_QueryCellStyle;

        }

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