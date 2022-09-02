using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyCars.Core.DomainEntity.Customer;
using SkyCars.Core.DomainEntity.Grid;
using SkyCars.Service.Customers;
using SkyCarsWebAPI.Extensions;
using SkyCarsWebAPI.Models;
using SkyCarsWebAPI.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SkyCarsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        #region Fields

        private static IWebHostEnvironment _Iwebhostenvironment;


        private readonly ICustomer _CustomerService;

        #endregion

        #region Ctor

        public CustomerController(ICustomer CustomerService, IWebHostEnvironment webHost)
        {
            _CustomerService = CustomerService;
            _Iwebhostenvironment = webHost;

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
        public async Task<ApiResponse> Post([FromForm]Customer model)
        {
            if (await _CustomerService.IsNameExist(model.LicenseNumber, model.Id))
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "User name already exists.");

            if (model.Id == 0)
            {
                //if (model.LicenseImageFront.Length > 0)
                //{

                //        if (!Directory.Exists(_Iwebhostenvironment.WebRootPath + "\\Images\\"))
                //        {
                //            Directory.CreateDirectory(_Iwebhostenvironment.WebRootPath + "\\Images\\");
                //        }
                //        using(FileStream  file=System.IO.File.Create(_Iwebhostenvironment.WebRootPath +"\\Images\\" + model.LicenseImageFront.FileName))
                //        {
                //            model.LicenseImageFront.CopyTo(file);
                //            file.Flush();
                //            await _CustomerService.InsertAsync(model.MapTo<CustomerModel>(), CurrentUserId, CurrentUserName);
                //        }
                //    }
                if (model.LicenseImageFront.Length > 0)
                {

                    if (!Directory.Exists(_Iwebhostenvironment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(_Iwebhostenvironment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_Iwebhostenvironment.WebRootPath + "\\uploads\\" + model.LicenseImageFront.FileName))
                    {
                        model.LicenseImageFront.CopyTo(filestream);
                        filestream.Flush();
                        await _CustomerService.InsertAsync(model.MapTo<CustomerModel>(), CurrentUserId, CurrentUserName);
                    }
                }
                   
            }
            else
            {
                await _CustomerService.UpdateAsync(model.MapTo<CustomerModel>(), CurrentUserId, CurrentUserName);
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

