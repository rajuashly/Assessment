using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Express.Models;
using Express.Interfaces;

namespace Express.DataAccess
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ExpressDB_APIContext _context;

        public VehicleRepository(ExpressDB_APIContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            var expressDBContext = _context.Vehicle.Include(v => v.Branch);
            return await expressDBContext.ToListAsync();
        }


        public async Task<Vehicle> GetVehicle(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var vehicle = await _context.Vehicle.Include(v => v.Branch).FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return null;
            }

            return vehicle;
        }


        public async Task<bool> CheckIfRegistrationExists(string registration)
        {
            var vehicle = await _context.Vehicle.FirstOrDefaultAsync(x => x.Registration == registration);
            if (vehicle != null && vehicle.Id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckIfVinExists(string vin)
        {
            var vehicle = await _context.Vehicle.FirstOrDefaultAsync(x => x.Vinumber == vin);
            if (vehicle != null && vehicle.Id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> CreateVehicle(Vehicle vehicle)
        {
            vehicle.CreatedDate = DateTime.Now;
            _context.Vehicle.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle.Id;
        }

        public async Task<bool> UpdateVehicle(int id, Vehicle vehicle)
        {

            var vehicleToUpdate = await GetVehicle(id);
            vehicleToUpdate.ModifiedDate = DateTime.Now;
            vehicleToUpdate.BranchId = vehicle.BranchId;
            vehicleToUpdate.Color = vehicle.Color;
            vehicleToUpdate.Make = vehicle.Make;
            vehicleToUpdate.Model = vehicle.Model;
            vehicleToUpdate.Registration = vehicle.Registration;
            vehicleToUpdate.Vinumber = vehicle.Vinumber;
            vehicleToUpdate.Year = vehicle.Year;
            _context.Entry(vehicleToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(vehicleToUpdate.Id))
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);

            if (vehicle == null)
            {
                return false;
            }
            else
            {
                _context.Vehicle.Remove(vehicle);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
