using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace Inventory_management_system.Areas.SEC_User.Models
{
    public class SEC_UserModel
    {

        public int? UserId { get; set; }


        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digit long.")]
        public string MobileNo { get; set; } 


        [Required(ErrorMessage = "FirstName is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must not contain numbers.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name must not contain numbers.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be in proper format")]
        public string Email { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
    public class UserLoginModel
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
