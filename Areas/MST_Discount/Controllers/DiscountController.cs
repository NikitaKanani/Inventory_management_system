using Inventory_management_system.Areas.MST_Discount.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Inventory_management_system.Areas.MST_Company.Models;
using Inventory_management_system.Areas.MST_Product.Models;
using Inventory_management_system.BAL;

namespace Inventory_management_system.Areas.MST_Discount.Controllers
{

    [CheckAccess]
    [Area("MST_Discount")]
    [Route("{controller}/{action}")]
    public class DiscountController : Controller
    {
        public IConfiguration Configuration;
        public DiscountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult DiscountList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection sqlconnection = new SqlConnection(connectionstr);
            sqlconnection.Open();
            SqlCommand sqlCommand = sqlconnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Discount_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View("DiscountList", dt);
        }


        public IActionResult DiscountSelectByID(int DiscountID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Discount_SelectByPk";
            ObjCmd.Parameters.AddWithValue("DiscountID", DiscountID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }

        public IActionResult DiscountAddEditMethod(DiscountModel model, int DiscountID = 0)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            if (DiscountID == 0)
            {
                ObjCmd.CommandText = "PR_Discount_Insert";
                ObjCmd.Parameters.AddWithValue("ProductID", model.ProductID);
                ObjCmd.Parameters.AddWithValue("Amount", model.Amount);
                if (model.StartDate < DateTime.Today)
                {

                    TempData["StartDate"] = "StartDate must be today or futureDate";
                        /*
                    return RedirectToAction("DiscountAddEdit");*/
                }
                else
                {
                    ObjCmd.Parameters.AddWithValue("StartDate", model.StartDate);
                }
                if (model.StartDate > model.EndDate)
                {
                    TempData["EndDate"] = "EndDate must be greater than StartDate";/*
                    return RedirectToAction("DiscountAddEdit");*/
                }
                else
                {
                    ObjCmd.Parameters.AddWithValue("EndDate", model.EndDate);
                }
          
                TempData["Message"] = "Record Inserted Successfully";
            }
            else
            {
                ObjCmd.CommandText = "PR_Discount_Update";
                ObjCmd.Parameters.AddWithValue("DiscountID", DiscountID);
                ObjCmd.Parameters.AddWithValue("Amount", model.Amount);
                ObjCmd.Parameters.AddWithValue("StartDate", model.StartDate);
                ObjCmd.Parameters.AddWithValue("EndDate", model.EndDate);
            }
            ObjCmd.ExecuteNonQuery();
            return RedirectToAction("DiscountList");
        }

        public IActionResult DiscountAddEdit(int DiscountID)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();


            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Product_SelectAll";
            SqlDataReader Op_sqlDataReader = sqlCommand.ExecuteReader();
            DataTable Op_dt = new DataTable();
            Op_dt.Load(Op_sqlDataReader);

            List<ProductModel> li = new List<ProductModel>();

            foreach (DataRow dr in Op_dt.Rows)
            {
                ProductModel obj = new ProductModel();
                obj.ProductID = int.Parse(dr["ProductID"].ToString());
                obj.ProductName = dr["ProductName"].ToString();
                li.Add(obj);

            }
            ViewBag.Productlist = li;


            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Discount_SelectByPk";
            ObjCmd.Parameters.AddWithValue("DiscountID", DiscountID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            DiscountModel model = new DiscountModel();
            foreach (DataRow dr in dt.Rows)
            {
                model.DiscountID = int.Parse(dr["DiscountID"].ToString());
                model.ProductID = Convert.ToInt32(dr["ProductID"]);
                model.Amount = (float)Convert.ToDecimal(dr["Amount"]);
                model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                model.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            return View(model);
        }
    }
}
