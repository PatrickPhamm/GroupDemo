using KoiShowManagementSystem.Service.Base;
using KoiShowManagementSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KoiShowManagementSystem.Data.Models;

namespace KoiShowManagementSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private readonly IContestService _service;
        public ContestController(IContestService service) => _service = service;
        
        [HttpGet]
        public async Task<IBusinessResult> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetById(int id)
        {
            return await _service.GetContestById(id);
        }

        [HttpPost]
        public async Task<IBusinessResult> PostContest(Contest contest)
        {
            //return await _service.Save(koi);
            return await _service.Save(contest);
        }
    }
}
