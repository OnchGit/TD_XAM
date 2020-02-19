using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using FourSquare.Services;
using Plugin.Media;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourSquare.ViewModels
{
    class ProfilePageViewModel: ViewModelBase
    {
        public ICommand TakePictureCommand { get; }
        public ICommand PickPictureCommand { get; }
        public ICommand ProfileUpdateCommand { get; }
        public ICommand PasswordUpdateCommand { get; }

        private int ImgId;


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

        private string _OldPsw;
        public string OldPsw
        {
            get => _OldPsw;
            set => SetProperty(ref _OldPsw, value);
        }

        private string _NewPsw;
        public string NewPsw
        {
            get => _NewPsw;
            set => SetProperty(ref _NewPsw, value);
        }



        private byte[] _ImgByte { get; set; }

        private ImageSource _ImgSrc;
        public ImageSource ImgSrc
        {
            get => _ImgSrc;
            set => SetProperty(ref _ImgSrc, value);
        }

        public ProfilePageViewModel()
        {
            TakePictureCommand = new Command(Take_Photo);
            PickPictureCommand = new Command(Pick_Photo);
            ProfileUpdateCommand = new Command(ProfileUpdate);
            PasswordUpdateCommand = new Command(PasswordUpdate);
            Setup();
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

        public async void Setup()
        {
            var user = PersistencyService.GetUser();
            var id = user.ImageId;
            if (id.HasValue)
            {
                var tmp = await ApiService.GetImageFromId(id.Value);
                ImgSrc = (StreamImageSource)ImageSource.FromStream(() => new MemoryStream(tmp));
            }
            else
            {
                var tmp = await ApiService.GetImageFromId(1);
                PersistencyService.GetUser().ImageId = 1;
                ImgSrc = (StreamImageSource)ImageSource.FromStream(() => new MemoryStream(tmp));
            }
       
        }

        public async void ProfileUpdate()
        {
            var ImgId = await ApiService.UploadImage(_ImgByte);
            var tmp = await ApiService.UpdateProfile(fn, ln, ImgId);
            if (tmp == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Profile Change Error!", "Please check your information.", "I got it!");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Profile Change Success!", "Enjoy your new profile!", "I got it!");
            }

        }

        public async void PasswordUpdate()
        {
           var tmp = await ApiService.UpdatePassword(OldPsw, NewPsw);
            if (tmp == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Password Change Error!", "Please check your information.", "I got it!");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Password Change Success!", "Enjoy your new password!", "I got it!");
            }
        }


    }
}
