using System;
using System.ComponentModel.DataAnnotations;

namespace SkyCarsWebAPI.Models
{
    public class CouponModel:BaseModel
    {
        [Required(ErrorMessage ="Please Select CouponName")]
        public string? CouponName { get; set; }

        [Required(ErrorMessage = "Please Apply CouponCode(option)")]
        public string? CouponCode { get; set; }
        [Required(ErrorMessage = "Please Enter  DiscountValue")]
        public decimal DiscountValue { get; set; }
        [Required(ErrorMessage = "Please Enter  StartDate")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please Enter  EndDate")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Please Enter  MaxBookingAmount")]
        public decimal MaxBookingAmount { get; set; }
        [Required(ErrorMessage = "Please Enter  MinBookingAmount")]
        public decimal MinBookingAmount { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
