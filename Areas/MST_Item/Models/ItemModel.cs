namespace Inventory_management_system.Areas.MST_Item.Models
{
    public class ItemModel
    {
        public int ItemID { get; set; }

        public int CompanyID { get; set; }

        public int CategoryID { get; set; }

        public string ComapnyName { get; set; }

        public string CategoryName{ get; set; }

        public string SerialNumber { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public DateTime PurchaseDate { get; set; }
         

    }
}
