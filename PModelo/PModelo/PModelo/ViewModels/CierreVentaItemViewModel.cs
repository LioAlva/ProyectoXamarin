using Syncfusion.XForms.DataForm;

namespace PModelo.ViewModels
{
    public class CierreVentaItemViewModel
    {
        private string email;
        private string contactNo;

        public CierreVentaItemViewModel()
        {

        }

        [Display(ShortName = "Cantidad de Ventas")]
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }

        [Display(ShortName = "Cantidad de Transacciones")]
        public string ContactNumber
        {
            get { return contactNo; }
            set
            {
                this.contactNo = value;
            }
        }
    }
}
