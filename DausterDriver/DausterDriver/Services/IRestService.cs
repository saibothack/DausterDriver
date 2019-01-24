using DausterDriver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DausterDriver.Services
{
    public interface IRestService
    {
        #region Catalogs
        Task<List<Vehicle>> GetVehicle();
        #endregion

        Task<UserLogin> LoginAsync(User item);
        Task<int> RegisterAsync(User user);
        Task<User> GetUser();
        Task<int> CurrentLocationAsync(Dictionary<string, object> coordenadas);
    }
}
