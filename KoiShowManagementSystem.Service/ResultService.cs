using KoiShowManagementSystem.Common;
using KoiShowManagementSystem.Data;
using KoiShowManagementSystem.Data.Models;
using KoiShowManagementSystem.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShowManagementSystem.Service
{
    public interface IResultService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int resultId);
        //Task<IBusinessResult> Create(Result result);
        Task<IBusinessResult> Update(Result result);
        Task<IBusinessResult> Save(Result result);
        Task<IBusinessResult> DeleteById(int resultId);
        Task<IBusinessResult> Search(int? resultId, string medal, decimal? score);
    }

    public class ResultService : IResultService
    {
        private readonly UnitOfWork _unitOfWork;

        public ResultService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var results = await _unitOfWork.resultRepository.GetAllAsync();

            if(results == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Result>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, results);
            }
        }

        public async Task<IBusinessResult> GetById(int resultId)
        {
            var results = await _unitOfWork.resultRepository.GetByIdAsync(resultId);

            if (results == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Result());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, results);
            }
        }

        /*public async Task<IBusinessResult> Create(Result result)
        {
            var results = await _unitOfWork.resultRepository.CreateAsync(result);

            if (results == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, results);
            }
        }*/

        public async Task<IBusinessResult> Update(Result result)
        {
            try
            {
                //int results = await _unitOfWork.resultRepository.UpdateAsync(result);
                var results = await _unitOfWork.resultRepository.UpdateAsync(result);

                if (results > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, results);
                }
            }catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int resultId)
        {
            #region Business Rule
            #endregion

            try
            {
                var res = await _unitOfWork.resultRepository.GetByIdAsync(resultId);
                if (res == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Result());
                }
                else
                {
                    var resul = await _unitOfWork.resultRepository.RemoveAsync(res);
                    if (resul)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, res);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, res);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        /*public async Task<IBusinessResult> Save(Result result)
        {
            try
            {
                int res = -1;

                var resuTemp = _unitOfWork.resultRepository.GetById(result.ResultId);

                if (resuTemp != null)   
                {
                    #region Business Rule
                    #endregion

                    res = await _unitOfWork.resultRepository.UpdateAsync(result);

                    if(res > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, result);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    #region Business Rule
                    #endregion

                    res = await _unitOfWork.resultRepository.CreateAsync(result);

                    if (res > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, result);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, result);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message.ToString());
            }
        }*/

        public async Task<IBusinessResult> Save(Result result)
        {
            try
            {
                int resuTemp = await _unitOfWork.resultRepository.CreateAsync(result);

                if (resuTemp > 0)
                {
                    #region Business Rule
                    #endregion Business Rule

                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, result);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message.ToString());
            }
        }

        public async Task<IBusinessResult> Search(int? resultId, string medal, decimal? score)
        {
            try
            {
                var results = await _unitOfWork.resultRepository.GetAllAsync();
                var query = results.AsQueryable();

                if (resultId.HasValue)
                {
                    query = query.Where(r => r.ResultId == resultId);
                }

                if (!string.IsNullOrWhiteSpace(medal))
                {
                    query = query.Where(r => r.Medal.ToLower().Contains(medal.ToLower()));
                }

                if (score.HasValue)
                {
                    query = query.Where(r => r.Score == score);
                }

                var searchResults = query.ToList();

                if (!searchResults.Any())
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Result>());
                }

                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, searchResults);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
