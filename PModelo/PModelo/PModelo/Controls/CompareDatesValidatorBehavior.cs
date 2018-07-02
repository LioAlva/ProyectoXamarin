using PModelo.Services;
using System;
using Xamarin.Forms;

namespace PModelo.Controls
{
    public class CompareDatesValidatorBehavior : Behavior<DatePicker>
    {
        #region Attributes
        public DialogService dialogService;
        #endregion

        public CompareDatesValidatorBehavior()
        {
            dialogService = new DialogService();
        }

        public static BindableProperty DateProperty = BindableProperty.Create<CompareDatesValidatorBehavior, DateTime>(dt => dt.Date, DateTime.Now, BindingMode.TwoWay);

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static BindableProperty OrderProperty = BindableProperty.Create<CompareDatesValidatorBehavior, int>(dt => dt.Order, 0, BindingMode.TwoWay);

        public int Order
        {
            get { return (int)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        public static BindableProperty DatePickProperty = BindableProperty.Create<CompareDatesValidatorBehavior, DatePicker>(dt => dt.DatePick, null, BindingMode.TwoWay);

        public DatePicker DatePick
        {
            get { return (DatePicker)GetValue(DatePickProperty); }
            set { SetValue(DatePickProperty, value); }
        }

        protected override void OnAttachedTo(DatePicker dp)
        {
            dp.DateSelected += DateSelected;
            base.OnAttachedTo(dp);
        }

        // Compara que la fecha seleccionada sea mayor a la de otro control
        private async void DateSelected(object sender, DateChangedEventArgs e)
        {
            //var cierreVentaViewModel = CierreVentaViewModel.GetInstance();
            //bool valido = (Order == 1) 
            //    ? Date > e.NewDate 
            //    : (e.NewDate > Date);
            //((DatePicker)sender).BackgroundColor = valido ?
            //    Color.Green :
            //    Color.Red
            //    ;await dialogService.ShowMessage("Mensaje","La fecha Fin no puede ser menor o igual a la fecha Inicio") ;
            //DatePick.BackgroundColor = valido ? 
            //    Color.Green : Color.Red;
            bool valido = (Order == 1) ? Date >= e.NewDate : (e.NewDate >= Date);
            if (valido)
            {
                ((DatePicker)sender).BackgroundColor = Color.Default;
                DatePick.BackgroundColor = Color.Default;
                //cierreVentaViewModel.IsEnabledButton = true;
                //cierreVentaViewModel.ActivateIsEnableSearch();
            }
            else
            {
                //cierreVentaViewModel.DesactivateIsEnableSearch();
                //cierreVentaViewModel.IsEnabledButton = false;
                await dialogService.ShowMessage("Mensaje", "La fecha desde no puede ser mayor a la fecha hasta");
                ((DatePicker)sender).BackgroundColor = Color.Default;// Color.Red;
                DatePick.BackgroundColor = Color.Default;// ; Color.Red;
            }
        }

        protected override void OnDetachingFrom(DatePicker dp)
        {
            dp.DateSelected -= DateSelected;
            base.OnDetachingFrom(dp);
        }
    }
}
