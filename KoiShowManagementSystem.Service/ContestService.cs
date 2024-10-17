using KoiShowManagementSystem.Common;
using KoiShowManagementSystem.Data;
using KoiShowManagementSystem.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiShowManagementSystem.Data.Models;

namespace KoiShowManagementSystem.Service
{
    public interface IContestService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetContestById(int contestId);
        Task<IBusinessResult> Save(Contest contest);
        Task<IBusinessResult> DeleteById(int code);
        Task<IBusinessResult> Update(Contest contest);
    }

    public class ContestService : IContestService
    {
        private readonly UnitOfWork _unitOfWork;
        public ContestService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> DeleteById(int code)
        {
            try
            {
                var kois = await _unitOfWork.contestRepository.GetByIdAsync(code);
                if (kois == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Contest());
                }
                else
                {
                    var result = await _unitOfWork.contestRepository.RemoveAsync(kois);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, kois);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, kois);
                    }

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_DELETE_CODE, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            var contest = await _unitOfWork.contestRepository.GetAllAsync();
            if (contest == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Contest>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, contest);
            }
        }

        public async Task<IBusinessResult> GetContestById(int contestId)
        {
            var contest = await _unitOfWork.contestRepository.GetByIdAsync(contestId);

            if (contest == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Contest());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, contest);
            }
        }

        public async Task<IBusinessResult> Save(Contest contest)
        {
            try
            {
                int result = await _unitOfWork.contestRepository.CreateAsync(contest);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, contest);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(Contest contest)
        {
            try
            {
                var result = await _unitOfWork.contestRepository.UpdateAsync(contest);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, result);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
    }
}
