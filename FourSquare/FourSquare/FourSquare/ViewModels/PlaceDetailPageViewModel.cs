using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using FourSquare.Auxilliary;
using FourSquare.Services;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FourSquare.ViewModels
{
    class PlaceDetailPageViewModel: ViewModelBase
    {
        public ICommand PostCommentCommand { get; }
        public ICommand CommentUpdateCommand { get; }
        public ICommand GoBackCommand { get; }

        private Map _Map;
        private ObservableCollection<CommentCell> _CommentList = new ObservableCollection<CommentCell>();
        private PlaceItem PI;
        private string _CommentContent;

        public string CommentContent
        {
            get => _CommentContent;
            set => SetProperty(ref _CommentContent, value);
        }

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
            PersistencyService.WipePlaceDetail();
            PostCommentCommand = new Command(PostComment);
            CommentUpdateCommand = new Command(Update);
            GoBackCommand = new Command(GoBack);
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
            CommentList = new ObservableCollection<CommentCell>();
            foreach (CommentItem element in PI.Comments)
            {
                var tmp = new CommentCell
                {
                    Author = element.Author.Email + " says:",
                    Date = element.Date.ToLongTimeString()+":",
                    Text = element.Text
                };

                if (!CommentList.Contains(tmp))
                {
                    CommentList.Add(tmp);
                }
            }
        }

        private async void PostComment()
        {
            int temp = await ApiService.PostComment(PI.Id, CommentContent);
            Update();

        }

        private async void Update()
        {
            PI = await ApiService.GetPlaceFromId(PI.Id);
            CommentSetup();
        }

        private async void GoBack()
        {
            await NavigationService.PopAsync();
        }
    }
}
