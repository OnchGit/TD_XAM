using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using App1.Objects;
using App1.Services;
using App1.Views;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace App1.ViewModels
{
    class MainPageViewModel:ViewModelBase
    {
        private ObservableCollection<PlaceItemSummary2> _oc = PersistencyService.getOc();


        public ICommand GoToNewPlaceCommand { get; }


        public MainPageViewModel()
        {
            GoToNewPlaceCommand = new Command(GoToNewPlaceAction);
        }

        private async void GoToNewPlaceAction()
        {
            await NavigationService.PushAsync(new NewPlacePage());
        }


        public ObservableCollection<PlaceItemSummary2> oc
        {
            get => _oc;
            set => SetProperty(ref _oc, value);
        }





 












    }
}
