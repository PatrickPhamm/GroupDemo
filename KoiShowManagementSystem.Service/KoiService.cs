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
    public interface IKoiBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int koiId);
        Task<IBusinessResult> Update(Koi koi);
        Task<IBusinessResult> Save(Koi koi);
        Task<IBusinessResult> DeleteById(int koiId);
    }
    public class KoiService : IKoiBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public KoiService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> DeleteById(int koiId)
        {
            try
            {
                var kois = await _unitOfWork.koiRepository.GetByIdAsync(koiId);
                if (kois == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Koi());
                }
                else
                {
                    var result = await _unitOfWork.koiRepository.RemoveAsync(kois);
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
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            var kois = await _unitOfWork.koiRepository.GetAllAsync();
            if (kois == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Koi>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, kois);
            }
        }

        public async Task<IBusinessResult> GetById(int koiId)
        {
            var kois = await _unitOfWork.koiRepository.GetByIdAsync(koiId);
            if (kois == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Koi());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, kois);
            }
        }

        //public async Task<IBusinessResult> Save(Koi koi)
        //{
        //    try
        //    {
        //        int result = -1;
        //        var koiTemp = _unitOfWork.koiRepository.GetById(koi.KoiId);
        //        if (koiTemp != null)
        //        {
        //            result = await _unitOfWork.koiRepository.UpdateAsync(koiTemp);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, koi);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        //            }
        //        }
        //        else
        //        {
        //            result = await _unitOfWork.koiRepository.CreateAsync(koi);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, koi);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, koi);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        //    }

        //}
        public async Task<IBusinessResult> Update(Koi koi)
        {
            try
            {
                var kois = await _unitOfWork.koiRepository.UpdateAsync(koi);

                if (kois > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, kois);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
        public async Task<IBusinessResult> Save(Koi koi)
        {
            try
            {
                int result = await _unitOfWork.koiRepository.CreateAsync(koi);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, koi);
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
    }
}
