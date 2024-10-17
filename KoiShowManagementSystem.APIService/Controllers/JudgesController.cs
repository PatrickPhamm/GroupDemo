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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace KoiShowManagementSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgesController : ControllerBase
    {
        //private readonly FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext _context;
        private readonly JudgeService _judgeService;

        /*public JudgesController(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context)
        {
            _context = context;
        }*/

        public JudgesController(JudgeService judgeService) => _judgeService = judgeService;

        // GET: api/Judges
        [HttpGet]
        public async Task<IBusinessResult> GetJudges()
        {
            return await _judgeService.GetAllJudges();
        }

        // GET: api/Judges/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetJudge(int id)
        {
            return await _judgeService.GetJudgeById(id);
        }

        // POST: api/Judges
        [HttpPost]
        //public async Task<ActionResult<Result>> PostJudge(Result result)
        public async Task<IBusinessResult> PostJudge(Judge judge)
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

            return await _judgeService.Save(judge);
        }
    }
}
