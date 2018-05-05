﻿using GalaSoft.MvvmLight.Command;
using PModelo.Pages;
using PModelo.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PModelo.ViewModels
{
    public class MenuItemViewModel
    {
        private NavigationService navigationService;

        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }

        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }

        //public ICommand NavigateCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(
        //            () => navigationService.Navigate(PageName)
        //            );
        //    }
        //}


        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }

        private async void Navigate()
        {
            await navigationService.Navigate(PageName);
        }

    }
}
