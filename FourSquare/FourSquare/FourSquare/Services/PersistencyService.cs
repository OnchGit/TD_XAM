using FourSquare.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace FourSquare.Services
{
    static class PersistencyService
    {
        static LoginResult lr = new LoginResult();
        static ObservableCollection<PlaceItemSummary2> oc = new ObservableCollection<PlaceItemSummary2>();
        static ImageSource ImgSrcAddPlace = null;
        static int PlaceId = -1;
        static UserItem User = null;
        static PlaceItem PlaceDetail;


        public static void WipePlaceId()
        {
            PlaceId = -1;
        }

        public static int GetPlaceId()
        {
            return PlaceId;
        }

        public static UserItem GetUser()
        {
            return User;
        }

        public static void SetUser(UserItem ui)
        {
            User = ui;
        }

        public static int GetUserId()
        {
            return User.Id;
        }

        public static void SetPlaceId(int id)
        {
            PlaceId = id;
        }


        public static void WipeOc()
        {
            oc = new ObservableCollection<PlaceItemSummary2>();
        }

        public static void lr_update(LoginResult res)
        {
            lr.AccessToken = res.AccessToken;
            lr.ExpiresIn = res.ExpiresIn;
            lr.RefreshToken = res.RefreshToken;
            lr.TokenType = res.TokenType;
        }

        public static string GetRefreshToken()
        {
            return lr.RefreshToken;
        }

        public static string GetAccessToken()
        {
            return lr.AccessToken;
        }

        public static ObservableCollection<PlaceItemSummary2> getOc()
        {
            return oc;
        }

        public static async void OcFiller(List<PlaceItemSummary2> l)
        {
            List<PlaceItemSummary2> temp = null;

            try {
                var loc = await Geolocation.GetLastKnownLocationAsync();
                if(loc != null)
                {

                    temp=l.OrderBy(x =>CoordsDistance(x.Latitude,x.Longitude,loc.Latitude,loc.Longitude)).ToList();

                }
                else
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Low);
                    var loc2 = await Geolocation.GetLocationAsync(request);
                    if(loc2 != null)
                    {
                        temp.OrderBy(x => CoordsDistance(x.Latitude, x.Longitude, loc2.Latitude, loc2.Longitude)).ToList();
                    }

                }


            } catch (Exception) { }
            
            if(temp == null)
            {
                temp = l;
            }

            if (temp.Count != oc.Count)
            {
                oc = new ObservableCollection<PlaceItemSummary2>();
                foreach (PlaceItemSummary2 element in temp)
                {
                    
                    var tmp = await ApiService.GetImageFromId(element.ImageId);
                    element.ImgSrc = (StreamImageSource)ImageSource.FromStream(() => new MemoryStream(tmp));
                    if (element.Description.Length > 100) element.Description = element.Description.Substring(0, 99) + "...";
                    oc.Add(element);
                }

            }
        }

        public static ImageSource GetImgSrcAddPlace()
        {
            return ImgSrcAddPlace;
        }

        public static void SetImgSrcAddPlace(ImageSource im)
        {
            ImgSrcAddPlace = im;
        }

        public static void WipeImgSrcAddPlace()
        {
            ImgSrcAddPlace = null;
        }

        public static async Task<int> SetPlaceDetail()
        {
            PlaceDetail = await ApiService.GetPlaceFromId(PlaceId);
            return 0;
        }

        public static PlaceItem GetPlaceDetail()
        {
           
            return PlaceDetail;
        }

        public static void WipePlaceDetail()
        {
            PlaceDetail = null;
        }

        public static double CoordsDistance(double d1,double d2,double d3,double d4)
        {
            var Coords_1 = Math.Abs( d1 - d2);
            var Coords_2 = Math.Abs ( d3 - d4);
            return Math.Sqrt(Coords_1 + Coords_2);
        }


    }
}
