using System.ComponentModel.DataAnnotations;

namespace Inventory_management_system.Areas.MST_Discount.Models
{
    public class DiscountModel
    {
        [Required]
        public int DiscountID { get; set; }


        [Required(ErrorMessage = "Product Name is required.")]
        public int ProductID { get; set; }


        public string ProductName{ get; set; }


        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to 0.")]
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Amount must be a valid positive number.")]
        public float? Amount { get; set; }


        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime? StartDate { get; set; }


        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime? EndDate { get; set; }


        
         
    } 
}
