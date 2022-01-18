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
    public class VehicleService: IVehicleService
    {
        public string baseUrl = "https://localhost:44353/api/vehicle";
        HttpClient client;
        private IHttpContextAccessor _httpContextAccessor;
        public VehicleService(IHttpContextAccessor httpContextAccessor)
        {
            client = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            var token = SessionHelper.DoesTokenExist(_httpContextAccessor.HttpContext.Session, "JWToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
            }
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            List<Vehicle> list = new List<Vehicle>();
            string apiUrl = $"{baseUrl}/getallvehicles";

            var uri = new Uri(string.Format(apiUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Vehicle>>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return list;
        }

        public async Task<Vehicle> GetVehicle(int id)
        {
            Vehicle item = new Vehicle();
            string apiUrl = $"{baseUrl}/getvehicle/{id}";

            var uri = new Uri(string.Format(apiUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Vehicle>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return item;
        }

        public async Task<int> CreateVehicle(Vehicle vehicle)
        {
            int id = 0;
            string apiUrl = $"{baseUrl}/createvehicle";
            try
            {
                var json = JsonConvert.SerializeObject(vehicle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        id = Convert.ToInt32(await response.Content.ReadAsStringAsync());
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"				ERROR {0}", ex.Message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return id;
        }

        public async Task<bool> UpdateVehicle(Vehicle vehicle)
        {
            bool updated = false;
            string apiUrl = $"{baseUrl}/updatevehicle/{vehicle.Id}";
            try
            {
                var json = JsonConvert.SerializeObject(vehicle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PutAsync(apiUrl, content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        updated = Convert.ToBoolean(await response.Content.ReadAsStringAsync());
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"				ERROR {0}", ex.Message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return updated;
        }

        public async Task<bool> DeleteVehicle(int id)
        {
            bool deleted = false;
            string apiUrl = $"{baseUrl}/deletevehicle/{id}";
            try
            {
                try
                {
                    var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        deleted = Convert.ToBoolean(await response.Content.ReadAsStringAsync());
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"				ERROR {0}", ex.Message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return deleted;
        }
        public async Task<bool> CheckIfRegistrationExists(string registration)
        {
            bool item = false;
            string apiUrl = $"{baseUrl}/checkifregistrationexists/{registration}";

            var uri = new Uri(string.Format(apiUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return item;
        }
        public async Task<bool> CheckIfVinExists(string vin)
        {
            bool item = false;
            string apiUrl = $"{baseUrl}/checkifvinexists/{vin}";

            var uri = new Uri(string.Format(apiUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return item;
        }
    }
}
