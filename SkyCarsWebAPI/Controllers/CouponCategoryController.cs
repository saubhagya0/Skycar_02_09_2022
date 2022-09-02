using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyCars.Core.DomainEntity.CouponCategory;
using SkyCars.Core.DomainEntity.Grid;
using SkyCars.Service.CouponCategory;
using SkyCarsWebAPI.Extensions;
using SkyCarsWebAPI.Models;
using SkyCarsWebAPI.Models.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyCarsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponCategoryController : BaseController
    {

        #region Fields

        private readonly ICouponCategory _CustomerService;

        #endregion

        #region Ctor

        public CouponCategoryController(ICouponCategory CustomerService)
        {
            _CustomerService = CustomerService;
        }

        #endregion

        [HttpPost]
        [Route("[action]")]
        public async Task<ApiResponse> FiltersData(GridRequestModel objGrid)
        {
            var DocList = await _CustomerService.GetAllAsync(objGrid);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Customer", DocList.ToGrid());
        }

        [HttpGet("{id?}")]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No Data Found");

            var data = await _CustomerService.GetByIdAsync(id);
            return ApiResponseHelper.GenerateResponse(data.GetApiResponseStatusCodeByData(), data is null ? "NoDataFound" : string.Format("User"), data);
        }
        [HttpPost]
        //public async Task<ApiResponse> Post(CustomerModel model)
        //{
        //    if (await _CustomerService.IsNameExist(model.LicenseNumber, model.Id))
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "User name already exists.");

        //    if (model.Id == 0)
        //    {
        //        await _CustomerService.InsertAsync(model.MapTo<CustomerModel>(), CurrentUserId, CurrentUserName);


        //    else
        //        {
        //            await _CustomerService.UpdateAsync(model.MapTo<CustomerModel>(), CurrentUserId, CurrentUserName);
        //        }
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.Id == 0 ? "User saved successfully." : "User updated successfully.");
        //    }



        //}
        [HttpPost]
        public async Task<ApiResponse> Post(CouponeCategoryModel model)
        {
            //if (await _CustomerService.IsNameExist(model.LicenseNumber, model.Id))
            //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "User name already exists.");

            if (model.Id == 0)
            {
                await _CustomerService.InsertAsync(model.MapTo<CouponCategoryDomain>(), CurrentUserId, CurrentUserName);
            }
            else
            {
                await _CustomerService.UpdateAsync(model.MapTo<CouponCategoryDomain>(), CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.Id == 0 ? "User saved successfully." : "User updated successfully.");
        }
        [HttpDelete]
        public async Task<ApiResponse> Delete(IList<int> Ids)
        {
            if (Ids.Count == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status422UnprocessableEntity, "Invalid Request Parmeters");

            var obj = await _CustomerService.GetByIdsAsync(Ids).ConfigureAwait(false);
            if (obj == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status422UnprocessableEntity, "No Data Found");

            obj.ToList().ForEach(s => s.IsDelete = true);
            await _CustomerService.UpdateAsync(obj, CurrentUserId, CurrentUserName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "User daleted successfully.");

        }
    }
}
