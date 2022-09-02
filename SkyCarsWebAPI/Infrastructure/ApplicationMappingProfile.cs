using AutoMapper;
using System.Linq;

using SkyCars.Core.DomainEntity.User;
using SkyCarsWebAPI.Models;
using SkyCars.Core.DomainEntity.CouponCategory;
using SkyCars.Core.DomainEntity.Coupon;
using SkyCars.Core.DomainEntity.Customer;
using SkyCars.Core.DomainEntity.Discount;

namespace SkyCarsWebAPI.Infrastructure
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {

            //#region :: Settings ::           
            //CreateMap<SystemMessageModel, SystemMessage>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<SystemSettingModel, Settings>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //#endregion

            #region :: Settings ::      
            CreateMap<UserModel, User>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
           CreateMap<CouponeCategoryModel, CouponCategoryDomain>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<CouponModel, CouponDomain>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Customer, CustomerModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DiscountModel, DiscountDomain>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            #endregion
        }
    }
}
