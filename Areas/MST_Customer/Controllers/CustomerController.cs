using Inventory_management_system.Areas.MST_Customer.Models;
using Inventory_management_system.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Inventory_management_system.Areas.MST_Customer.Controllers
{

    [CheckAccess]
    [Area("MST_Customer")]
    [Route("{controller}/{action}")]
    public class CustomerController : Controller
    {
        public IConfiguration Configuration;
        public CustomerController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult CustomerList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection sqlconnection = new SqlConnection(connectionstr);
            sqlconnection.Open();
            SqlCommand sqlCommand = sqlconnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Customer_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View("CustomerList", dt);
        }


        public IActionResult CustomerSelectByID(int CustomerID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Customer_SelectByPk";
            ObjCmd.Parameters.AddWithValue("CustomerID", CustomerID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("CustomerSelectByID", dt);
        }

        public IActionResult CustomerAddEditMethod(CustomerModel model, int CustomerID = 0)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            if (CustomerID == 0)
            {
                ObjCmd.CommandText = "PR_Customer_Insert";
                ObjCmd.Parameters.AddWithValue("CustomerName", model.CustomerName);
                ObjCmd.Parameters.AddWithValue("ContactNumber", model.ContactNumber);
                ObjCmd.Parameters.AddWithValue("Email", model.Email);
                ObjCmd.Parameters.AddWithValue("Address", model.Address);
                ObjCmd.ExecuteNonQuery();
                TempData["Message"] = "Record Inserted Successfully";
            }
            else
            {
                ObjCmd.CommandText = "PR_Customer_Update";
                ObjCmd.Parameters.AddWithValue("CustomerID", CustomerID);
                ObjCmd.Parameters.AddWithValue("CustomerName", model.CustomerName);
                ObjCmd.Parameters.AddWithValue("ContactNumber", model.ContactNumber);
                ObjCmd.Parameters.AddWithValue("Email", model.Email);
                ObjCmd.Parameters.AddWithValue("Address", model.Address);
                ObjCmd.ExecuteNonQuery();
                TempData["Message"] = "Record Updated Successfully";
            }
            return RedirectToAction("CustomerList");
        }

        public IActionResult CustomerAddEdit(int CustomerID)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Customer_SelectByPk";
            ObjCmd.Parameters.AddWithValue("CustomerID", CustomerID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            CustomerModel model = new CustomerModel();
            foreach (DataRow dr in dt.Rows)
            {
                model.CustomerID = int.Parse(dr["CustomerID"].ToString());
                model.CustomerName = dr["CustomerName"].ToString();
                model.ContactNumber = dr["ContactNumber"].ToString();
                model.Email = dr["Email"].ToString();
                model.Address = dr["Address"].ToString();
            }
            return View(model);

        }

    }
}
