using Express.Helpers;
using Express.Interfaces;
using Express.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Express.Services
{
    public class BranchService : IBranchService
    {
        public string baseUrl = "https://localhost:44353/api/branch";
        HttpClient client;
        private IHttpContextAccessor _httpContextAccessor;
        public BranchService(IHttpContextAccessor httpContextAccessor)
        {
            client = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            var token = SessionHelper.DoesTokenExist(_httpContextAccessor.HttpContext.Session, "JWToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
            }
        }

        public async Task<IEnumerable<Branch>> GetAllBranches()
        {
            List<Branch> list = new List<Branch>();
            string apiUrl = $"{baseUrl}/getallbranches";

            var uri = new Uri(string.Format(apiUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Branch>>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return list.OrderBy(x => x.Name);
        }

        public async Task<Branch> GetBranch(int id)
        {
            Branch item = new Branch();
            string apiUrl = $"{baseUrl}/getbranch/{id}";

            var uri = new Uri(string.Format(apiUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Branch>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return item;
        }

        public async Task<int> CreateBranch(Branch branch)
        {
            int id = 0;
            string apiUrl = $"{baseUrl}/createbranch";
            try
            {
                var json = JsonConvert.SerializeObject(branch);
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

        public async Task<bool> UpdateBranch(Branch branch)
        {
            bool updated = false;
            string apiUrl = $"{baseUrl}/updatebranch/{branch.Id}";
            try
            {
                var json = JsonConvert.SerializeObject(branch);
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

        public async Task<bool> DeleteBranch(int id)
        {
            bool deleted = false;
            string apiUrl = $"{baseUrl}/deletebranch/{id}";
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
