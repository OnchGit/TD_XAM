using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using App1.Services;
using Storm.Mvvm;
using TD.Api.Dtos;

namespace App1.ViewModels
{
    class MainPageViewModel:ViewModelBase
    {
        private ObservableCollection<PlaceItem> _oc = PersistencyService.getOc();


        public ObservableCollection<PlaceItem> oc
        {
            get => _oc;
            set => SetProperty(ref _oc, value);
        }

        










    }
}
