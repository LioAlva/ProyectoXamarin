using GalaSoft.MvvmLight.Command;
using PModelo.Pages;
using PModelo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace PModelo.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        private NavigationService navigationService;
        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<OrderViewModel> Orders { get; set; }
        #endregion

        public MainViewModel()
        {
            navigationService = new NavigationService();
            LoadMenu();
            LoadData();
        }


        #region Commands
        public ICommand GoToCommand { get { return new RelayCommand<string>(GoTo); } }

        private void GoTo(string pageName)
        {
            navigationService.Navigate(pageName);
        }


        public ICommand StartCommand { get { return new RelayCommand(Start); } }

        private void Start()
        {
            navigationService.SetMainPage(new MasterPage());
        }



        #endregion


        private void LoadData()
        {
            Orders = new ObservableCollection<OrderViewModel>();

            for (int i = 0; i < 10; i++)
            {
                Orders.Add(new OrderViewModel
                {
                    Title = "Lorem Ipsum",
                    DeliveryDate = DateTime.Today,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                });
            }
        }

        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();

            Menu.Add(new MenuItemViewModel
            {
                Icon = "icon.png",
                PageName = "NewOrderPage",
                Title = "MainPage",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "icon.png",
                PageName = "ClientsPage",
                Title = "Clientes",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "icon.png",
                PageName = "AlarmsPage",
                Title = "Alarmas",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "icon.png",
                PageName = "SettingsPage",
                Title = "Ajustes",
            });
        }
    }

}
