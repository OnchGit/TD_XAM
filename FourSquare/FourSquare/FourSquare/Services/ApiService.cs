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
using System.Diagnostics;

namespace FourSquare.Services
{
    static class ApiService
    {

        const string API = "https://td-api.julienmialon.com/";
        const string API_AUTH = API + "auth/";

        public static HttpClient GetAuthClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", PersistencyService.GetAccessToken());
            return client;
        }

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

        public static async Task<int> UploadImage(byte[] Img)
        {
            var client = GetAuthClient();
            var uri = new Uri(API + "images");
            byte[] imageData = Img;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, API + "images");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", PersistencyService.GetAccessToken());

            MultipartFormDataContent requestContent = new MultipartFormDataContent();

            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
            requestContent.Add(imageContent, "file", "file.jpg");

            request.Content = requestContent;

            HttpResponseMessage response = await client.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<Response<ImageItem>>(result);

                
                return res.Data.Id;
                
            }
            else
            {
                
                return -1;
            }

            /* MultipartFormDataContent requestContent = new MultipartFormDataContent();
             var imageContent = new ByteArrayContent(imageData);
             imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
             requestContent.Add(imageContent, "File", "test.jpeg");

             HttpResponseMessage response = await client.PostAsync(uri, requestContent);
             if (response.IsSuccessStatusCode)
             {
                 var tmp = JsonConvert.DeserializeObject<Response<ImageItem>>( await response.Content.ReadAsStringAsync());
                 int res = tmp.Data.Id;
                 Console.WriteLine("IMG UPLOAD ID: " + res);
                 response.Dispose();
                 client.Dispose();
                 return res;
             }
             else
             {
                 response.Dispose();
                 client.Dispose();
                 return 0;
             }*/

        }

        public static async Task<int> UploadPlace(int ImgId, string _Title, string Desc, double Lat, double Lon)
        {
            CreatePlaceRequest tmp = new CreatePlaceRequest
            {
                ImageId=ImgId,
                Title = _Title,
                Description = Desc,
                Latitude = Lat,
                Longitude =Lon
            };

            var json = JsonConvert.SerializeObject(tmp);

            var client = GetAuthClient();
            var uri = new Uri(API + "places");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                
            }

            client.Dispose();
            response.Dispose();
            return 0;



        }

        public static async Task<PlaceItem> GetPlaceFromId(int id)
        {
            var client = GetAuthClient();
            var uri = new Uri(API+"places/"+id);
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var tmp = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Response<PlaceItem>>(tmp);
                client.Dispose();
                response.Dispose();
                return json.Data;
            }
            else
                client.Dispose();
                response.Dispose();
                return null;


        }
    }
}
