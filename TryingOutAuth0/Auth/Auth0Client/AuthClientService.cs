using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TryingOutAuth0.Auth.Auth0Client.Models;

namespace TryingOutAuth0.Auth.Auth0Client
{
    public class AuthClientService
    {
        private readonly AppSettings _appSettings;

        public AuthClientService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri($"{Utils.URLFromDomain(_appSettings.Auth0.Domain)}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                client_id = _appSettings.Auth0.client_id,
                client_secret = _appSettings.Auth0.client_secret,
                audience = _appSettings.Auth0.ApiIdentifier,
                grant_type = "password",
                scope = "openid",
                username = email,
                password = password,
            };

            var response = await client.PostAsJsonAsync("oauth/token", payload);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Your response data is: " + responseString);

                var deserialized = JsonSerializer.Deserialize<LoginResponseModel>(responseString);
                return deserialized.access_token;
            }

            return "Could not authenticate user.";
        }

        public async Task<bool> SignupAsync(string email, string password)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri($"{Utils.URLFromDomain(_appSettings.Auth0.Domain)}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                client_id = _appSettings.Auth0.client_id,
                email = email,
                password = password,
                connection = _appSettings.Auth0.Connection
            };

            var response = await client.PostAsJsonAsync("dbconnections/signup", payload);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Your response data is: " + responseString);

                return true;
            }

            return false;
        }

        public async Task<string> GetEmailAsync(string token)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri($"{Utils.URLFromDomain(_appSettings.Auth0.Domain)}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("userinfo");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Your response data is: " + responseString);

                var deserialized = JsonSerializer.Deserialize<GetUserModel>(responseString);
                return deserialized.email;
            }

            return "Could not authenticate user.";
        }
    }
}
