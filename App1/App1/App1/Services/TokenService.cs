using System;
using System.Collections.Generic;
using System.Text;
using TD.Api.Dtos;

namespace App1.Services
{
    static class TokenService
    {
        static LoginResult lr = new LoginResult();


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

    }
}
