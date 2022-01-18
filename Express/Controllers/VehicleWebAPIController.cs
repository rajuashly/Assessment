using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Express.Models;
using Express.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Express.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleWebAPIController : ControllerBase
    {
        private readonly IVehicleRepository _vehiclerepository;
        public VehicleWebAPIController(IVehicleRepository vehiclerepository)
        {
            _vehiclerepository = vehiclerepository;
        }

        [HttpGet("/api/vehicle/getallvehicles")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehicles()
        {
            return await _vehiclerepository.GetAllVehicles();
        }

        [HttpGet("/api/vehicle/getvehicle/{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _vehiclerepository.GetVehicle(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        [HttpPut("/api/vehicle/updatevehicle/{id}")]
        public async Task<ActionResult<bool>> UpdateVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            try
            {
                return await _vehiclerepository.UpdateVehicle(id, vehicle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_vehiclerepository.VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost("/api/vehicle/createvehicle")]
        public async Task<ActionResult<int>> CreateVehicle(Vehicle vehicle)
        {
          
            return await _vehiclerepository.CreateVehicle(vehicle);
        }

        [HttpDelete("/api/vehicle/deletevehicle/{id}")]
        public async Task<ActionResult<bool>> DeleteVehicle(int id)
        {
            var vehicle = _vehiclerepository.VehicleExists(id);
            if (vehicle)
            {
                return await _vehiclerepository.DeleteVehicle(id);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("/api/vehicle/checkifregistrationexists/{registration}")]
        public async Task<ActionResult<bool>> CheckIfRegistrationExists(string registration)
        {
            var result = await _vehiclerepository.CheckIfRegistrationExists(registration);
            return result;
        }
        [HttpGet("/api/vehicle/checkifvinexists/{vin}")]
        public async Task<ActionResult<bool>> CheckIfVinExists(string vin)
        {
            var result = await _vehiclerepository.CheckIfVinExists(vin);
            return result;
        }

    }
}
