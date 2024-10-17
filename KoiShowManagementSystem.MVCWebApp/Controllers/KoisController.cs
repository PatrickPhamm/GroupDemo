using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiShowManagementSystem.Data.Models;
using KoiShowManagementSystem.Common;
using Newtonsoft.Json;
using KoiShowManagementSystem.Service.Base;
using System.Diagnostics;

namespace KoiShowManagementSystem.MVCWebApp.Controllers
{
    public class KoisController : Controller
    {
        private readonly FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext _context;

        public KoisController(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Application>> GetApplication()
        {
            var applications = new List<Application>();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Application"))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            applications = JsonConvert.DeserializeObject<List<Application>>(result.Data.ToString());

                        }
                    }
                }
            }
            return applications;
        }

        // GET: Kois
        public async Task<IActionResult> KoiIndex()
        {
            
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Kois")) //+ "Kois"
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Koi>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<Koi>());

        }

       
        // GET: Kois/Details/5
        public async Task<IActionResult> KoiDetails(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Kois/" + id)) //+ "Kois"
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Koi>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Koi());
        }

        // GET: Kois/Create
        public async Task<IActionResult> KoiCreate()
        {
            ViewData["ApplicationId"] = new SelectList(await this.GetApplication(), "ApplicationId", "ApplicationId");
            return View();
        }

        // POST: Kois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("KoiId,ApplicationId,Name,Species,Breed,Age,Dayofbirth,Size,ColorPatern,HealthStatus")] Koi koi)
        public async Task<IActionResult> KoiCreate(Koi koi)
        {
            bool savestatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var respone = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "Kois/", koi))
                    {
                        if (respone.IsSuccessStatusCode)
                        {
                            var content = await respone.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);


                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                savestatus = true;
                            }
                            else
                            {
                                savestatus = false;
                            }
                        }
                    }
                }
            }

            #region
            #endregion
            if (savestatus)
            {
                return RedirectToAction(nameof(KoiIndex));
            }
            else
            {
                ViewData["ApplicationId"] = new SelectList(await this.GetApplication(), "ApplicationId", "ApplicationId");
                return View(koi);
            }
        }

        // GET: Kois/Edit/5
        public async Task<IActionResult> KoiEdit(int id)
        {
            var koi = new Koi();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Kois/" + id)) //+ "Kois"
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            koi = JsonConvert.DeserializeObject<Koi>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ApplicationId"] = new SelectList(await this.GetApplication(), "ApplicationId", "ApplicationId");
            return View(koi);
        }

        // POST: Kois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("KoiId,OwnerName,Name,Species,Breed,Age,Dayofbirth,Size,ColorPatern,HealthStatus")] Koi koi)
        public async Task<IActionResult> KoiEdit(int id, Koi koi)
        {
            bool saveStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "Kois/",koi)) //+ "Kois"
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if(result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                        {
                            saveStatus = true;
                        }
                        else
                        {
                            saveStatus = false;
                        }
                       
                    }
                }
            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(KoiIndex));
            }
            else
            {
                ViewData["ApplicationId"] = new SelectList(await this.GetApplication(), "ApplicationId", "ApplicationId");
                return View(koi);
            }
            
        }

        // GET: Kois/Delete/5
        public async Task<IActionResult> KoiDelete(int id)
        {
            var koi = new Koi();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Kois/" + id)) //+ "Kois"
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            koi = JsonConvert.DeserializeObject<Koi>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ApplicationId"] = new SelectList(await this.GetApplication(), "ApplicationId", "ApplicationId");
            return View(koi);
        }

        // POST: Kois/Delete/5
        [HttpPost, ActionName("KoiDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KoiDeleteConfirmed(int id)
        {
            bool deleteSave = false;
            if(ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var reponse = await httpClient.DeleteAsync(Const.APIEndPoint + "Kois/" + id))
                    {
                        if (reponse.IsSuccessStatusCode)
                        {
                            var content = await reponse.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                deleteSave = true;
                            }
                            else
                            {
                                deleteSave = false;
                            }
                        }
                    }
                }
            }
            if (deleteSave)
            {
                return RedirectToAction(nameof(KoiIndex));
            }
            else
            {
                return RedirectToAction(nameof(KoiDelete));
            }
        }

        private bool KoiExists(int id)
        {
            return _context.Kois.Any(e => e.KoiId == id);
        }
    }
}
