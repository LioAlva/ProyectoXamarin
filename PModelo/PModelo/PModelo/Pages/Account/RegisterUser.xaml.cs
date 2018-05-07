using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterUser : ContentPage
	{
       
        public RegisterUser ()
		{
			InitializeComponent ();
            // clickToShowPopup.Clicked += ClickToShowPopup_Clicked;
            //SfPicker picker = new SfPicker();
            //ColorInfo info = new ColorInfo();

            //picker.ItemsSource = info.Colors;
            // MyRadiouGroup.CheckedChanged += MyRadiouGroup_CheckedChanged;
            //popUpLayout = new SfPopupLayout();
            //clickToShowPopup.Clicked += ClickToShowPopup_Clicked;

            //clickToShowPopup.Clicked += ClickToShowPopup_Clicked;
            //popUpLayout.PopupView.AnimationMode = AnimationMode.SlideOnLeft;
        }

        //private void ClickToShowPopup_Clicked(object sender, EventArgs e)
        //{
        //    popUpLayout.Show();
        //}

        //private void ClickToShowPopup_Clicked(object sender, EventArgs e)
        ////{
        //    popupLayout.Show(mainLayout);
        //}


        //private void ClickToShowPopup_Clicked(object sender, EventArgs e)
        //{
        //    // Shows SfPopupLayout at the bottom of the label.
        //   // popUpLayout.ShowRelativeToView(null, RelativePosition.AlignBottom);
        //}

        //private void ClickToShowPopup_Clicked(object sender, EventArgs e)
        //{
        //    // Shows SfPopupLayout at the center of the view.
        //    popUpLayout.IsOpen = true;
        //}

        //void MyRadiouGroup_CheckedChanged(object sender, int e)
        //{
        //    var radio = sender as CustomRadioButton;

        //    if (radio == null || radio.Id == -1) return;

        //    txtSelected.Text = radio.Text;


        //}

    }
    public class ColorInfo

    {

        private ObservableCollection<string> _color;

        public ObservableCollection<string> Colors

        {

            get { return _color; }

            set { _color = value; }

        }

        public ColorInfo()

        {

            Colors = new ObservableCollection<string>();

            Colors.Add("Red");

            Colors.Add("Green");

            Colors.Add("Yellow");

            Colors.Add("Blue");

            Colors.Add("SkyBlue");

            Colors.Add("Orange");

            Colors.Add("Gray");

            Colors.Add("Pink");

        }

    }
}