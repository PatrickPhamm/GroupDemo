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
    public interface IApplicationBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetApplicationById(int applicationId);
        Task<IBusinessResult> Save(Application application);
    }
    public class ApplicationService : IApplicationBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public ApplicationService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            var application = await _unitOfWork.applicationRepository.GetAllAsync();
            if (application == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Application>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, application);
            }
        }

        public async Task<IBusinessResult> GetApplicationById(int applicationId)
        {
            var application = await _unitOfWork.applicationRepository.GetByIdAsync(applicationId);

            if (application == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Application());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, application);
            }
        }

        public async Task<IBusinessResult> Save(Application application)
        {
            try
            {
                int result = await _unitOfWork.applicationRepository.CreateAsync(application);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, application);
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
