
using DausterDriver.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DausterDriver.Services
{
    public class ServiceManager
    {
        IRestService restService;

        public ServiceManager(IRestService service)
        {
            restService = service;
        }

        #region Catalogs
        public Task<List<Vehicle>> GetVehicle()
        {
            return restService.GetVehicle();
        }

        #endregion

        public Task<UserLogin> LoginAsync(User item)
        {
            return restService.LoginAsync(item);
        }

        public Task<int> RegisterAsync(User user)
        {
            return restService.RegisterAsync(user);
        }

        public Task<User> GetUser()
        {
            return restService.GetUser();
        }

        public Task<int> CurrentLocationAsync(Dictionary<string, object> coordenadas)
        {
            return restService.CurrentLocationAsync(coordenadas);
        }

    }
}
