using System.ComponentModel.DataAnnotations;
namespace Inventory_management_system.Areas.MST_Category.Models
{
    public class CategoryModel
    {
        [Required]
        public int CategoryID { get; set; }


        [Required(ErrorMessage = "CategoryName is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Category Name must not contain numbers.")]
        public string CategoryName { get; set; }
    }
}
