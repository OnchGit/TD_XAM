using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Services;
using App1.Views;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace App1
{
    class LoginPageViewModel:ViewModelBase
    {
        public ICommand LoginCommand { get; }

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

        public LoginPageViewModel()
        {
            LoginCommand = new Command(LoginAction);
        }

        private async void LoginAction()
        {
            LoginResult res = await ApiService.LoginHandler(usr, passw);
            if(res != null)
            {
                TokenService.lr_update(res);
                await NavigationService.PushAsync(new MainPage());
            }
            

        }

    }
}
