using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PModelo.Pages.GridP
{
    public class BreakfastViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation navigation;
        private ObservableCollection<BreakfastMenu> breakfastMenuList;
        private BreakfastMenu selectedBreakfastMenu;
        public ObservableCollection<BreakfastMenu> BreakfastMenuList
        {
            get => breakfastMenuList;
            set => SetObservableProperty(ref breakfastMenuList, value);
        }
        public BreakfastMenu SelectedBreakfastMenu
        {
            get => selectedBreakfastMenu;
            set => SetObservableProperty(ref selectedBreakfastMenu, value);
        }

        public ICommand MenuTappedCommand { get; set; }

        public BreakfastViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            BreakfastMenuList = new ObservableCollection<BreakfastMenu>()
           {
             new BreakfastMenu() { ImageSource = "Burger.png", MenuTitle = "BURGER" },
             new BreakfastMenu() { ImageSource = "Pizza.png", MenuTitle = "PIZZA" },
             new BreakfastMenu() { ImageSource = "EggBacon.png", MenuTitle = "BACON" },
             new BreakfastMenu() { ImageSource = "Sandwich.png", MenuTitle = "SANDWICH" },
              new BreakfastMenu() { ImageSource = "Burger.png", MenuTitle = "BURGER" },
             new BreakfastMenu() { ImageSource = "Pizza.png", MenuTitle = "PIZZA" },
             new BreakfastMenu() { ImageSource = "EggBacon.png", MenuTitle = "BACON" },
             new BreakfastMenu() { ImageSource = "Sandwich.png", MenuTitle = "SANDWICH" },
              new BreakfastMenu() { ImageSource = "Burger.png", MenuTitle = "BURGER" },
             new BreakfastMenu() { ImageSource = "Pizza.png", MenuTitle = "PIZZA" },
             new BreakfastMenu() { ImageSource = "EggBacon.png", MenuTitle = "BACON" },
             new BreakfastMenu() { ImageSource = "Sandwich.png", MenuTitle = "SANDWICH" },
              new BreakfastMenu() { ImageSource = "Burger.png", MenuTitle = "BURGER" },
             new BreakfastMenu() { ImageSource = "Pizza.png", MenuTitle = "PIZZA" },
             new BreakfastMenu() { ImageSource = "EggBacon.png", MenuTitle = "BACON" },
             new BreakfastMenu() { ImageSource = "Sandwich.png", MenuTitle = "SANDWICH" },
           };
            MenuTappedCommand = new Command(async () => await MenuSelectedAsync());
        }

        private async Task MenuSelectedAsync()
        {
            switch (SelectedBreakfastMenu.MenuTitle)
            {
                case "BURGER":
                    await navigation.PushModalAsync(new MainPage());
                    break;
                case "PIZZA":
                    await navigation.PushModalAsync(new MainPage());
                    break;
                case "BACON":
                    await navigation.PushModalAsync(new MainPage());
                    break;
                case "SANDWICH":
                    await navigation.PushModalAsync(new MainPage());
                    break;
            }

        }

        protected void SetObservableProperty<T>(ref T field, T value,
        [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class BreakfastMenu
{
    public ImageSource ImageSource { get; set; }
    public string MenuTitle { get; set; }
    public int Id_Espacio { get; set; }

}
