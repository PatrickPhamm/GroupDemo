using KoiShowManagementSystem.Common;
using KoiShowManagementSystem.Data;
using KoiShowManagementSystem.Data.Models;
using KoiShowManagementSystem.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KoiShowManagementSystem.Service
{
    public interface IJudgesCriteriaService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int JCId);
        Task<IBusinessResult> Update(JudgesCriterion JC);
        Task<IBusinessResult> Save(JudgesCriterion JC);
        Task<IBusinessResult> DeleteById(int JCId);
    }

    public class JudgesCriteriaService : IJudgesCriteriaService
    {
        private readonly UnitOfWork _unitOfWork;

        public JudgesCriteriaService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule

            #endregion
            var JC = await _unitOfWork.JudgesCriteriaRepo.GetAllAsync();

            if (JC == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<JudgesCriterion>());
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, JC);
            }
        }

        public async Task<IBusinessResult> GetById(int JCId)
        {
            #region Business Rule
            #endregion
            var JudgesCriteriaService = await _unitOfWork.JudgesCriteriaRepo.GetByIdAsync(JCId);
            if (JudgesCriteriaService == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new JudgesCriteriaService());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, JudgesCriteriaService);
            }
        }

        public async Task<IBusinessResult> Update(JudgesCriterion JC)
        {
            try
            {
                var JCs = await _unitOfWork.JudgesCriteriaRepo.UpdateAsync(JC);

                if (JCs > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, JCs);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Save(JudgesCriterion JC)
        {
            try
            {
                int result = -1;

                var JCTmp = _unitOfWork.JudgesCriteriaRepo.GetById(JC.CriteriaId);

                if (JCTmp != null)
                {
                    #region Business Rule
                    #endregion Business Rule

                    result = await _unitOfWork.JudgesCriteriaRepo.UpdateAsync(JC);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, JC);
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
                    result = await _unitOfWork.JudgesCriteriaRepo.CreateAsync(JC);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, JC);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, JC);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
        
        public async Task<IBusinessResult> DeleteById(int JCId)
        {
            #region Business Rule

            #endregion

            try
            {


                var JC = await _unitOfWork.JudgesCriteriaRepo.GetByIdAsync(JCId);

                if (JC == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new JudgesCriterion());
                }
                else
                {
                    var result = await _unitOfWork.JudgesCriteriaRepo.RemoveAsync(JC);

                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, JC);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, JC);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message, new JudgesCriteriaService());
            }
        }
    }
}
