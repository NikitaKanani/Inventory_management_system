    using Inventory_management_system.Areas.MST_Product.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Inventory_management_system.Areas.MST_Company.Models;
using Inventory_management_system.Areas.MST_Category.Models;
using Inventory_management_system.BAL;

namespace Inventory_management_system.Areas.MST_Product.Controllers
{
    [CheckAccess]
    [Area("MST_Product")]
    [Route("{controller}/{action}")]
    public class ProductController : Controller
    {
        public IConfiguration Configuration;
        public ProductController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult ProductList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection sqlconnection = new SqlConnection(connectionstr);
            sqlconnection.Open();
            SqlCommand sqlCommand = sqlconnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Product_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View("ProductList", dt);
        }


        public IActionResult ProductSelectByID(int ProductID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Product_SelectByPk";
            ObjCmd.Parameters.AddWithValue("ProductID", ProductID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }

        public IActionResult ProductAddEditMethod(ProductModel model, int ProductID = 0)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.Parameters.AddWithValue("ProductName", model.ProductName);
            ObjCmd.Parameters.AddWithValue("CompanyID", model.CompanyID);
            ObjCmd.Parameters.AddWithValue("CategoryID", model.CategoryID);
            ObjCmd.Parameters.AddWithValue("PurchasePrice", model.PurchasePrice);
            ObjCmd.Parameters.AddWithValue("TexAmount", model.TexAmount);
            ObjCmd.Parameters.AddWithValue("SellingPrice", model.SellingPrice);
            ObjCmd.Parameters.AddWithValue("Description", model.Description);
            ObjCmd.Parameters.AddWithValue("Availables", model.Availables);
            if (model.ProductID == null || model.ProductID == 0)
            {
                ObjCmd.CommandText = "PR_Product_Insert";
                TempData["Message"] = "Record Inserted Successfully";
            }
            else
            {
                ObjCmd.CommandText = "PR_Product_Update";
                ObjCmd.Parameters.AddWithValue("ProductID", model.ProductID);
                TempData["Message"] = "Record Updated Successfully";
            }
            ObjCmd.ExecuteNonQuery();
            return RedirectToAction("ProductList");
        }

            public IActionResult ProductAddEdit(int ProductID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();

/*for Comapny name */
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Company_SelectAll";
            SqlDataReader Op_sqlDataReader = sqlCommand.ExecuteReader();
            DataTable Op_dt = new DataTable();
            Op_dt.Load(Op_sqlDataReader);
            List<CompanyModel> li = new List<CompanyModel>();
            foreach (DataRow dr in Op_dt.Rows)
            {
                CompanyModel obj = new CompanyModel();
                obj.CompanyID = int.Parse(dr["CompanyID"].ToString());
                obj.CompanyName = dr["CompanyName"].ToString();
                li.Add(obj);

            }
            ViewBag.Companylist = li;



 /*for Category name */
            SqlCommand sqlCommand2 = sqlConnection.CreateCommand();
            sqlCommand2.CommandType = CommandType.StoredProcedure;
            sqlCommand2.CommandText = "PR_Category_SelectAll";
            SqlDataReader Op_sqlDataReader2 = sqlCommand2.ExecuteReader();
            DataTable Op_dt2 = new DataTable();
            Op_dt2.Load(Op_sqlDataReader2);
            List<CategoryModel> li2 = new List<CategoryModel>();
            foreach (DataRow dr in Op_dt2.Rows)
            {
                CategoryModel obj = new CategoryModel();
                obj.CategoryID = int.Parse(dr["CategoryID"].ToString());
                obj.CategoryName = dr["CategoryName"].ToString();
                li2.Add(obj);

            }
            ViewBag.Categorylist = li2;






            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Product_SelectByPk";
            ObjCmd.Parameters.AddWithValue("ProductID", ProductID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            ProductModel model = new ProductModel();
            foreach (DataRow dr in dt.Rows)
            {
                model.ProductID = int.Parse(dr["ProductID"].ToString());
                model.ProductName = dr["ProductName"].ToString();
                model.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                model.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                model.PurchasePrice = (float)Convert.ToDecimal(dr["PurchasePrice"]);
                model.TexAmount = (float)Convert.ToDecimal(dr["TexAmount"]);
                model.SellingPrice = (float)Convert.ToDecimal(dr["SellingPrice"]);
                model.Description = dr["Description"].ToString();
                model.Availables = Convert.ToInt32(dr["Availables"]);
            }
            return View(model);
        }
    }
}
