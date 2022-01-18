using Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Interfaces
{
   public interface IWayBillService
    {
        Task<List<WayBill>> GetAllWayBills();
        Task<WayBill> GetWayBill(int id);
        Task<int> CreateWayBill(WayBill waybill);
        Task<bool> UpdateWayBill(WayBill waybill);
        Task<bool> DeleteWayBill(int id);
    }
}
