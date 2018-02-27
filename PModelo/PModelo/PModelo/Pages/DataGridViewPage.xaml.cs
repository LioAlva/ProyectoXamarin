using PModelo.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DataGridViewPage : ContentPage
	{
		public DataGridViewPage()
		{
            InitializeComponent();
            this.dataGrid.ItemsSource = DataGridViewModel.OrdersInfo;
            DataGridViewModel.filtertextchanged = OnFilterChanged;
            //ColumnsList.SelectedIndex = 0;
        }

        //void OnColumnsSelectionChanged(object sender, EventArgs e)
        //{
        //    Picker newPicker = (Picker)sender;
        //    DataGridViewModel.SelectedColumn = newPicker.Items[newPicker.SelectedIndex];
        //    if (DataGridViewModel.SelectedColumn == "All Columns")
        //    {
        //        DataGridViewModel.SelectedCondition = "Contains";
        //        OptionsList.IsVisible = false;
        //        this.OnFilterChanged();
        //    }
        //    else
        //    {
        //        OptionsList.IsVisible = true;
        //        foreach (var prop in typeof(ItemList).GetProperties())
        //        {
        //            if (prop.Name == DataGridViewModel.SelectedColumn)
        //            {
        //                if (prop.PropertyType == typeof(string))
        //                {
        //                    OptionsList.Items.Clear();
        //                    OptionsList.Items.Add("Contains");
        //                    OptionsList.Items.Add("Equals");
        //                    OptionsList.Items.Add("NotEquals");
        //                    if (this.DataGridViewModel.SelectedCondition == "Equals")
        //                        OptionsList.SelectedIndex = 1;
        //                    else if (this.DataGridViewModel.SelectedCondition == "NotEquals")
        //                        OptionsList.SelectedIndex = 2;
        //                    else
        //                        OptionsList.SelectedIndex = 0;
        //                }
        //                else
        //                {
        //                    OptionsList.Items.Clear();
        //                    OptionsList.Items.Add("Equals");
        //                    OptionsList.Items.Add("NotEquals");
        //                    if (this.DataGridViewModel.SelectedCondition == "Equals")
        //                        OptionsList.SelectedIndex = 0;
        //                    else
        //                        OptionsList.SelectedIndex = 1;
        //                }
        //            }
        //        }
        //    }
        //}

        void OnFilterOptionsChanged(object sender, EventArgs e)
        {
            Picker newPicker = (Picker)sender;
            if (newPicker.SelectedIndex >= 0)
            {
                DataGridViewModel.SelectedCondition = newPicker.Items[newPicker.SelectedIndex];
                if (filterText.Text != null)
                    this.OnFilterChanged();
            }
        }

        void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
                DataGridViewModel.FilterText = "";
            else
                DataGridViewModel.FilterText = e.NewTextValue;
        }

        void OnFilterChanged()
        {
            if (dataGrid.View != null)
            {
                this.dataGrid.View.Filter = DataGridViewModel.FilerRecords;
                this.dataGrid.View.RefreshFilter();
            }
        }
    }
}