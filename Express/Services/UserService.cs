using Express.Helpers;
using Express.Interfaces;
using Express.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Express.Services
{
    public class UserService : IUserService
    {
        public string baseUrl = "https://localhost:44353/api/user";
        BusinessLayer.Cryptography cryptography = new BusinessLayer.Cryptography();
        HttpClient client;
        private IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            client = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<User> GetUserByEmail(string username)
        {
            User user = new User();
            username = cryptography.Encrypt(username);
            string apiUrl = $"{baseUrl}/getuserbyemail/{username}";

            var uri = new Uri(string.Format(apiUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return user;
        }

        public async Task<bool> LogOut(string email)
        {
            var token = SessionHelper.DoesTokenExist(_httpContextAccessor.HttpContext.Session, "JWToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
            }

            bool loggedOut = false;
            string apiUrl = $"{baseUrl}/logout";
            var uri = new Uri(string.Format(apiUrl, string.Empty));
            var json = JsonConvert.SerializeObject(email);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(uri, content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    loggedOut = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return loggedOut;
        }
        //public async Task<bool> RefreshToken(string email)
        //{
        //    var token = SessionHelper.DoesTokenExist(_httpContextAccessor.HttpContext.Session, "JWToken");
        //    if (!string.IsNullOrWhiteSpace(token))
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
        //    }

        //    bool loggedOut = false;
        //    string apiUrl = $"{baseUrl}/refreshtoken";
        //    var uri = new Uri(string.Format(apiUrl, string.Empty));
        //    var json = JsonConvert.SerializeObject(email);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    try
        //    {
        //        var response = await client.PostAsync(uri, content).ConfigureAwait(false);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsStringAsync();
        //            loggedOut = JsonConvert.DeserializeObject<bool>(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"				ERROR {0}", ex.Message);
        //    }
        //    return loggedOut;
        //}
        public async Task<SignInResult> SignIn(User user)
        {
            Models.SignInResult signinresult = new Models.SignInResult();
            string apiUrl = $"{baseUrl}/authenticate";
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            user.Username = cryptography.Encrypt(user.Username);
            user.PasswordHash = cryptography.Encrypt(user.PasswordHash);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(uri, content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    signinresult = JsonConvert.DeserializeObject<Models.SignInResult>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return signinresult;
        }
    }
}