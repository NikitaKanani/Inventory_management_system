using Inventory_management_system.Areas.MST_Category.Models;
using Inventory_management_system.Areas.MST_Company.Models;
using Inventory_management_system.Areas.MST_Vendor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Inventory_management_system.BAL;

namespace Inventory_management_system.Areas.MST_Vendor.Controllers
{
    [CheckAccess]
    [Area("MST_Vendor")]
    [Route("{controller}/{action}")]
    public class VendorController : Controller
    {
        public IConfiguration Configuration;
        public VendorController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult VendorList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection sqlconnection = new SqlConnection(connectionstr);
            sqlconnection.Open();
            SqlCommand sqlCommand = sqlconnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Vendor_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View("VendorList", dt);
        }


        public IActionResult VendorSelectByID(int VendorID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Vendor_SelectByPk";
            ObjCmd.Parameters.AddWithValue("VendorID", VendorID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }

        public IActionResult VendorAddEditMethod(VendorModel model, int VendorID = 0)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.Parameters.AddWithValue("CompanyID", model.CompanyID);
            ObjCmd.Parameters.AddWithValue("VendorName", model.VendorName);
            ObjCmd.Parameters.AddWithValue("ContactNumber", model.ContactNumber);
            ObjCmd.Parameters.AddWithValue("Email", model.Email);
            ObjCmd.Parameters.AddWithValue("Address", model.Address); 

            if (model.VendorID == null || model.VendorID == 0)
            {
                ObjCmd.CommandText = "PR_Vendor_Insert";
                TempData["Message"] = "Record Inserted Successfully";
            }
            else
            {
                ObjCmd.CommandText = "PR_Vendor_Update";
                ObjCmd.Parameters.AddWithValue("VendorID", model.VendorID);
                TempData["Message"] = "Record Updated Successfully";
            }
            ObjCmd.ExecuteNonQuery();
            return RedirectToAction("VendorList");
        }

        public IActionResult VendorAddEdit(int VendorID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
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

            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Vendor_SelectByPk";
            ObjCmd.Parameters.AddWithValue("VendorID", VendorID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            VendorModel model = new VendorModel();
            foreach (DataRow dr in dt.Rows)
            {
                model.VendorID = int.Parse(dr["VendorID"].ToString());
                model.VendorName = dr["VendorName"].ToString();
                model.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                model.ContactNumber = dr["ContactNumber"].ToString();                           
                model.Email = dr["Email"].ToString();                   
                model.Address = dr["Address"].ToString();             
            }
            return View(model);
        }
    }
}
