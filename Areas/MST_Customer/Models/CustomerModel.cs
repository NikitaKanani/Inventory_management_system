using System.ComponentModel.DataAnnotations;

namespace Inventory_management_system.Areas.MST_Customer.Models
{
    public class CustomerModel
    {
        [Required]
        public int CustomerID { get; set; }



        [Required(ErrorMessage = "Customer Name is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Customer Name must not contain numbers.")]
        public string CustomerName { get; set; }


        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact Number must be 10 digit long.")]
        public string ContactNumber { get; set; }



        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be in proper format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        
       
    }
}
