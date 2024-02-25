using Inventory_management_system.Areas.MST_Purchase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Inventory_management_system.BAL;
using Inventory_management_system.Areas.MST_Vendor.Models;
using Inventory_management_system.Areas.MST_Category.Models;
using Inventory_management_system.Areas.MST_Company.Models;

namespace Inventory_management_system.Areas.MST_Purchase.Controllers
{
    [CheckAccess]
    [Area("MST_Purchase")]
    [Route("{controller}/{action}")]
    public class PurchaseController : Controller
    {
        public IConfiguration Configuration;
        public PurchaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult PurchaseList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection sqlconnection = new SqlConnection(connectionstr);
            sqlconnection.Open();
            SqlCommand sqlCommand = sqlconnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Purchase_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View("PurchaseList", dt);
        }


        public IActionResult PurchaseSelectByID(int PurchaseID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Purchase_SelectByPk";
            ObjCmd.Parameters.AddWithValue("PurchaseID", PurchaseID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View("PurchaseSelectByID", dt);
        }

        public IActionResult PurchaseAddEditMethod(PurchaseModel model, int PurchaseID = 0)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;


            if (PurchaseID == 0)
            {
                ObjCmd.CommandText = "PR_Purchase_Insert";
                ObjCmd.Parameters.AddWithValue("VendorID", model.VendorID);
                ObjCmd.Parameters.AddWithValue("PurchaseDate", model.PurchaseDate);
                ObjCmd.Parameters.AddWithValue("PaymentStatus", model.PaymentStatus);
                ObjCmd.Parameters.AddWithValue("DueDate", model.DueDate);
                ObjCmd.ExecuteNonQuery();
                TempData["Message"] = "Record Inserted Successfully";
            }
            else
            {



                ObjCmd.CommandText = "PR_Purchase_Update";
                ObjCmd.Parameters.AddWithValue("PurchaseID", PurchaseID);
                ObjCmd.Parameters.AddWithValue("VendorID", model.VendorID);
                ObjCmd.Parameters.AddWithValue("PurchaseDate", model.PurchaseDate);
                ObjCmd.Parameters.AddWithValue("PaymentStatus", model.PaymentStatus);
                ObjCmd.Parameters.AddWithValue("DueDate", model.DueDate);
                ObjCmd.ExecuteNonQuery();
                TempData["Message"] = "Record Updated Successfully";
            }
            return RedirectToAction("PurchaseList");

        }

        public IActionResult PurchaseAddEdit(int PurchaseID)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Vendor_SelectAll";
            SqlDataReader Op_sqlDataReader = sqlCommand.ExecuteReader();
            DataTable Op_dt = new DataTable();
            Op_dt.Load(Op_sqlDataReader);
            List<VendorModel> li = new List<VendorModel>();
            foreach (DataRow dr in Op_dt.Rows)
            {
                VendorModel obj = new VendorModel();
                obj.VendorID = int.Parse(dr["VendorID"].ToString());
                obj.VendorName = dr["VendorName"].ToString();
                li.Add(obj);

            }
            ViewBag.Vendorlist = li;




            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Purchase_SelectByPk";
            ObjCmd.Parameters.AddWithValue("PurchaseID", PurchaseID);
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            PurchaseModel model = new PurchaseModel();
            foreach (DataRow dr in dt.Rows)
            {
                model.PurchaseID = int.Parse(dr["PurchaseID"].ToString());
                model.PaymentStatus = dr["PaymentStatus"].ToString();
                model.VendorID = Convert.ToInt32(dr["VendorID"]);
                model.TotalAmount = (float)Convert.ToDecimal(dr["TotalAmount"]);
                model.PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]);
                model.DueDate = Convert.ToDateTime(dr["DueDate"]);
            }
            return View(model);

        }
    }
}
