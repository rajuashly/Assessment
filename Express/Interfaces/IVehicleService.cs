using Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Interfaces
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAllVehicles();
        Task<Vehicle> GetVehicle(int id);
        Task<int> CreateVehicle(Vehicle vehicle);
        Task<bool> UpdateVehicle(Vehicle vehicle);
        Task<bool> DeleteVehicle(int id);
        Task<bool> CheckIfRegistrationExists(string registration);
        Task<bool> CheckIfVinExists(string vin);
    }
}
