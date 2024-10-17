using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiShowManagementSystem.Data.Models;
using Newtonsoft.Json;
using KoiShowManagementSystem.Service;
using KoiShowManagementSystem.Common;
using KoiShowManagementSystem.Service.Base;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace KoiShowManagementSystem.MVCWebApp.Controllers
{
    public class ResultsController : Controller
    {
        private readonly FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext _context;

        public ResultsController(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Judge>> GetJudges()
        {
            var res = new List<Judge>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Judges"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            res = JsonConvert.DeserializeObject<List<Judge>>(result.Data.ToString());
                        }
                    }
                }
            }

            return res;
        }

        public async Task<IActionResult> ResultIndex()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Results"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if(result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Result>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<Result>());

            //var fA24_SE1717_PRN231_G2_KoiShowManagementSystemContext = _context.Results.Include(r => r.Judges);
            //var tmp = fA24_SE1717_PRN231_G2_KoiShowManagementSystemContext.ToListAsync();
            //return View(await fA24_SE1717_PRN231_G2_KoiShowManagementSystemContext.ToListAsync());
        }

        public async Task<IActionResult> ResultDetails(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Results/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Result>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Result());
        }


        public async Task<IActionResult> ResultCreate()
        {
            ViewData["JudgesId"] = new SelectList(await this.GetJudges(), "JudgesId", "JudgesId");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ResultId,CriteriaId,JudgesId,KoiId,Score,Rank,Comment,EvaluationDate,Disqualification,Prize")] Result result)
        public async Task<IActionResult> ResultCreate(Result result)
        {
            {
                bool saveStatus = false;

                #region Saving
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "Results/", result))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var resu = JsonConvert.DeserializeObject<BusinessResult>(content);

                                if (resu != null && resu.Status == Const.SUCCESS_CREATE_CODE)
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
                #endregion

                if (saveStatus)
                {
                    return RedirectToAction(nameof(ResultIndex));
                }
                else
                {
                    ViewData["JudgesId"] = new SelectList(await this.GetJudges(), "JudgesId", "JudgesId", result.JudgesId);
                    return View(result);
                }
            }
        }

        public async Task<IActionResult> ResultEdit(int id)
        {
            var resu = new Result();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Results/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            resu = JsonConvert.DeserializeObject<Result>(result.Data.ToString());
                        }
                    }
                }
            }

            ViewData["JudgesId"] = new SelectList(await this.GetJudges(), "JudgesId", "JudgesId", resu.JudgesId);
            return View(resu);
        }

        #region code chatgpt
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Tìm dữ liệu của Result từ database
            var resu = await _context.Results
                .Include(r => r.Judges)  // Bao gồm cả dữ liệu từ bảng Judges liên quan
                .FirstOrDefaultAsync(m => m.ResultId == id);

            if (resu == null)
            {
                return NotFound();
            }

            // Lấy danh sách Judges để đổ vào dropdown
            ViewData["JudgesId"] = new SelectList(await this.GetJudges(), "JudgesId", "Name", resu.JudgesId);

            // Trả dữ liệu về view để hiển thị
            return View(resu);
        }*/
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ResultId,CriteriaId,JudgesId,KoiId,Score,Rank,Comment,EvaluationDate,Disqualification,Prize")] Result result)
        public async Task<IActionResult> ResultEdit(int id, Result result)
        {
            bool saveStatus = false;

            #region Saving
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "Results/", result))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var resu = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (resu != null && resu.Status == Const.SUCCESS_UPDATE_CODE)
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
            #endregion

            if (saveStatus)
            {
                return RedirectToAction(nameof(ResultIndex));
            }
            else
            {
                /*                var res = new List<Result>();

                                using (var httpClient = new HttpClient())
                                {
                                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Results"))
                                    {
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var content = await response.Content.ReadAsStringAsync();
                                            var re = JsonConvert.DeserializeObject<BusinessResult>(content);

                                            if (re != null && re.Data != null)
                                            {
                                                var data = JsonConvert.DeserializeObject<List<Result>>(re.Data.ToString());
                                            }
                                        }
                                    }
                                }
                            }*/

                ViewData["JudgesId"] = new SelectList(await this.GetJudges(), "JudgesId", "JudgesId", result.JudgesId);
                return View(result);

                #region code chatgpt
                /*if (id != result.ResultId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        // Cập nhật dữ liệu trong context
                        _context.Update(result);
                        await _context.SaveChangesAsync();  // Lưu thay đổi vào database
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ResultExists(result.ResultId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    // Chuyển hướng sau khi cập nhật thành công
                    return RedirectToAction(nameof(Index));
                }

                // Nếu có lỗi, hiển thị lại view với dữ liệu
                ViewData["JudgesId"] = new SelectList(await this.GetJudges(), "JudgesId", "Name", result.JudgesId);
                return View(result);*/
                #endregion
            }
        }

        public async Task<IActionResult> ResultDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Results/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                           var resu = JsonConvert.DeserializeObject<Result>(result.Data.ToString());

                            return View(resu);
                        }
                    }
                }
            }

            return View(new Result());
        }


        [HttpPost, ActionName("ResultDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResultDeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            if(ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "Results/" + id))
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
                return RedirectToAction(nameof(ResultIndex));
            }
            else
            {
                return RedirectToAction(nameof(ResultDelete));
            }
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.ResultId == id);
        }
    }
}
