using Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Interfaces
{
    public interface IWayBillRepository
    {
        Task<List<WayBill>> GetAllWayBills();
        Task<WayBill> GetWayBill(int? id);
        Task<int> CreateWayBill(WayBill wayBill);
        Task<bool> UpdateWayBill(int id, WayBill wayBill);
        Task<bool> DeleteWayBill(int id);
        bool WayBillExists(int id);
    }
}
