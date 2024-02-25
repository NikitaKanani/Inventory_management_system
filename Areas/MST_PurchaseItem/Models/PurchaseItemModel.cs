using System.ComponentModel.DataAnnotations;

namespace Inventory_management_system.Areas.MST_PurchaseItem.Models
{
    public class PurchaseItemModel
    {

        [Required]
        public int PurchaseItemID { get; set; }


        [Required]
        public int PurchaseID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public float? Price { get; set; }

        [Required]
        public float? Tax { get; set; }

        [Required]

        public int CompanyID { get; set; }

        [Required]

        public string CompanyName { get; set; }


        [Required]
        public int CategoryID { get; set; }





    }
}




