using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Express.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using Express.Services;
using Express.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Express.Controllers
{
    public class BranchMVCController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchMVCController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            return View(await _branchService.GetAllBranches());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchService.GetBranch(Convert.ToInt32(id));
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: Branches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,Address")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                var branchId = await _branchService.CreateBranch(branch);
                if (branchId > 0)
                {
                    TempData["CreateSuccess"] = $"{branch.Name} was successfully added.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create branch, please try again.");
                }
            }
            return View(branch);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchService.GetBranch(Convert.ToInt32(id));
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,Address")] Branch branch)
        {
            if (id != branch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updated = await _branchService.UpdateBranch(branch);
                    if (updated)
                    {
                        TempData["UpdateSuccess"] = $"Branch was successfully updated.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to update branch, please try again.");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Failed to update branch, please try again.");
                    throw;
                }
            }

            return View(branch);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchService.GetBranch(Convert.ToInt32(id));
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _branchService.DeleteBranch(id);
            if (deleted)
            {
                TempData["DeleteSuccess"] = $"Branch was successfully deleted.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete branch, please try again.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
