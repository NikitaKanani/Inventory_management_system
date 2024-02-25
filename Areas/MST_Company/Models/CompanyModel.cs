using System.ComponentModel.DataAnnotations;

namespace Inventory_management_system.Areas.MST_Company.Models
{
    public class CompanyModel
    {
        [Required]
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Company Name is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Company Name must not contain numbers.")]
        public string CompanyName { get; set; }


        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digit long.")]
        public string ContactNumber { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be in proper format")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

    }
}
