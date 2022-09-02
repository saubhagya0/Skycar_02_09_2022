using System;
using System.ComponentModel.DataAnnotations;

namespace SkyCarsWebAPI.Models
{
    public class DiscountModel:BaseModel
    {
        [Required(ErrorMessage = "Please eneter DiscountTypeName")]
        public string? DiscountTypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
