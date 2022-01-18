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
    public class WayBillRepository: IWayBillRepository
    {
        private readonly ExpressDB_APIContext _context;

        public WayBillRepository(ExpressDB_APIContext context)
        {
            _context = context;
        }

        public async Task<List<WayBill>> GetAllWayBills()
        {
            var expressDBContext = _context.WayBill.Include(w => w.AssignedToVehicle).Include(w => w.CreatedBy);
            return await expressDBContext.ToListAsync();
        }

        public async Task<WayBill> GetWayBill(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var wayBill = await _context.WayBill
                .Include(w => w.AssignedToVehicle)
                .Include(w => w.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wayBill == null)
            {
                return null;
            }

            return wayBill;
        }

        public async Task<int> CreateWayBill(WayBill wayBill)
        {
            if (wayBill.AssignedToVehicleId==0)
            {
                wayBill.AssignedToVehicleId = null;
            }
            wayBill.CreatedDate = DateTime.Now;
            _context.Add(wayBill);
            await _context.SaveChangesAsync();
            return wayBill.Id;
        }
        public async Task<bool> UpdateWayBill(int id, WayBill wayBill)
        {
            var toUpdate = await GetWayBill(id);
            toUpdate.AssignedToVehicleId = wayBill.AssignedToVehicleId;
            _context.Entry(toUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WayBillExists(wayBill.Id))
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteWayBill(int id)
        {
            var wayBill = await _context.WayBill.FindAsync(id);

            if (wayBill == null)
            {
                return false;
            }
            else
            {
                _context.WayBill.Remove(wayBill);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public bool WayBillExists(int id)
        {
            return _context.WayBill.Any(e => e.Id == id);
        }
    }
}
