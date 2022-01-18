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
    public class WayBillService : IWayBillService
    {
        public string baseUrl = "https://localhost:44353/api/waybill";
        HttpClient client;
        private IHttpContextAccessor _httpContextAccessor;
        public WayBillService(IHttpContextAccessor httpContextAccessor)
        {
            client = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            var token = SessionHelper.DoesTokenExist(_httpContextAccessor.HttpContext.Session, "JWToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
            }
        }

        public async Task<List<WayBill>> GetAllWayBills()
        {
            List<WayBill> list = new List<WayBill>();
            string apiUrl = $"{baseUrl}/getallwaybills";

            var uri = new Uri(string.Format(apiUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<WayBill>>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return list;
        }

        public async Task<WayBill> GetWayBill(int id)
        {
            WayBill item = new WayBill();
            string apiUrl = $"{baseUrl}/getwaybill/{id}";

            var uri = new Uri(string.Format(apiUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<WayBill>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return item;
        }

        public async Task<int> CreateWayBill(WayBill waybill)
        {
            int id = 0;
            string apiUrl = $"{baseUrl}/createwaybill";
            try
            {
                var json = JsonConvert.SerializeObject(waybill);
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

        public async Task<bool> UpdateWayBill(WayBill waybill)
        {
            bool updated = false;
            string apiUrl = $"{baseUrl}/updatewaybill/{waybill.Id}";
            try
            {
                waybill.Destination = "dummydata";
                waybill.VehicleStartsFrom = "dummydata";
                waybill.ContentDescription = "dummydata";
                var json = JsonConvert.SerializeObject(waybill);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    //empty string that wont be updated to pass validation errors on api side

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

        public async Task<bool> DeleteWayBill(int id)
        {
            bool deleted = false;
            string apiUrl = $"{baseUrl}/deletewaybill/{id}";
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
    }
}
