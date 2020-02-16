using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FourSquare.Auxilliary;
using FourSquare.Services;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms.Maps;

namespace FourSquare.ViewModels
{
    class PlaceDetailPageViewModel: ViewModelBase
    {
        private Map _Map;
        private ObservableCollection<CommentCell> _CommentList = new ObservableCollection<CommentCell>();
        private PlaceItem PI;

        public ObservableCollection<CommentCell> CommentList
        {
            get => _CommentList;
            set => SetProperty(ref _CommentList, value);
        }


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
            CommentSetup();
        }

        private void MapSetup()
        {

            Position pos = new Position(PI.Latitude, PI.Longitude);
            Pin pin = new Pin
            {
                
                Label = PI.Title,
                Position = pos
            };

            PlaceMap.Pins.Add(pin);
            PlaceMap.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(1)));
           
            

        }

        private void CommentSetup()
        {
            foreach(CommentItem element in PI.Comments)
            {
                var tmp = new CommentCell
                {
                    Author = element.Author.Email + " says:",
                    Date = element.Date.ToShortTimeString(),
                    Text = element.Text
                };

                CommentList.Add(tmp);
            }
        }

    }
}
