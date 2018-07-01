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
    public partial class TabbedCenterPage : TabbedPage
    {
        public TabbedCenterPage ()
        {
            InitializeComponent();
        }
    }
}