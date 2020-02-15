using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TD.Api.Dtos;

namespace App1.Services
{
    static class PersistencyService
    {
        static LoginResult lr = new LoginResult();
        static ObservableCollection<PlaceItem> oc = new ObservableCollection<PlaceItem>();


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

        public static ObservableCollection<PlaceItem> getOc()
        {
            return oc;
        }

        public static void OcFiller(List<PlaceItem> l)
        {
            if(l.Count != oc.Count)
            {
                oc = new ObservableCollection<PlaceItem>();
                foreach (PlaceItem element in l)
                {
                    if (element.Description.Length > 100) element.Description = element.Description.Substring(0, 99) + "...";
                    oc.Add(element);
                }

            }




        }


    }
}
