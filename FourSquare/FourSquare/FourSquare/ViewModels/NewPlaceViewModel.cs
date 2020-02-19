using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FourSquare.Services;
using Plugin.Media;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using System.IO;
using FourSquare.Views;

namespace FourSquare.ViewModels
{
    class NewPlaceViewModel:ViewModelBase
    {
        public ICommand TakePictureCommand { get; }
        public ICommand PickPictureCommand { get; }
        public ICommand GetCoordsCommand { get; }
        public ICommand UploadPlaceCommand { get; }

        private byte[] _ImgByte { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _desc;
        public string Desc
        {
            get => _desc;
            set => SetProperty(ref _desc, value);
        }

        private string _lat;
        public string Lat
        {
            get => _lat;
            set => SetProperty(ref _lat, value);
        }

        private string _lon;
        public string Lon
        {
            get => _lon;
            set => SetProperty(ref _lon, value);
        }




        private ImageSource _ImgSrc;
        public ImageSource ImgSrc
        {
            get => _ImgSrc;
            set => SetProperty(ref _ImgSrc, value);
        }


        public NewPlaceViewModel()
        {
            TakePictureCommand = new Command(Take_Photo);
            PickPictureCommand = new Command(Pick_Photo);
            GetCoordsCommand = new Command(GetCoords);
            UploadPlaceCommand = new Command(UploadPlace);
        }


        public async void Pick_Photo()
        {
            await CrossMedia.Current.Initialize();

            

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();


            if (file == null)
                return;

            ImageSource res = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            _ImgByte = StreamService.ReadFully(file.GetStream());
            ImgSrc = res;
            return;


        }

        public async void Take_Photo()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {

            });

            if (file == null)
                return;

            ImageSource res = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            _ImgByte = StreamService.ReadFully(file.GetStream());


            ImgSrc = res;
            return;

        }

        public async void GetCoords()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Lat = location.Latitude.ToString();
                    Lon = location.Longitude.ToString();
                }

                else
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Low);
                    var location2 = await Geolocation.GetLocationAsync(request);

                    if (location2 != null)
                    {
                        Lat = location2.Latitude.ToString();
                        Lon = location2.Longitude.ToString();
                    }
                }

            }
            catch(Exception )
            {

            }
        }

        public async void UploadPlace()
        {

            int ImgId = 1;
            ImgId = await ApiService.UploadImage(_ImgByte);

            double LatConvert = -1;
            double LonConvert = -1;
            double.TryParse(Lon, out LonConvert);

            if(double.TryParse(Lon, out LonConvert) && double.TryParse(Lat, out LatConvert))
            {
                int waiter = await ApiService.UploadPlace(ImgId, Title, Desc, LatConvert, LonConvert);
                PersistencyService.WipeOc();
                PersistencyService.OcFiller(await ApiService.GetPlaces());
                await NavigationService.PushAsync(new MainPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Bad Coordinates!", "Please enter valid numbers.", "I got it!");
            }
        }



    }
}
