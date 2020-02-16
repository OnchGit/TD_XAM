﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FourSquare.Auxilliary;
using FourSquare.Objects;
using FourSquare.Services;
using FourSquare.Views;
using Plugin.Media;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace FourSquare.ViewModels
{
    class MainPageViewModel:ViewModelBase
    {
        private ObservableCollection<PlaceItemSummary2> _oc = PersistencyService.getOc();


        public ICommand GoToNewPlaceCommand { get; }
        public ICommand PVCSelectedCommand { get; }

        private PlaceItemSummary2 _PVC;
        public PlaceItemSummary2 PVC
        {
            get => _PVC;
            set {
                SetProperty(ref _PVC, value);
                PersistencyService.SetPlaceId(PVC.Id);
                PersistencyService.SetPlaceDetail();
                GoToPlaceDetailAction();
                

            }
        }




        public MainPageViewModel()
        {
            GoToNewPlaceCommand = new Command(GoToNewPlaceAction);
            PVCSelectedCommand = new Command(GoToPlaceDetailAction);


        }

        private async void GoToPlaceDetailAction()
        {
            await NavigationService.PushAsync(new PlaceDetailPage());
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
