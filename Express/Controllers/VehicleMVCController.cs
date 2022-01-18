using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Express.Models;
using Express.Interfaces;

namespace Express.Controllers
{
    public class VehicleMVCController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IVehicleService _vehicleService;

        public VehicleMVCController(IVehicleService vehicleService, IBranchService branchService)
        {
            _vehicleService = vehicleService;
            _branchService = branchService;

        }
        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _vehicleService.GetAllVehicles());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleService.GetVehicle(Convert.ToInt32(id));
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["BranchId"] = new SelectList(await _branchService.GetAllBranches(), "Id", "Name");
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchId,Make,Model,Year,Color,Registration,Vinumber")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                var registrationExists = await _vehicleService.CheckIfRegistrationExists(vehicle.Registration);
            
                var VinNumberExists = await _vehicleService.CheckIfVinExists(vehicle.Vinumber);
                if (!VinNumberExists && !registrationExists)
                {
                    var Id = await _vehicleService.CreateVehicle(vehicle);
                    if (Id > 0)
                    {
                        TempData["CreateSuccess"] = $"Vehicle was successfully added.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to create vehicle, please try again.");
                    }
                }
                else if (VinNumberExists)
                {
                    ModelState.AddModelError("", "Vehicle vin number already exists, please check vin and try again.");
                }
                else if (registrationExists)
                {
                    ModelState.AddModelError("", "Vehicle registration already exists, please check registration and try again.");
                }
            }
            ViewData["BranchId"] = new SelectList(await _branchService.GetAllBranches(), "Id", "Name", vehicle.BranchId);
            return View(vehicle);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleService.GetVehicle(Convert.ToInt32(id));
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(await _branchService.GetAllBranches(), "Id", "Name", vehicle.BranchId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchId,Make,Model,Year,Color,Registration,Vinumber,ModifiedDate")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updated = await _vehicleService.UpdateVehicle(vehicle);
                    if (updated)
                    {
                        TempData["UpdateSuccess"] = $"Vehicle was successfully updated.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to update vehicle, please try again.");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Failed to update vehicle, please try again.");
                    throw;

                }
            }
            ViewData["BranchId"] = new SelectList(await _branchService.GetAllBranches(), "Id", "Name", vehicle.BranchId);
            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleService.GetVehicle(Convert.ToInt32(id));

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _vehicleService.DeleteVehicle(id);
            if (deleted)
            {
                TempData["DeleteSuccess"] = $"Vehicle was successfully deleted.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete vehicle, please try again.");
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
