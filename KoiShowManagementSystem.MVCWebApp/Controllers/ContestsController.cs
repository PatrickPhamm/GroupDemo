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

namespace KoiShowManagementSystem.MVCWebApp.Controllers
{
    public class ContestsController : Controller
    {
        private readonly FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext _context;

        public ContestsController(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Contests
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Contest")) 
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Contest>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<Contest>());
        }

        // GET: Contests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Contest/" + id))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Contest>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Contest());
        }

        // GET: Contests/Create
        public IActionResult Create()
        {
            ViewData["KoiId"] = new SelectList(_context.Kois, "KoiId", "KoiId");
            return View();
        }

        // POST: Contests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contest contest)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "Contest/", contest))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
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
            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["KoiId"] = new SelectList(_context.Kois, "KoiId", "KoiId", contest.KoiId);
                return View(contest);
            }
            
        }

        // GET: Contests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var contest = new Contest();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Contest/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            contest = JsonConvert.DeserializeObject<Contest>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["KoiId"] = new SelectList(_context.Kois, "KoiId", "KoiId", contest.KoiId);
            return View(contest);
        }

        // POST: Contests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contest contest)
        {
            bool saveStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "Contest/", contest))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
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
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["KoiId"] = new SelectList(_context.Kois, "KoiId", "KoiId", contest.KoiId);
                return View(contest);
            }
        }
            
        

        // GET: Contests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Contest/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Contest>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Contest());
        }

        // POST: Contests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "Contest/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                deleteStatus = true;
                            }
                            else
                            {
                                deleteStatus = false;
                            }
                        }
                    }
                }
            }
            if (deleteStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ContestExists(int id)
        {
            return _context.Contests.Any(e => e.ContestId == id);
        }
    }
}
