using System;
using System.Collections.Generic;
using System.Text;
using FourSquare.Services;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms.Maps;

namespace FourSquare.ViewModels
{
    class PlaceDetailPageViewModel: ViewModelBase
    {
        private Map _Map;
        
        private PlaceItem PI;
        public Map PlaceMap
        {
            get => _Map;
            set => SetProperty(ref _Map, value);
        }
     

        public PlaceDetailPageViewModel()
        {
            PlaceMap = new Map();
            PlaceMap.MinimumHeightRequest = 250;
            PI = PersistencyService.GetPlaceDetail();
            //PersistencyService.WipePlaceDetail();
            MapSetup();
        }

        private void MapSetup()
        {
            double prout = PI.Latitude;
            double prout2 = PI.Latitude;
            Position pos = new Position(PI.Latitude, PI.Longitude);
            Pin pin = new Pin
            {
                Address = PI.Title,
                Label = PI.Title,
                Position = pos
            };

            PlaceMap.Pins.Add(pin);
            

        }



    }
}
