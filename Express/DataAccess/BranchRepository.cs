using Express.Interfaces;
using Express.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.DataAccess
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ExpressDB_APIContext _context;

        public BranchRepository(ExpressDB_APIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> GetAllBranches()
        {
            return await _context.Branch.ToListAsync();
        }


        public async Task<Branch> GetBranch(int id)
        {
            var branch = await _context.Branch.FindAsync(id);

            if (branch == null)
            {
                return null;
            }

            return branch;
        }

        public async Task<bool> UpdateBranch(int id, Branch branch)
        { 

            _context.Entry(branch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<int> CreateBranch(Branch branch)
        {
            _context.Branch.Add(branch);
            await _context.SaveChangesAsync();
            return branch.Id;
        }


        public async Task<bool> DeleteBranch(int id)
        {
            var branch = await _context.Branch.FindAsync(id);
            if (branch == null)
            {
                return false;
            }
            else
            {
                _context.Branch.Remove(branch);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public bool BranchExists(int id)
        {
            return _context.Branch.Any(e => e.Id == id);
        }
    }
}
