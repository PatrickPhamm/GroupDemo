using KoiShowManagementSystem.Service.Base;
using KoiShowManagementSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KoiShowManagementSystem.Data.Models;

namespace KoiShowManagementSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationBusiness _service;
        public ApplicationController(IApplicationBusiness service) => _service = service;
        // GET: api/Kois
        [HttpGet]
        public async Task<IBusinessResult> Get()
        {
            return await _service.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetById(int id)
        {
            return await _service.GetApplicationById(id);
        }
        [HttpPost]
        public async Task<IBusinessResult> PostApplication(Application application)
        {
            //return await _service.Save(koi);
            return await _service.Save(application);
        }
    }
}
