using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using TD.Api.Dtos;
using System.Threading.Tasks;
using Common.Api.Dtos;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using FourSquare.Objects;
using System.IO;

namespace FourSquare.Services
{
    static class ApiService
    {

        const string API = "https://td-api.julienmialon.com/";
        const string API_AUTH = API + "auth/";




        public static async Task<LoginResult> LoginHandler(string usn,string passw)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(API_AUTH + "login/");
            var requestBody = new LoginRequest();
            requestBody.Email = usn;
            requestBody.Password = passw;
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var test = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Response<LoginResult>>(test);
                response.Dispose();
                client.Dispose();
                return res.Data;
            }
            else {
                response.Dispose();
                client.Dispose();
                return null;
            }
        }

        public static async Task<LoginResult> RegistrationHandler(string usn,string ln,string fn,string passw)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(API_AUTH + "register/");
            var requestBody = new RegisterRequest();
            requestBody.Email = usn;
            requestBody.FirstName = fn;
            requestBody.LastName = ln;
            requestBody.Password = passw;

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var test = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Response<LoginResult>>(test);
                response.Dispose();
                client.Dispose();
                return res.Data;
            }
            else
            {
                response.Dispose();
                client.Dispose();
                return null;
            }
        }


        public static HttpClient GetAuthClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", PersistencyService.GetAccessToken());
            return client;
        }

        public static async Task<List<PlaceItemSummary2>> GetPlaces()
        {
            var client = GetAuthClient();
            var uri = new Uri(API + "places/");
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var temp = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Response<List<PlaceItemSummary2>>>(temp);
                response.Dispose();
                client.Dispose();
                return res.Data;
            }
            else
            {
                response.Dispose();
                client.Dispose();
                return null;
            }
        }

        public static async Task<byte[]> GetImageFromId(int id)
        {
            var client = GetAuthClient();
            var uri = new Uri(API + "images/"+id);
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                byte[] tmp = await response.Content.ReadAsByteArrayAsync();                
                response.Dispose();
                client.Dispose();
                
                return tmp;
            }
            else
            {
                response.Dispose();
                client.Dispose();
                return null;
            }


        }

    }
}
