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
    public class WayBillWebAPIController : ControllerBase
    {
        private readonly IWayBillRepository _waybillrepository;
        public WayBillWebAPIController(IWayBillRepository waybillrepository)
        {
            _waybillrepository = waybillrepository;
        }

        [HttpGet("/api/waybill/getallwaybills")]
        public async Task<ActionResult<IEnumerable<WayBill>>> GetAllWaybills()
        {
            return await _waybillrepository.GetAllWayBills();
        }

        [HttpGet("/api/waybill/getwaybill/{id}")]
        public async Task<ActionResult<WayBill>> GetWaybill(int id)
        {
            var waybill = await _waybillrepository.GetWayBill(id);

            if (waybill == null)
            {
                return NotFound();
            }

            return waybill;
        }


        [HttpPut("/api/waybill/updatewaybill/{id}")]
        public async Task<ActionResult<bool>> UpdateWaybill(int id, WayBill waybill)
        {
            if (id != waybill.Id)
            {
                return BadRequest();
            }

            try
            {
                return await _waybillrepository.UpdateWayBill(id, waybill);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_waybillrepository.WayBillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost("/api/waybill/createwaybill")]
        public async Task<ActionResult<int>> CreateWayBill(WayBill waybill)
        {

            return await _waybillrepository.CreateWayBill(waybill);
        }


        [HttpDelete("/api/waybill/deletewaybill/{id}")]
        public async Task<ActionResult<bool>> DeleteWaybill(int id)
        {
            var waybill = _waybillrepository.WayBillExists(id);
            if (waybill)
            {
                return await _waybillrepository.DeleteWayBill(id);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
