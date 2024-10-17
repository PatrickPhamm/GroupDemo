using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiShowManagementSystem.Data.Models;
using KoiShowManagementSystem.Service;
using KoiShowManagementSystem.Service.Base;
using KoiShowManagementSystem.Common;

namespace KoiShowManagementSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        //private readonly FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext _context;
        private readonly ResultService _resultService;

        /*public ResultsController(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context)
        {
            _context = context;
        }
*/

        //public ResultsController() => _resultService ??= new ResultService();

        public ResultsController(ResultService resultService)  => _resultService = resultService;

        // GET: api/Results
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        public async Task<IBusinessResult> GetResults()
        {
            //return await _context.Results.ToListAsync();
            return await _resultService.GetAll();
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Result>> GetResult(int id)
        public async Task<IBusinessResult> GetResult(int id)
        {
            //var result = await _resultService.GetById(id);

            //if (result == null)
            //{
            //    return NotFound();
            //}

            //return result;

            return await _resultService.GetById(id);
        }

        // PUT: api/Results/5
        [HttpPut]
        //public async Task<IActionResult> PutResult(int id, Result result)
        public async Task<IBusinessResult> PutResult(Result result)
        {
            #region old code
            //if (id != result.ResultId)
            //{
            //    return BadRequest();
            //}

            ////_context.Entry(result).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ResultExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
            #endregion

            //return await _resultService.Save(result);
            return await _resultService.Update(result);
        }


        // POST: api/Results
        [HttpPost]
        //public async Task<ActionResult<Result>> PostResult(Result result)
        public async Task<IBusinessResult> PostResult(Result result)
        {
            /*_context.Results.Add(result);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ResultExists(result.ResultId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetResult", new { id = result.ResultId }, result);*/

            return await _resultService.Save(result);
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteResult(int id)
        public async Task<IBusinessResult> DeleteResult(int id)
        {
            /*var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();*/

            return await _resultService.DeleteById(id);
        }

        /*private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.ResultId == id);
        }*/
    }
}
