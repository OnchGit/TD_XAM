using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using TD.Api.Dtos;
using System.Threading.Tasks;
using Common.Api.Dtos;

namespace App1.Services
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


    }
}
