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
    public partial class TabPage :ContentPage
    {
        public TabPage()
        {
            InitializeComponent();
            //this.Appearing += (sender, e) => 
            //Label.Text = string.Format(Label.Text, Title);

        }
    }
}