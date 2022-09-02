using System;
using System.ComponentModel.DataAnnotations;

namespace SkyCarsWebAPI.Models
{
    public class CouponeCategoryModel:BaseModel
    {
        [Required(ErrorMessage = "Please eneter CategoryName")]
        public string CategoryName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
