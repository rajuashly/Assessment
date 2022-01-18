using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Express.Models;
using Express.Interfaces;
using System.Text;
using Express.BusinessLayer;
using Express.Helpers;

namespace Express.Controllers
{
    public class WayBillMVCController : Controller
    {
        private readonly IWayBillService _waybillService;
        private readonly IVehicleService _vehicleService;
        private readonly IUserService _usersercice;
        BusinessLayer.Cryptography _cryptography = new BusinessLayer.Cryptography();

        public WayBillMVCController(IWayBillService waybillService, IVehicleService vehicleService, IUserService userservice)
        {
            _waybillService = waybillService;
            _vehicleService = vehicleService;
            _usersercice = userservice;
        }
        // GET: WayBills
        public async Task<IActionResult> Index()
        {
            return View(await _waybillService.GetAllWayBills());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wayBill = await _waybillService.GetWayBill(Convert.ToInt32(id));
            if (wayBill == null)
            {
                return NotFound();
            }

            return View(wayBill);
        }

        // GET: WayBills/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["AssignedToVehicleId"] = ViewData["AssignedToVehicleId"] = await GetVehicleListWithSelectedItem(0);
            return View();
        }

        // POST: WayBills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssignedToVehicleId,TotalWeight,ParcelCount,Reference,ContentDescription,VehicleStartsFrom,Destination,CreatedById,CreatedDate,Date")] WayBill wayBill)
        {
            if (ModelState.IsValid)
            {
                MethodLibrary methodLibrary = new MethodLibrary();
                var reference = methodLibrary.RandomReferenceGenerator();
                wayBill.Reference = reference;

                var encryptedEmail = SessionHelper.DoesUserEmailExist(HttpContext.Session, "EncryptedEmail");
                if (!string.IsNullOrWhiteSpace(encryptedEmail))
                {
                    var user = await _usersercice.GetUserByEmail(_cryptography.Decrypt(encryptedEmail));
                    if (user != null && user.Id > 0)
                    {
                        wayBill.CreatedById = user.Id;
                    }
                }
        

                var Id = await _waybillService.CreateWayBill(wayBill);
                if (Id > 0)
                {
                    TempData["CreateSuccess"] = $"Waybill reference is: {wayBill.Reference}";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create waybill, please try again.");
                }
            }

            ViewData["AssignedToVehicleId"] = await GetVehicleListWithSelectedItem(Convert.ToInt32(wayBill.AssignedToVehicleId));
            return View(wayBill);
        }

        public async Task<IActionResult> Assign(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wayBill = await _waybillService.GetWayBill(Convert.ToInt32(id));
            if (wayBill == null)
            {
                return NotFound();
            }

            ViewData["AssignedToVehicleId"] = ViewData["AssignedToVehicleId"] = await GetVehicleListWithSelectedItem(Convert.ToInt32(wayBill.AssignedToVehicleId));
            return View(wayBill);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybill = await _waybillService.GetWayBill(Convert.ToInt32(id));
            if (waybill == null)
            {
                return NotFound();
            }
            ViewData["AssignedToVehicleId"] = await GetVehicleListWithSelectedItem(Convert.ToInt32(waybill.AssignedToVehicleId));
            return View(waybill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssignedToVehicleId")] WayBill wayBill)
        {
            if (id != wayBill.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Destination");
            ModelState.Remove("ContentDescription");
            ModelState.Remove("VehicleStartsFrom");

            if (ModelState.IsValid)
            {
                try
                {
                    var updated = await _waybillService.UpdateWayBill(wayBill);
                    if (updated)
                    {
                        TempData["UpdateSuccess"] = $"Waybill was successfully updated.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to update waybill, please try again.");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Failed to update waybill, please try again.");
                }
            }
            ViewData["AssignedToVehicleId"] = await GetVehicleListWithSelectedItem(Convert.ToInt32(wayBill.AssignedToVehicleId));
            return View(wayBill);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wayBill = await _waybillService.GetWayBill(Convert.ToInt32(id));

            if (wayBill == null)
            {
                return NotFound();
            }

            return View(wayBill);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _vehicleService.DeleteVehicle(id);
            if (deleted)
            {
                TempData["DeleteSuccess"] = $"Waybill was successfully deleted.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete waybill, please try again.");
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IEnumerable<SelectListItem>> GetVehicleListWithSelectedItem(int assignedToId)
        {
            List<SelectListItem> items = new SelectList(await _vehicleService.GetAllVehicles(), "Id", "VehicleDescription", assignedToId).ToList();
            var blankvehicle = new SelectListItem() { Value = "0", Text = "--- Select Vehicle ---" };
            items.Insert(0, blankvehicle);
            return items;
        }
    }
}
