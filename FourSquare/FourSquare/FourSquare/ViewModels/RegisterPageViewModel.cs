using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FourSquare.Services;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace FourSquare.ViewModels
{
    class RegisterPageViewModel: ViewModelBase
    {
        public ICommand Register2Command { get; }

        private string _usr;
        public string usr
        {
            get => _usr;
            set => SetProperty(ref _usr, value);
        }

        private string _passw;
        public string passw
        {
            get => _passw;
            set => SetProperty(ref _passw, value);
        }

        private string _fn;
        public string fn
        {
            get => _fn;
            set => SetProperty(ref _fn, value);
        }

        private string _ln;
        public string ln
        {
            get => _ln;
            set => SetProperty(ref _ln, value);
        }

        public RegisterPageViewModel()
        {
            Register2Command = new Command(RegisterAction);
        }
        private async void RegisterAction()
        {
            LoginResult res = await ApiService.RegistrationHandler(usr, ln, fn, passw);
                await NavigationService.PopAsync();
        }


    }
}
