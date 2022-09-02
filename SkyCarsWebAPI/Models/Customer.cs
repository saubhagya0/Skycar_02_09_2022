using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace SkyCarsWebAPI.Models
{
    public class Customer: BaseModel
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter NameOnLicence")]
        [StringLength(15,MinimumLength =3,ErrorMessage ="Name must be between 3 to 15")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string NameOnLicence { get; set; }

        [Required(ErrorMessage = "Please Enter DOB")]
        
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Please Enter LicenseNumber")]
        [MaxLength(10,ErrorMessage = "LicenseNumber must be 10 Length")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Please Enter ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Please Enter VehicleClass")]
        public string VehicleClass { get; set; }

        [Required(ErrorMessage = "Please Select LicenseImageFront")]
        public IFormFile LicenseImageFront { get; set; }

        [Required(ErrorMessage = "Please Select LicenseImageBack")]
        public string LicenseImageBack { get; set; }

        
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
