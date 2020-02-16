using System;
using System.Collections.Generic;
using System.Text;
using Storm.Mvvm;
using Xamarin.Forms.Maps;

namespace FourSquare.ViewModels
{
    class PlaceDetailPageViewModel: ViewModelBase
    {
        private Map _Map;
        public Map PlaceMap
        {
            get => _Map;
            set => SetProperty(ref _Map, value);
        }
     

        public PlaceDetailPageViewModel()
        {
            PlaceMap = new Map();
        }

    }
}
