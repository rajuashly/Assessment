using Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllVehicles();
        Task<Vehicle> GetVehicle(int? id);
        Task<int> CreateVehicle(Vehicle vehicle);
        Task<bool> UpdateVehicle(int id, Vehicle vehicle);
        Task<bool> DeleteVehicle(int id);
        bool VehicleExists(int id);
        Task<bool> CheckIfRegistrationExists(string registration);
        Task<bool> CheckIfVinExists(string vin);
    }
}
