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
    public partial class MainTabbedPage : TabbedPage
    {//https://github.com/NAXAM/bottomtabbedpage-xamarin-forms.git
        public MainTabbedPage ()
        {
            InitializeComponent();
        }
    }
}