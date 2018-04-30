

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage 
    {
 
		public MasterPage ()
		{
			InitializeComponent ();
            App.Master = this;
            App.Navigator = Navigator;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //var Bar = BarPage.GetInstance();
            //Bar.Master = this;
            //App.Navigator = Navigator;
        }

        #region Singleton

        static MasterPage instance;

        public static MasterPage GetInstance()
        {
            if (instance == null)
            {
                instance = new MasterPage();
            }

            return instance;
        }

        #endregion

    }
}