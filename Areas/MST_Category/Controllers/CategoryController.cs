using Inventory_management_system.Areas.MST_Category.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Inventory_management_system.BAL;

namespace Inventory_management_system.Areas.MST_Category.Controllers
{
    [CheckAccess]

    [Area("MST_Category")]
    [Route("{controller}/{action}")]
    public class CategoryController : Controller
    {
        public IConfiguration Configuration;
        public CategoryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult CategoryList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection sqlconnection = new SqlConnection(connectionstr);
            sqlconnection.Open();
            SqlCommand sqlCommand = sqlconnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Category_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View("CategoryList", dt);
        }


        public IActionResult CategorySelectByID(int CategoryID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Category_SelectByPk";
            ObjCmd.Parameters.AddWithValue("CategoryID", CategoryID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }

        public IActionResult CategoryAddEditMethod(CategoryModel model, int CategoryID = 0)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            if (CategoryID == 0)
            {
                ObjCmd.CommandText = "PR_Category_Insert";
                ObjCmd.Parameters.AddWithValue("CategoryName", model.CategoryName);
                ObjCmd.ExecuteNonQuery();
                TempData["Message"] = "Record Inserted Successfully";
            }
            else
            {
                ObjCmd.CommandText = "PR_Category_Update";
                ObjCmd.Parameters.AddWithValue("CategoryID", CategoryID);
                ObjCmd.Parameters.AddWithValue("CategoryName", model.CategoryName);
                ObjCmd.ExecuteNonQuery();
                TempData["Message"] = "Record Updated Successfully";
            }
            return RedirectToAction("CategoryList");
        }

        public IActionResult CategoryAddEdit(int CategoryID)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Category_SelectByPk";
            ObjCmd.Parameters.AddWithValue("CategoryID", CategoryID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            CategoryModel model = new CategoryModel();
            foreach (DataRow dr in dt.Rows)
            {
                model.CategoryID = int.Parse(dr["CategoryID"].ToString());
                model.CategoryName = dr["CategoryName"].ToString();
            }
            return View(model);
        }
    }
}
