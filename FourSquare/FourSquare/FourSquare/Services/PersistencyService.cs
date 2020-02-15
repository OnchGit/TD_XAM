using FourSquare.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace FourSquare.Services
{
    static class PersistencyService
    {
        static LoginResult lr = new LoginResult();
        static ObservableCollection<PlaceItemSummary2> oc = new ObservableCollection<PlaceItemSummary2>();


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
            if(l.Count != oc.Count)
            {
                oc = new ObservableCollection<PlaceItemSummary2>();
                foreach (PlaceItemSummary2 element in l)
                {
                    
                    var tmp = await ApiService.GetImageFromId(element.ImageId);
                    element.ImgSrc = (StreamImageSource)ImageSource.FromStream(() => new MemoryStream(tmp));
                    if (element.Description.Length > 100) element.Description = element.Description.Substring(0, 99) + "...";
                    oc.Add(element);
                }

            }




        }


    }
}
