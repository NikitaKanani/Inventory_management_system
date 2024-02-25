using Inventory_management_system.Areas.MST_Category.Models;
using System.ComponentModel.DataAnnotations;

namespace Inventory_management_system.Areas.MST_Product.Models
{
    public class ProductModel
    {
        [Required]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "ProductName is required.")]
        public string? ProductName { get; set; }


        [Required(ErrorMessage = "CompanyName is required.")]
        public int   CompanyID { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "CategoryName is required.")]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "PurchasePrice is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "PurchasePrice must be greater than or equal to 0.")]
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "PurchasePrice must be a valid positive number.")]
        public float? PurchasePrice { get; set; }


        [Required(ErrorMessage = "TexAmount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "TexAmount must be greater than or equal to 0.")]
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "TexAmount must be a valid positive number.")]
        public float? TexAmount { get; set; }


        [Required(ErrorMessage = "SellingPrice is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "SellingPrice must be greater than or equal to 0.")]
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "SellingPrice must be a valid positive number.")]
        public float? SellingPrice { get; set; }

        [Required]
        public string Description { get; set; }


        [Required(ErrorMessage = "Availables is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Availables must be greater than or equal to 0.")]
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Availables must be a valid positive number.")]
        public int? Availables { get; set; }

    }
}

                         