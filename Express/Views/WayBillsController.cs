using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Express.Models;

namespace Express.Views
{
    public class WayBillsController : Controller
    {
        private readonly ExpressDB_APIContext _context;

        public WayBillsController(ExpressDB_APIContext context)
        {
            _context = context;
        }

        // GET: WayBills
        public async Task<IActionResult> Index()
        {
            var expressDB_APIContext = _context.WayBill.Include(w => w.AssignedToVehicle).Include(w => w.CreatedBy);
            return View(await expressDB_APIContext.ToListAsync());
        }

        // GET: WayBills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wayBill = await _context.WayBill
                .Include(w => w.AssignedToVehicle)
                .Include(w => w.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wayBill == null)
            {
                return NotFound();
            }

            return View(wayBill);
        }

        // GET: WayBills/Create
        public IActionResult Create()
        {
            ViewData["AssignedToVehicleId"] = new SelectList(_context.Vehicle, "Id", "Color");
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "PasswordHash");
            return View();
        }

        // POST: WayBills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssignedToVehicleId,TotalWeight,ParcelCount,Reference,ContentDescription,VehicleStartsFrom,Destination,CreatedById,CreatedDate,Date")] WayBill wayBill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wayBill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignedToVehicleId"] = new SelectList(_context.Vehicle, "Id", "Color", wayBill.AssignedToVehicleId);
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "PasswordHash", wayBill.CreatedById);
            return View(wayBill);
        }

        // GET: WayBills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wayBill = await _context.WayBill.FindAsync(id);
            if (wayBill == null)
            {
                return NotFound();
            }
            ViewData["AssignedToVehicleId"] = new SelectList(_context.Vehicle, "Id", "Color", wayBill.AssignedToVehicleId);
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "PasswordHash", wayBill.CreatedById);
            return View(wayBill);
        }

        // POST: WayBills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssignedToVehicleId,TotalWeight,ParcelCount,Reference,ContentDescription,VehicleStartsFrom,Destination,CreatedById,CreatedDate,Date")] WayBill wayBill)
        {
            if (id != wayBill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wayBill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WayBillExists(wayBill.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignedToVehicleId"] = new SelectList(_context.Vehicle, "Id", "Color", wayBill.AssignedToVehicleId);
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "PasswordHash", wayBill.CreatedById);
            return View(wayBill);
        }

        // GET: WayBills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wayBill = await _context.WayBill
                .Include(w => w.AssignedToVehicle)
                .Include(w => w.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wayBill == null)
            {
                return NotFound();
            }

            return View(wayBill);
        }

        // POST: WayBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wayBill = await _context.WayBill.FindAsync(id);
            _context.WayBill.Remove(wayBill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WayBillExists(int id)
        {
            return _context.WayBill.Any(e => e.Id == id);
        }
    }
}
