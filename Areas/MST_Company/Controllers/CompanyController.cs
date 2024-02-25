using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Inventory_management_system.Areas.MST_Company.Models;
using Inventory_management_system.BAL;

namespace Inventory_management_system.Areas.MST_Company.Controllers
{
    [CheckAccess]
    [Area("MST_Company")]
    [Route("{controller}/{action}")]
    public class CompanyController : Controller
    {
        public IConfiguration Configuration;
        public CompanyController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult CompanyList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection sqlconnection = new SqlConnection(connectionstr);
            sqlconnection.Open();
            SqlCommand sqlCommand = sqlconnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Company_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View("CompanyList", dt);
        }


        public IActionResult CompanySelectByID(int CompanyID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Company_SelectByPk";
            ObjCmd.Parameters.AddWithValue("CompanyID", CompanyID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }
        
        public IActionResult CompanyAddEditMethod(CompanyModel model, int CompanyID = 0)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            if (CompanyID == 0)
            {
                ObjCmd.CommandText = "PR_Company_Insert";
                ObjCmd.Parameters.AddWithValue("CompanyName", model.CompanyName);
                ObjCmd.Parameters.AddWithValue("ContactNumber", model.ContactNumber);
                ObjCmd.Parameters.AddWithValue("Email", model.Email);
                ObjCmd.Parameters.AddWithValue("Address", model.Address);
                ObjCmd.ExecuteNonQuery();
                TempData["Message"] = "Record Inserted Successfully";
            }
            else
            {
                ObjCmd.CommandText = "PR_Company_Update";
                ObjCmd.Parameters.AddWithValue("CompanyID", CompanyID);
                ObjCmd.Parameters.AddWithValue("CompanyName", model.CompanyName);
                ObjCmd.Parameters.AddWithValue("ContactNumber", model.ContactNumber);
                ObjCmd.Parameters.AddWithValue("Email", model.Email);
                ObjCmd.Parameters.AddWithValue("Address", model.Address);
                ObjCmd.ExecuteNonQuery();
                TempData["Message"] = "Record Updated Successfully";
            }
            return RedirectToAction("CompanyList");
        }
       
        public IActionResult CompanyAddEdit(int CompanyID)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Company_SelectByPk";
            ObjCmd.Parameters.AddWithValue("CompanyID", CompanyID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            CompanyModel model = new CompanyModel();
            foreach (DataRow dr in dt.Rows)
            {
                model.CompanyID = int.Parse(dr["CompanyID"].ToString());
                model.CompanyName = dr["CompanyName"].ToString();
                model.ContactNumber = dr["ContactNumber"].ToString();
                model.Email = dr["Email"].ToString();
                model.Address = dr["Address"].ToString();
            }
            return View(model);
        }

    }
}