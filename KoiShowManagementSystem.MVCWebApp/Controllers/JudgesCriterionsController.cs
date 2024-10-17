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
using System.Net.Http.Json;
using Azure;

namespace KoiShowManagementSystem.MVCWebApp.Controllers
{
    public class JudgesCriterionsController : Controller
    {
        private readonly FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext _context;
        public JudgesCriterionsController(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Contest>> GetContest()
        {
            var contests = new List<Contest>();
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
                            contests = JsonConvert.DeserializeObject<List<Contest>>(result.Data.ToString());

                        }
                    }
                }
            }
            return contests;
        }

        // GET: JudgesCriterions
        public async Task<IActionResult> JCIndex()
        {
            using (var httpClient = new HttpClient())   
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "JudgesCriterions")) //+ "JudgesCriterions"
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<JudgesCriterion>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<JudgesCriterion>());
        }

        // GET: JudgesCriterions/Details/5
        public async Task<IActionResult> JCDetails(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "JudgesCriterions/" + id))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<JudgesCriterion>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new JudgesCriterion());
        }

        // GET: JudgesCriterions/Create
        public async Task<IActionResult> JCCreate()
        {
            ViewData["ContestId"] = new SelectList(await this.GetContest(), "ContestId", "ContestId");
            return View();
        }

        // POST: JudgesCriterions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JCCreate(JudgesCriterion jc)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "JudgesCriterions/", jc))
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
                return RedirectToAction(nameof(JCIndex));
            }
            else
            {
                ViewData["ContestId"] = new SelectList(await this.GetContest(), "ContestId", "ContestId");
                return View(jc);
            }
        }

        // GET: JudgesCriterions/Edit/5
        public async Task<IActionResult> JCEdit(int id)
        {
            var jc = new JudgesCriterion();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "JudgesCriterions/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            jc = JsonConvert.DeserializeObject<JudgesCriterion>(result.Data.ToString());
                        }
                    }
                }
            }

            ViewData["ContestId"] = new SelectList(await this.GetContest(), "ContestId", "ContestId");
            return View(jc);
        }

        // POST: JudgesCriterions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JCEdit(int id, JudgesCriterion judgesCriterion)
        {
            bool saveStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "JudgesCriterions/", judgesCriterion))
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
                return RedirectToAction(nameof(JCIndex));
            }
            else
            {
                ViewData["ContestId"] = new SelectList(await this.GetContest(), "ContestId", "ContestId");
                return View(judgesCriterion);
            }

        }

        // GET: JudgesCriterions/Delete/5
        public async Task<IActionResult> JCDelete(int? id)
        {
            var jc = new JudgesCriterion();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "JudgesCriterions/" + id))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            jc = JsonConvert.DeserializeObject<JudgesCriterion>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ContestId"] = new SelectList(await this.GetContest(), "ContestId", "ContestId");
            return View(jc);
        }

        // POST: JudgesCriterions/Delete/5
        [HttpPost, ActionName("JCDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "JudgesCriterions/" + id))
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
                return RedirectToAction(nameof(JCIndex));
            }
            else
            {
                return RedirectToAction(nameof(JCDelete));
            }
        }

        private bool JudgesCriterionExists(int id)
        {
            return _context.JudgesCriteria.Any(e => e.CriteriaId == id);
        }

    }
}