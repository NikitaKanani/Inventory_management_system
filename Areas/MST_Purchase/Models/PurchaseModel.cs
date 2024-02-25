namespace Inventory_management_system.Areas.MST_Purchase.Models
{
    public class PurchaseModel
    {
        public int PurchaseID { get; set; }

        public int VendorID { get; set; }

        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string VendorName { get; set; }

        public DateTime PurchaseDate { get; set; }

        public float TotalAmount { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime DueDate { get; set; }
    }
}
