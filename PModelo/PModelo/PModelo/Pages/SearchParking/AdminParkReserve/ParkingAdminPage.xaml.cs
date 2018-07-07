using Syncfusion.SfCarousel.XForms;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PModelo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParkingAdminPage : ContentPage
	{
		public ParkingAdminPage ()
		{
			InitializeComponent ();
            SfCarousel carousel = new SfCarousel() { ItemWidth = 170, ItemHeight = 250 };

            ObservableCollection<SfCarouselItem> collectionOfItems = new ObservableCollection<SfCarouselItem>();

            collectionOfItems.Add(new SfCarouselItem() { ItemContent = new Label() { Text = "ItemContent2", BackgroundColor = Color.FromHex("#7E6E6B"), FontSize = 12 } });
            collectionOfItems.Add(new SfCarouselItem() { ItemContent = new Image() { Source = "icon.png", Aspect = Aspect.AspectFit } });
            collectionOfItems.Add(new SfCarouselItem() { ItemContent = new Button() { Text = "ItemContent1", TextColor = Color.White, BackgroundColor = Color.FromHex("#7E6E6B"), FontSize = 12 } });

            collectionOfItems.Add(new SfCarouselItem() { ImageName = "icon.png" });
            //collectionOfItems.Add(new SfCarouselItem() { ImageName = "icon.png" });
            //collectionOfItems.Add(new SfCarouselItem() { ImageName = "icon.png" });
            //collectionOfItems.Add(new SfCarouselItem() { ImageName = "icon.png" });
            //collectionOfItems.Add(new SfCarouselItem() { ImageName = "icon.png" });
            //collectionOfItems.Add(new SfCarouselItem() { ImageName = "icon.png" });

            carousel.ItemsSource = collectionOfItems;

            this.Content = carousel;
        }
	}
}