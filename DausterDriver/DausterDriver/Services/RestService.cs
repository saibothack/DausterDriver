using DausterDriver.Helpers;
using DausterDriver.Models;
using DausterDriver.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DausterDriver.Services
{
    public class RestService : IRestService
    {
        HttpClient client;

        #region Catalogs
        public async Task<List<Vehicle>> GetVehicle() {
            var uri = new Uri(string.Format(Constants.RestUrl + "vehicles", string.Empty));
            List<Vehicle> lsVehicles = new List<Vehicle>();

            try
            {
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    lsVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return lsVehicles;

        }
        #endregion

        public async Task<UserLogin> LoginAsync(User item)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "login-driver", string.Empty));
            UserLogin itemUser = new UserLogin();

            try
            {
                var postDriver = JsonConvert.SerializeObject(item);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemUser = JsonConvert.DeserializeObject<UserLogin>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemUser;
        }

        public async Task<int> RegisterAsync(User user) {
            var uri = new Uri(string.Format(Constants.RestUrl + "register-driver"));
            int IsSuccessStatusCode = 0;
            try
            {
                var postDriver = JsonConvert.SerializeObject(user);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                IsSuccessStatusCode = (int)response.StatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return IsSuccessStatusCode;
        }

        public async Task<User> GetUser()
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "vehicles", string.Empty));
            User itemUser = new User();

            try
            {
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(uri);

                var request = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    itemUser = JsonConvert.DeserializeObject<User>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemUser;

        }

        public async Task<int> CurrentLocationAsync(Dictionary<string, object> coordenadas)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "tracking"));
            int IsSuccessStatusCode = 0;
            try
            {
                var postDriver = JsonConvert.SerializeObject(coordenadas);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                IsSuccessStatusCode = (int)response.StatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return IsSuccessStatusCode;
        }
    }
}
