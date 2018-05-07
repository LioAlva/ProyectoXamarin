using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PModelo.Services
{
    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Aceptar");
        }
      
        public async Task<bool> ShowMessageYesAndNot(string title, string message)
        {//DisplayAlert("Question","Do you want to reset the search-options?", "Yes", "No");
            var result = await App.Current.MainPage.DisplayAlert(title, message, "Sí", "No");

            return result;
        }

        public Task<bool> ShowMessageYesNot(string title, string message)
        {
            var result = App.Current.MainPage.DisplayAlert(title, message, "Sí", "No");
            return result;
        }

        public async Task<string> ShowImageOptions()
        {
            return await App.Current.MainPage.DisplayActionSheet(
                "¿De dónde deseas la imagen?",
                "Cancelar",
                null,
                "De la Galeria",
                "De la Cámara");
        }

        public async Task<string> ShowOptions(string title, string message, string ms1, string ms2)
        {
            return await App.Current.MainPage.DisplayActionSheet(
                "¿De dónde deseas la imagen?",
                "Cancelar",
                null,
                ms1,
                ms2);
        }
    }
}
