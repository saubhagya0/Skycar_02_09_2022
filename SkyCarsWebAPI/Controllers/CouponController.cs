using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyCars.Core.DomainEntity.Coupon;
using SkyCars.Core.DomainEntity.Grid;
using SkyCars.Service.Coupon;
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
    public class CouponController : BaseController
    {
        #region Fields

        private readonly ICoupon _CouponService;

        #endregion

        #region Ctor

        public CouponController(ICoupon UserService)
        {
            _CouponService = UserService;
        }

        #endregion

        [HttpPost]
        [Route("[action]")]
        public async Task<ApiResponse> FiltersData(GridRequestModel objGrid)
        {
            var DocList = await _CouponService.GetAllAsync(objGrid);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "User", DocList.ToGrid());
        }

        [HttpGet("{id?}")]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No Data Found");

            var data = await _CouponService.GetByIdAsync(id);
            return ApiResponseHelper.GenerateResponse(data.GetApiResponseStatusCodeByData(), data is null ? "NoDataFound" : string.Format("User"), data);
        }
        [HttpPost]
        public async Task<ApiResponse> Post(CouponModel model)
        {
            //if (await _CouponService.IsNameExist(model.UserName, model.Id))
            //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "User name already exists.");

            if (model.Id == 0)
            {
                await _CouponService.InsertAsync(model.MapTo<CouponDomain>(), CurrentUserId, CurrentUserName);
            }
            else
            {
                await _CouponService.UpdateAsync(model.MapTo<CouponDomain>(), CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.Id == 0 ? "User saved successfully." : "User updated successfully.");
        }

        [HttpDelete]
        public async Task<ApiResponse> Delete(IList<int> Ids)
        {
            if (Ids.Count == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status422UnprocessableEntity, "Invalid Request Parmeters");

            var obj = await _CouponService.GetByIdsAsync(Ids).ConfigureAwait(false);
            if (obj == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status422UnprocessableEntity, "No Data Found");

            obj.ToList().ForEach(s => s.IsDelete = true);
            await _CouponService.UpdateAsync(obj, CurrentUserId, CurrentUserName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "User daleted successfully.");

        }

    }
}
