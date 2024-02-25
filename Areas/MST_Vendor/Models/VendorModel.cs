using System.ComponentModel.DataAnnotations;

namespace Inventory_management_system.Areas.MST_Vendor.Models
{
    public class VendorModel
    {
        [Required]
        public int VendorID { get; set; }

        [Required(ErrorMessage = "VendorName is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Vendor Name must not contain numbers.")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "CompanyName is required.")]
        public int CompanyID { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "ContactNumber is required.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact Number must be 10 digit long.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be in proper format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

    }
}

