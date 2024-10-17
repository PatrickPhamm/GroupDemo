using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiShowManagementSystem.Service;
using KoiShowManagementSystem.Service.Base;
using KoiShowManagementSystem.Data.Models;

namespace KoiShowManagementSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgesCriterionsController : ControllerBase
    {
        private readonly IJudgesCriteriaService _JCService;
        public JudgesCriterionsController(IJudgesCriteriaService service) => _JCService = service;
        // GET: api/JudgesCriterions
        [HttpGet]
        public async Task<IBusinessResult> GetJudgesCriteria()
        {
            return await _JCService.GetAll();
        }

        // GET: api/JudgesCriterions/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetJudgesCriterion(int id)
        {
            return await _JCService.GetById(id);
        }

        // PUT: api/JudgesCriterions/5
        [HttpPut]
        public async Task<IBusinessResult> PutJudgesCriterion(JudgesCriterion judgesCriterion)
        {
            return await _JCService.Update(judgesCriterion);
        }

        [HttpPost]
        public async Task<IBusinessResult> PostJudgesCriterion(JudgesCriterion judgesCriterion)
        {
            return await _JCService.Save(judgesCriterion);
        }

        // DELETE: api/JudgesCriterions/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteJudgesCriterion(int id)
        {
            return await _JCService.DeleteById(id);
        }
    }
}
