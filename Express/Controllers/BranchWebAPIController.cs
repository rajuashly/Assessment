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
    public class BranchWebAPIController : ControllerBase
    {
        private readonly IBranchRepository _branchrepository;
        public BranchWebAPIController(IBranchRepository branchrepository)
        {
            _branchrepository = branchrepository;
        }

        [HttpGet("/api/branch/getallbranches")]
        public async Task<ActionResult<IEnumerable<Branch>>> GetAllBranches()
        {
            var list = await _branchrepository.GetAllBranches();
            return list.ToList(); 
        }

        [HttpGet("/api/branch/getbranch/{id}")]
        public async Task<ActionResult<Branch>> GetBranch(int id)
        {
            var branch = await _branchrepository.GetBranch(id);

            if (branch == null)
            {
                return NotFound();
            }

            return branch;
        }

        [HttpPut("/api/branch/updatebranch/{id}")]
        public async Task<ActionResult<bool>> UpdateBranchAsync(int id, Branch branch)
        {
            if (id != branch.Id)
            {
                return BadRequest();
            }

            try
            {
                return await _branchrepository.UpdateBranch(id, branch);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_branchrepository.BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost("/api/branch/createbranch")]
        public async Task<ActionResult<int>> CreateBranch(Branch branch)
        {

            return await _branchrepository.CreateBranch(branch);
        }

        // DELETE: api/BranchesWebAPI/5
        [HttpDelete("/api/branch/deletebranch/{id}")]
        public async Task<ActionResult<bool>> DeleteBranch(int id)
        {
            return await _branchrepository.DeleteBranch(id);
        }

    }
}
