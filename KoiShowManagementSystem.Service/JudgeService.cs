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
    public interface IJudgeService
    {
        Task<IBusinessResult> GetAllJudges();
        Task<IBusinessResult> GetJudgeById(int JudgeId);
        Task<IBusinessResult> Save(Judge jud);
    }

    public class JudgeService : IJudgeService
    {
        private readonly UnitOfWork _unitOfWork;
        public JudgeService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAllJudges()
        {
            /*try
            {
                var Jud = await _unitOfWork.judgeRepository.GetAllAsync();
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Jud);


            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có ngoại lệ
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }*/

            var jud = await _unitOfWork.judgeRepository.GetAllAsync();

            if (jud == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Judge>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, jud);
            }
        }

        public async Task<IBusinessResult> GetJudgeById(int JudgeId)
        {
            /*try
            {
                //var Jud = _unitOfWork.judgeRepository.Get(o => o.JudgesId == JudgeId);
                var Jud = _unitOfWork.judgeRepository.GetById(JudgeId);
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Jud);
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có ngoại lệ
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }*/

            var jud = await _unitOfWork.judgeRepository.GetByIdAsync(JudgeId);

            if (jud == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Judge());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, jud);
            }
        }

        public async Task<IBusinessResult> Save(Judge jud)
        {
            try
            {
                int judTemp = await _unitOfWork.judgeRepository.CreateAsync(jud);

                if (judTemp > 0)
                {
                    #region Business Rule
                    #endregion Business Rule

                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, jud);
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
    }
}
