using Inventory_management_system.Areas.MST_Category.Models;
using Inventory_management_system.Areas.MST_Company.Models;
using Inventory_management_system.Areas.MST_Item.Models;
using Inventory_management_system.Areas.MST_Product.Models;
using Inventory_management_system.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Inventory_management_system.Areas.MST_Item.Controllers
{

    [CheckAccess]
    [Area("MST_Item")]
    [Route("{controller}/{action}")]
    public class ItemController : Controller
    {
        public IConfiguration Configuration;
        public ItemController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult ItemList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection sqlconnection = new SqlConnection(connectionstr);
            sqlconnection.Open();
            SqlCommand sqlCommand = sqlconnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Item_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View("ItemList", dt);
        }

        public IActionResult ItemSelectByID(int ItemID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Item_SelectByPk";
            ObjCmd.Parameters.AddWithValue("ItemID", ItemID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }

        public IActionResult ItemAddEdit(int ItemID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();


            SqlCommand sqlCommand_Company = sqlConnection.CreateCommand();
            sqlCommand_Company.CommandType = CommandType.StoredProcedure;
            sqlCommand_Company.CommandText = "PR_Company_SelectAll";
            SqlDataReader sqlDataReader_Company = sqlCommand_Company.ExecuteReader();
            DataTable dt_Company = new DataTable();
            dt_Company.Load(sqlDataReader_Company);

            List<CompanyModel> li_Company = new List<CompanyModel>();

            foreach (DataRow dr in dt_Company.Rows)
            {
                CompanyModel obj_Company = new CompanyModel();
                obj_Company.CompanyID = int.Parse(dr["CompanyID"].ToString());
                obj_Company.CompanyName = dr["CompanyName"].ToString();
                li_Company.Add(obj_Company);
            }
            ViewBag.Companylist = li_Company;



            SqlCommand sqlCommand_Product = sqlConnection.CreateCommand();
            sqlCommand_Product.CommandType = CommandType.StoredProcedure;
            sqlCommand_Product.CommandText = "PR_Product_SelectAll";
            SqlDataReader sqlDataReader_Product = sqlCommand_Product.ExecuteReader();
            DataTable dt_Product = new DataTable();
            dt_Product.Load(sqlDataReader_Product);

            List<ProductModel> li_Product = new List<ProductModel>();

            foreach (DataRow dr in dt_Product.Rows)
            {
                ProductModel obj_Product = new ProductModel();
                obj_Product.ProductID = int.Parse(dr["ProductID"].ToString());
                obj_Product.ProductName = dr["ProductName"].ToString();
                li_Product.Add(obj_Product);
            }
            ViewBag.ProductList = li_Product;



            SqlCommand sqlCommand_Category = sqlConnection.CreateCommand();
            sqlCommand_Category.CommandType = CommandType.StoredProcedure;
            sqlCommand_Category.CommandText = "PR_Category_SelectAll";
            SqlDataReader sqlDataReader_Category = sqlCommand_Category.ExecuteReader();
            DataTable dt_Category = new DataTable();
            dt_Category.Load(sqlDataReader_Category);

            List<CategoryModel> li_Category = new List<CategoryModel>();

            foreach (DataRow dr in dt_Category.Rows)
            {
                CategoryModel obj_Category = new CategoryModel();
                obj_Category.CategoryID = int.Parse(dr["CategoryID"].ToString());
                obj_Category.CategoryName = dr["CategoryName"].ToString();
                li_Category.Add(obj_Category);
            }
            ViewBag.CategoryList = li_Category;





            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Item_SelectByPk";
            ObjCmd.Parameters.AddWithValue("ItemID", ItemID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            ItemModel model = new ItemModel();
            foreach (DataRow dr in dt.Rows)
            {
                model.ItemID = int.Parse(dr["ItemID"].ToString());
                model.ProductID = Convert.ToInt32(dr["ProductID"]);
                model.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                model.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                model.SerialNumber = dr["ItemName"].ToString();
                model.ProductID = Convert.ToInt32(dr["ProductID"]);
            }
            return View("ItemAddEdit", model);
        }

        public IActionResult ItemAddEditMethod(ItemModel model)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.Parameters.AddWithValue("SerialNumber", model.SerialNumber);
            ObjCmd.Parameters.AddWithValue("ProductID", model.ProductID);
            if (model.ItemID == null || model.ItemID == 0)
            {
                ObjCmd.CommandText = "PR_Item_Insert";
                TempData["Message"] = "Record Inserted Successfully";
            }
            else
            {
                ObjCmd.CommandText = "PR_Item_Update";
                ObjCmd.Parameters.AddWithValue("ItemID", model.ItemID);
                TempData["Message"] = "Record Updated Successfully";
            }
            ObjCmd.ExecuteNonQuery();
            return RedirectToAction("ItemList");
        }
        public IActionResult ProductsForComboBox(int CompanyID)
        {
            SqlConnection connection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

            //connection open
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Product_ProductByCompanyID";
            command.Parameters.AddWithValue("CompanyID", CompanyID);



            SqlDataReader reader = command.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            connection.Close();

            List<ProductModel> ProductList = new List<ProductModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductModel productModel = new ProductModel();
                productModel.ProductID = int.Parse(dr["ProductID"].ToString());
                productModel.ProductName = dr["ProductName"].ToString();
                ProductList.Add(productModel);
            }

            ViewBag.ProductList = ProductList;

            return Json(ProductList);
        }

    }

}
